// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using TerraFX.Interop;
using TerraFX.Numerics;
using TerraFX.Utilities;
using static TerraFX.Graphics.Providers.D3D12.HelperUtilities;
using static TerraFX.Interop.D3D_PRIMITIVE_TOPOLOGY;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.D3D12_RTV_DIMENSION;
using static TerraFX.Interop.DXGI_FORMAT;
using static TerraFX.Interop.Windows;
using static TerraFX.Utilities.AssertionUtilities;
using static TerraFX.Utilities.DisposeUtilities;
using static TerraFX.Utilities.ExceptionUtilities;
using static TerraFX.Utilities.State;

namespace TerraFX.Graphics.Providers.D3D12
{
    /// <inheritdoc />
    public sealed unsafe class D3D12GraphicsContext : GraphicsContext
    {
        private readonly D3D12GraphicsFence _graphicsFence;
        private readonly D3D12GraphicsFence _waitForExecuteCompletionGraphicsFence;

        private ValueLazy<Pointer<ID3D12CommandAllocator>> _d3d12CommandAllocator;
        private ValueLazy<Pointer<ID3D12GraphicsCommandList>> _d3d12GraphicsCommandList;
        private ValueLazy<Pointer<ID3D12Resource>> _d3d12RenderTargetResource;
        private ValueLazy<D3D12_CPU_DESCRIPTOR_HANDLE> _d3d12RenderTargetView;

        private State _state;

        internal D3D12GraphicsContext(D3D12GraphicsDevice graphicsDevice, int index)
            : base(graphicsDevice, index)
        {
            _graphicsFence = new D3D12GraphicsFence(graphicsDevice);
            _waitForExecuteCompletionGraphicsFence = new D3D12GraphicsFence(graphicsDevice);

            _d3d12CommandAllocator = new ValueLazy<Pointer<ID3D12CommandAllocator>>(CreateD3D12CommandAllocator);
            _d3d12GraphicsCommandList = new ValueLazy<Pointer<ID3D12GraphicsCommandList>>(CreateD3D12GraphicsCommandList);
            _d3d12RenderTargetView = new ValueLazy<D3D12_CPU_DESCRIPTOR_HANDLE>(CreateD3D12RenderTargetDescriptor);
            _d3d12RenderTargetResource = new ValueLazy<Pointer<ID3D12Resource>>(CreateD3D12RenderTargetResource);

            _ = _state.Transition(to: Initialized);
        }

        /// <summary>Finalizes an instance of the <see cref="D3D12GraphicsContext" /> class.</summary>
        ~D3D12GraphicsContext()
        {
            Dispose(isDisposing: false);
        }

        /// <summary>Gets the <see cref="ID3D12CommandAllocator" /> used by the context.</summary>
        /// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
        public ID3D12CommandAllocator* D3D12CommandAllocator => _d3d12CommandAllocator.Value;

        /// <summary>Gets the <see cref="ID3D12GraphicsCommandList" /> used by the context.</summary>
        /// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
        public ID3D12GraphicsCommandList* D3D12GraphicsCommandList => _d3d12GraphicsCommandList.Value;

        /// <inheritdoc cref="GraphicsContext.GraphicsDevice" />
        public D3D12GraphicsDevice D3D12GraphicsDevice => (D3D12GraphicsDevice)GraphicsDevice;

        /// <inheritdoc cref="D3D12GraphicsContext.GraphicsFence" />
        public D3D12GraphicsFence D3D12GraphicsFence => _graphicsFence;

        /// <summary>Gets the <see cref="ID3D12Resource" /> for the render target used by the context.</summary>
        /// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
        public ID3D12Resource* D3D12RenderTargetResource => _d3d12RenderTargetResource.Value;

        /// <summary>Gets the <see cref="D3D12_CPU_DESCRIPTOR_HANDLE" /> for the render target used by the context.</summary>
        /// <exception cref="ObjectDisposedException">The context has been disposed.</exception>
        public D3D12_CPU_DESCRIPTOR_HANDLE D3D12RenderTargetView => _d3d12RenderTargetView.Value;

        /// <inheritdoc />
        public override GraphicsFence GraphicsFence => D3D12GraphicsFence;

        /// <summary>Gets a graphics fence that is used to wait for the context to finish execution.</summary>
        public D3D12GraphicsFence WaitForExecuteCompletionGraphicsFence => _waitForExecuteCompletionGraphicsFence;

        /// <inheritdoc />
        public override void BeginDrawing(ColorRgba backgroundColor)
        {
            var graphicsCommandList = D3D12GraphicsCommandList;

            var renderTargetResourceBarrier = D3D12_RESOURCE_BARRIER.InitTransition(D3D12RenderTargetResource, D3D12_RESOURCE_STATE_PRESENT, D3D12_RESOURCE_STATE_RENDER_TARGET);
            graphicsCommandList->ResourceBarrier(1, &renderTargetResourceBarrier);

            var renderTargetView = D3D12RenderTargetView;
            graphicsCommandList->OMSetRenderTargets(1, &renderTargetView, RTsSingleHandleToDescriptorRange: TRUE, pDepthStencilDescriptor: null);

            var graphicsSurface = D3D12GraphicsDevice.GraphicsSurface;

            var graphicsSurfaceWidth = graphicsSurface.Width;
            var graphicsSurfaceHeight = graphicsSurface.Height;

            var viewport = new D3D12_VIEWPORT {
                Width = graphicsSurfaceWidth,
                Height = graphicsSurfaceHeight,
                MinDepth = D3D12_MIN_DEPTH,
                MaxDepth = D3D12_MAX_DEPTH,
            };
            graphicsCommandList->RSSetViewports(1, &viewport);

            var scissorRect = new RECT {
                right = (int)graphicsSurfaceWidth,
                bottom = (int)graphicsSurfaceHeight,
            };
            graphicsCommandList->RSSetScissorRects(1, &scissorRect);

            graphicsCommandList->ClearRenderTargetView(renderTargetView, (float*)&backgroundColor, NumRects: 0, pRects: null);
            graphicsCommandList->IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

            var descriptorHeaps = stackalloc ID3D12DescriptorHeap*[1] {
                D3D12GraphicsDevice.D3D12ShaderResourceDescriptorHeap,
            };
            graphicsCommandList->SetDescriptorHeaps(1, descriptorHeaps);
        }

        /// <inheritdoc />
        public override void BeginFrame()
        {
            var graphicsFence = D3D12GraphicsFence;

            graphicsFence.Wait();
            graphicsFence.Reset();

            var commandAllocator = D3D12CommandAllocator;

            ThrowExternalExceptionIfFailed(nameof(ID3D12CommandAllocator.Reset), commandAllocator->Reset());
            ThrowExternalExceptionIfFailed(nameof(ID3D12GraphicsCommandList.Reset), D3D12GraphicsCommandList->Reset(commandAllocator, pInitialState: null));
        }

        /// <inheritdoc cref="Copy(GraphicsBuffer, GraphicsBuffer)" />
        public void Copy(D3D12GraphicsBuffer destination, D3D12GraphicsBuffer source)
        {
            ThrowIfNull(destination, nameof(destination));
            ThrowIfNull(source, nameof(source));

            var graphicsCommandList = D3D12GraphicsCommandList;

            var destinationCpuAccess = destination.GraphicsHeap.CpuAccess;
            var sourceCpuAccess = source.GraphicsHeap.CpuAccess;

            var d3d12DestinationResource = destination.D3D12Resource;
            var d3d12SourceResource = source.D3D12Resource;

            var d3d12DestinationResourceState = destination.D3D12ResourceState;
            var d3d12SourceResourceState = source.D3D12ResourceState;

            BeginCopy();

            graphicsCommandList->CopyResource(d3d12DestinationResource, d3d12SourceResource);

            EndCopy();

            void BeginCopy()
            {
                var resourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[2];
                var numResourceBarriers = 0u;

                if (destinationCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12DestinationResource,
                        stateBefore: d3d12DestinationResourceState,
                        stateAfter: D3D12_RESOURCE_STATE_COPY_DEST
                    );
                    numResourceBarriers++;
                }

                if (sourceCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12SourceResource,
                        stateBefore: d3d12SourceResourceState,
                        stateAfter: D3D12_RESOURCE_STATE_COPY_SOURCE
                    );
                    numResourceBarriers++;
                }

                if (numResourceBarriers != 0)
                {
                    graphicsCommandList->ResourceBarrier(numResourceBarriers, resourceBarriers);
                }
            }

            void EndCopy()
            {
                var resourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[2];
                var numResourceBarriers = 0u;

                if (sourceCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12SourceResource,
                        stateBefore: D3D12_RESOURCE_STATE_COPY_SOURCE,
                        stateAfter: d3d12SourceResourceState
                    );
                    numResourceBarriers++;
                }

                if (destinationCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12DestinationResource,
                        stateBefore: D3D12_RESOURCE_STATE_COPY_DEST,
                        stateAfter: d3d12DestinationResourceState
                    );
                    numResourceBarriers++;
                }

                if (numResourceBarriers != 0)
                {
                    graphicsCommandList->ResourceBarrier(numResourceBarriers, resourceBarriers);
                }
            }
        }

        /// <inheritdoc cref="Copy(GraphicsTexture, GraphicsBuffer)" />
        public void Copy(D3D12GraphicsTexture destination, D3D12GraphicsBuffer source)
        {
            ThrowIfNull(destination, nameof(destination));
            ThrowIfNull(source, nameof(source));

            var graphicsDevice = D3D12GraphicsDevice.D3D12Device;
            var graphicsCommandList = D3D12GraphicsCommandList;

            var destinationCpuAccess = destination.GraphicsHeap.CpuAccess;
            var sourceCpuAccess = source.GraphicsHeap.CpuAccess;

            var d3d12DestinationResource = destination.D3D12Resource;
            var d3d12SourceResource = source.D3D12Resource;

            var d3d12DestinationResourceState = destination.D3D12ResourceState;
            var d3d12SourceResourceState = source.D3D12ResourceState;

            BeginCopy();

            D3D12_PLACED_SUBRESOURCE_FOOTPRINT sourceFootprint;

            var destinationDesc = d3d12DestinationResource->GetDesc();
            graphicsDevice->GetCopyableFootprints(&destinationDesc, FirstSubresource: 0, NumSubresources: 1, BaseOffset: 0, &sourceFootprint, pNumRows: null, pRowSizeInBytes: null, pTotalBytes: null);

            var d3d12DestinationTextureCopyLocation = new D3D12_TEXTURE_COPY_LOCATION(d3d12DestinationResource, Sub: 0);
            var d3d12SourceTextureCopyLocation = new D3D12_TEXTURE_COPY_LOCATION(d3d12SourceResource, in sourceFootprint);

            graphicsCommandList->CopyTextureRegion(&d3d12DestinationTextureCopyLocation, DstX: 0, DstY: 0, DstZ: 0, &d3d12SourceTextureCopyLocation, pSrcBox: null);

            EndCopy();

            void BeginCopy()
            {
                var resourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[2];
                var numResourceBarriers = 0u;

                if (destinationCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12DestinationResource,
                        stateBefore: d3d12DestinationResourceState,
                        stateAfter: D3D12_RESOURCE_STATE_COPY_DEST
                    );
                    numResourceBarriers++;
                }

                if (sourceCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12SourceResource,
                        stateBefore: d3d12SourceResourceState,
                        stateAfter: D3D12_RESOURCE_STATE_COPY_SOURCE
                    );
                    numResourceBarriers++;
                }

                if (numResourceBarriers != 0)
                {
                    graphicsCommandList->ResourceBarrier(numResourceBarriers, resourceBarriers);
                }
            }

            void EndCopy()
            {
                var resourceBarriers = stackalloc D3D12_RESOURCE_BARRIER[2];
                var numResourceBarriers = 0u;

                if (sourceCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12SourceResource,
                        stateBefore: D3D12_RESOURCE_STATE_COPY_SOURCE,
                        stateAfter: d3d12SourceResourceState
                    );
                    numResourceBarriers++;
                }

                if (destinationCpuAccess == GraphicsHeapCpuAccess.None)
                {
                    resourceBarriers[numResourceBarriers] = D3D12_RESOURCE_BARRIER.InitTransition(
                        d3d12DestinationResource,
                        stateBefore: D3D12_RESOURCE_STATE_COPY_DEST,
                        stateAfter: d3d12DestinationResourceState
                    );
                    numResourceBarriers++;
                }

                if (numResourceBarriers != 0)
                {
                    graphicsCommandList->ResourceBarrier(numResourceBarriers, resourceBarriers);
                }
            }
        }

        /// <inheritdoc />
        public override void Copy(GraphicsBuffer destination, GraphicsBuffer source) => Copy((D3D12GraphicsBuffer)destination, (D3D12GraphicsBuffer)source);

        /// <inheritdoc />
        public override void Copy(GraphicsTexture destination, GraphicsBuffer source) => Copy((D3D12GraphicsTexture)destination, (D3D12GraphicsBuffer)source);

        /// <inheritdoc cref="Draw(GraphicsPrimitive)" />
        public void Draw(D3D12GraphicsPrimitive graphicsPrimitive)
        {
            ThrowIfNull(graphicsPrimitive, nameof(graphicsPrimitive));

            var graphicsCommandList = D3D12GraphicsCommandList;
            var graphicsPipeline = graphicsPrimitive.D3D12GraphicsPipeline;
            var vertexBuffer = graphicsPrimitive.D3D12VertexBuffer;

            graphicsCommandList->SetGraphicsRootSignature(graphicsPipeline.D3D12Signature.D3D12RootSignature);
            graphicsCommandList->SetPipelineState(graphicsPipeline.D3D12PipelineState);

            var vertexBufferView = new D3D12_VERTEX_BUFFER_VIEW {
                BufferLocation = vertexBuffer.D3D12Resource->GetGPUVirtualAddress(),
                StrideInBytes = (uint)vertexBuffer.Stride,
                SizeInBytes = (uint)vertexBuffer.Size,
            };
            graphicsCommandList->IASetVertexBuffers(StartSlot: 0, NumViews: 1, &vertexBufferView);

            var inputResources = graphicsPrimitive.InputResources;
            var inputResourcesLength = inputResources.Length;

            for (var index = 0; index < inputResourcesLength; index++)
            {
                var inputResource = inputResources[index];

                if (inputResource is D3D12GraphicsBuffer d3d12GraphicsBuffer)
                {
                    graphicsCommandList->SetGraphicsRootConstantBufferView(unchecked((uint)index), d3d12GraphicsBuffer.D3D12Resource->GetGPUVirtualAddress());
                }
                else if (inputResource is D3D12GraphicsTexture d3d12GraphicsTexture)
                {
                    graphicsCommandList->SetGraphicsRootDescriptorTable(unchecked((uint)index), D3D12GraphicsDevice.D3D12ShaderResourceDescriptorHeap->GetGPUDescriptorHandleForHeapStart());
                }
            }

            var indexBuffer = graphicsPrimitive.D3D12IndexBuffer;

            if (indexBuffer != null)
            {
                var indexBufferStride = indexBuffer.Stride;
                var indexFormat = DXGI_FORMAT_R16_UINT;

                if (indexBufferStride != 2)
                {
                    Assert(indexBufferStride == 4, "Index Buffer has an unsupported stride.");
                    indexFormat = DXGI_FORMAT_R32_UINT;
                }

                var indexBufferView = new D3D12_INDEX_BUFFER_VIEW {
                    BufferLocation = indexBuffer.D3D12Resource->GetGPUVirtualAddress(),
                    SizeInBytes = (uint)indexBuffer.Size,
                    Format = indexFormat,
                };
                graphicsCommandList->IASetIndexBuffer(&indexBufferView);

                graphicsCommandList->DrawIndexedInstanced(IndexCountPerInstance: (uint)(indexBuffer.Size / indexBufferStride), InstanceCount: 1, StartIndexLocation: 0, BaseVertexLocation: 0, StartInstanceLocation: 0);
            }
            else
            {
                graphicsCommandList->DrawInstanced(VertexCountPerInstance: (uint)(vertexBuffer.Size / vertexBuffer.Stride), InstanceCount: 1, StartVertexLocation: 0, StartInstanceLocation: 0);
            }
        }

        /// <inheritdoc />
        public override void Draw(GraphicsPrimitive graphicsPrimitive) => Draw((D3D12GraphicsPrimitive)graphicsPrimitive);

        /// <inheritdoc />
        public override void EndDrawing()
        {
            var renderTargetResourceBarrier = D3D12_RESOURCE_BARRIER.InitTransition(D3D12RenderTargetResource, D3D12_RESOURCE_STATE_RENDER_TARGET, D3D12_RESOURCE_STATE_PRESENT);
            D3D12GraphicsCommandList->ResourceBarrier(1, &renderTargetResourceBarrier);
        }

        /// <inheritdoc />
        public override void EndFrame()
        {
            var graphicsCommandList = D3D12GraphicsCommandList;

            var commandQueue = D3D12GraphicsDevice.D3D12CommandQueue;
            ThrowExternalExceptionIfFailed(nameof(ID3D12GraphicsCommandList.Close), graphicsCommandList->Close());
            commandQueue->ExecuteCommandLists(1, (ID3D12CommandList**)&graphicsCommandList);

            var executeGraphicsFence = WaitForExecuteCompletionGraphicsFence;
            ThrowExternalExceptionIfFailed(nameof(ID3D12CommandQueue.Signal), commandQueue->Signal(executeGraphicsFence.D3D12Fence, executeGraphicsFence.D3D12FenceSignalValue));

            executeGraphicsFence.Wait();
            executeGraphicsFence.Reset();
        }

        /// <inheritdoc />
        protected override void Dispose(bool isDisposing)
        {
            var priorState = _state.BeginDispose();

            if (priorState < Disposing)
            {
                _d3d12GraphicsCommandList.Dispose(ReleaseIfNotNull);
                _d3d12CommandAllocator.Dispose(ReleaseIfNotNull);
                _d3d12RenderTargetView.Dispose();
                _d3d12RenderTargetResource.Dispose(ReleaseIfNotNull);

                DisposeIfNotNull(_waitForExecuteCompletionGraphicsFence);
                DisposeIfNotNull(_graphicsFence);
            }

            _state.EndDispose();
        }

        internal void OnGraphicsSurfaceSizeChanged(object? sender, PropertyChangedEventArgs<Vector2> eventArgs)
        {
            if (_d3d12RenderTargetView.IsCreated)
            {
                _d3d12RenderTargetView.Reset(CreateD3D12RenderTargetDescriptor);

                ReleaseIfNotNull(_d3d12RenderTargetResource.Value);
                _d3d12RenderTargetResource.Reset(CreateD3D12RenderTargetResource);
            }
        }

        private Pointer<ID3D12CommandAllocator> CreateD3D12CommandAllocator()
        {
            _state.ThrowIfDisposedOrDisposing();

            ID3D12CommandAllocator* d3d12CommandAllocator;

            var iid = IID_ID3D12CommandAllocator;
            ThrowExternalExceptionIfFailed(nameof(ID3D12Device.CreateCommandAllocator), D3D12GraphicsDevice.D3D12Device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, &iid, (void**)&d3d12CommandAllocator));

            return d3d12CommandAllocator;
        }

        private Pointer<ID3D12GraphicsCommandList> CreateD3D12GraphicsCommandList()
        {
            _state.ThrowIfDisposedOrDisposing();

            ID3D12GraphicsCommandList* d3d12GraphicsCommandList;

            var iid = IID_ID3D12GraphicsCommandList;
            ThrowExternalExceptionIfFailed(nameof(ID3D12Device.CreateCommandList), D3D12GraphicsDevice.D3D12Device->CreateCommandList(nodeMask: 0, D3D12_COMMAND_LIST_TYPE_DIRECT, D3D12CommandAllocator, pInitialState: null, &iid, (void**)&d3d12GraphicsCommandList));

            // Command lists are created in the recording state, but there is nothing
            // to record yet. The main loop expects it to be closed, so close it now.
            ThrowExternalExceptionIfFailed(nameof(ID3D12GraphicsCommandList.Close), d3d12GraphicsCommandList->Close());

            return d3d12GraphicsCommandList;
        }

        private Pointer<ID3D12Resource> CreateD3D12RenderTargetResource()
        {
            _state.ThrowIfDisposedOrDisposing();

            ID3D12Resource* renderTargetResource;

            var iid = IID_ID3D12Resource;
            ThrowExternalExceptionIfFailed(nameof(IDXGISwapChain.GetBuffer), D3D12GraphicsDevice.DxgiSwapChain->GetBuffer(unchecked((uint)Index), &iid, (void**)&renderTargetResource));

            return renderTargetResource;
        }

        private D3D12_CPU_DESCRIPTOR_HANDLE CreateD3D12RenderTargetDescriptor()
        {
            _state.ThrowIfDisposedOrDisposing();

            D3D12_CPU_DESCRIPTOR_HANDLE renderTargetViewHandle;

            var graphicsDevice = D3D12GraphicsDevice;
            var d3d12Device = graphicsDevice.D3D12Device;

            var renderTargetViewDesc = new D3D12_RENDER_TARGET_VIEW_DESC {
                Format = graphicsDevice.DxgiSwapChainFormat,
                ViewDimension = D3D12_RTV_DIMENSION_TEXTURE2D,
                Anonymous = new D3D12_RENDER_TARGET_VIEW_DESC._Anonymous_e__Union {
                    Texture2D = new D3D12_TEX2D_RTV(),
                },
            };

            var renderTargetDescriptorIncrementSize = d3d12Device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_RTV);

            renderTargetViewHandle = graphicsDevice.D3D12RenderTargetDescriptorHeap->GetCPUDescriptorHandleForHeapStart();
            _ = renderTargetViewHandle.Offset(Index, renderTargetDescriptorIncrementSize);

            d3d12Device->CreateRenderTargetView(D3D12RenderTargetResource, &renderTargetViewDesc, renderTargetViewHandle);

            return renderTargetViewHandle;
        }
    }
}
