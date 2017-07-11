// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from um\d3d12.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TerraFX.Interop
{
    [Guid("553103FB-1FE7-4557-BB38-946D7D0E7CA7")]
    unsafe public /* blittable */ struct ID3D12GraphicsCommandList1
    {
        #region Fields
        public readonly void* /* Vtbl* */ lpVtbl;
        #endregion

        #region Delegates
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate void AtomicCopyBufferUINT(
            [In] ID3D12GraphicsCommandList1* This,
            [In] ID3D12Resource* pDstBuffer,
            [In] UINT64 DstOffset,
            [In] ID3D12Resource* pSrcBuffer,
            [In] UINT64 SrcOffset,
            [In] UINT Dependencies,
            [In] /* readonly */ ID3D12Resource** ppDependentResources,
            [In] /* readonly */ D3D12_SUBRESOURCE_RANGE_UINT64* pDependentSubresourceRanges
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate void AtomicCopyBufferUINT64(
            [In] ID3D12GraphicsCommandList1* This,
            [In] ID3D12Resource* pDstBuffer,
            [In] UINT64 DstOffset,
            [In] ID3D12Resource* pSrcBuffer,
            [In] UINT64 SrcOffset,
            [In] UINT Dependencies,
            [In] /* readonly */ ID3D12Resource** ppDependentResources,
            [In] /* readonly */ D3D12_SUBRESOURCE_RANGE_UINT64* pDependentSubresourceRanges
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate void OMSetDepthBounds(
            [In] ID3D12GraphicsCommandList1* This,
            [In] FLOAT Min,
            [In] FLOAT Max
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate void SetSamplePositions(
            [In] ID3D12GraphicsCommandList1* This,
            [In] UINT NumSamplesPerPixel,
            [In] UINT NumPixels,
            [In] D3D12_SAMPLE_POSITION* pSamplePositions
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate void ResolveSubresourceRegion(
            [In] ID3D12GraphicsCommandList1* This,
            [In] ID3D12Resource* pDstResource,
            [In] UINT DstSubresource,
            [In] UINT DstX,
            [In] UINT DstY,
            [In] ID3D12Resource* pSrcResource,
            [In] UINT SrcSubresource,
            [In, Optional] D3D12_RECT* pSrcRect,
            [In] DXGI_FORMAT Format,
            [In] D3D12_RESOLVE_MODE ResolveMode
        );
        #endregion

        #region Structs
        public /* blittable */ struct Vtbl
        {
            #region Fields
            public ID3D12GraphicsCommandList.Vtbl BaseVtbl;

            public AtomicCopyBufferUINT AtomicCopyBufferUINT;

            public AtomicCopyBufferUINT64 AtomicCopyBufferUINT64;

            public OMSetDepthBounds OMSetDepthBounds;

            public SetSamplePositions SetSamplePositions;

            public ResolveSubresourceRegion ResolveSubresourceRegion;
            #endregion
        }
        #endregion
    }
}