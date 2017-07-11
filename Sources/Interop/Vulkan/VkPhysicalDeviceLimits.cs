// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from src\spec\vk.xml in the Vulkan-Docs repository for tag v1.0.51-core
// Original source is Copyright © 2015-2017 The Khronos Group Inc.

using TerraFX.Utilities;

namespace TerraFX.Interop
{
    public /* blittable */ struct VkPhysicalDeviceLimits
    {
        #region Fields
        public uint maxImageDimension1D;

        public uint maxImageDimension2D;

        public uint maxImageDimension3D;

        public uint maxImageDimensionCube;

        public uint maxImageArrayLayers;

        public uint maxTexelBufferElements;

        public uint maxUniformBufferRange;

        public uint maxStorageBufferRange;

        public uint maxPushConstantsSize;

        public uint maxMemoryAllocationCount;

        public uint maxSamplerAllocationCount;

        public VkDeviceSize bufferImageGranularity;

        public VkDeviceSize sparseAddressSpaceSize;

        public uint maxBoundDescriptorSets;

        public uint maxPerStageDescriptorSamplers;

        public uint maxPerStageDescriptorUniformBuffers;

        public uint maxPerStageDescriptorStorageBuffers;

        public uint maxPerStageDescriptorSampledImages;

        public uint maxPerStageDescriptorStorageImages;

        public uint maxPerStageDescriptorInputAttachments;

        public uint maxPerStageResources;

        public uint maxDescriptorSetSamplers;

        public uint maxDescriptorSetUniformBuffers;

        public uint maxDescriptorSetUniformBuffersDynamic;

        public uint maxDescriptorSetStorageBuffers;

        public uint maxDescriptorSetStorageBuffersDynamic;

        public uint maxDescriptorSetSampledImages;

        public uint maxDescriptorSetStorageImages;

        public uint maxDescriptorSetInputAttachments;

        public uint maxVertexInputAttributes;

        public uint maxVertexInputBindings;

        public uint maxVertexInputAttributeOffset;

        public uint maxVertexInputBindingStride;

        public uint maxVertexOutputComponents;

        public uint maxTessellationGenerationLevel;

        public uint maxTessellationPatchSize;

        public uint maxTessellationControlPerVertexInputComponents;

        public uint maxTessellationControlPerVertexOutputComponents;

        public uint maxTessellationControlPerPatchOutputComponents;

        public uint maxTessellationControlTotalOutputComponents;

        public uint maxTessellationEvaluationInputComponents;

        public uint maxTessellationEvaluationOutputComponents;

        public uint maxGeometryShaderInvocations;

        public uint maxGeometryInputComponents;

        public uint maxGeometryOutputComponents;

        public uint maxGeometryOutputVertices;

        public uint maxGeometryTotalOutputComponents;

        public uint maxFragmentInputComponents;

        public uint maxFragmentOutputAttachments;

        public uint maxFragmentDualSrcAttachments;

        public uint maxFragmentCombinedOutputResources;

        public uint maxComputeSharedMemorySize;

        public _maxComputeWorkGroupCount_e__FixedBuffer maxComputeWorkGroupCount;

        public uint maxComputeWorkGroupInvocations;

        public _maxComputeWorkGroupSize_e__FixedBuffer maxComputeWorkGroupSize;

        public uint subPixelPrecisionBits;

        public uint subTexelPrecisionBits;

        public uint mipmapPrecisionBits;

        public uint maxDrawIndexedIndexValue;

        public uint maxDrawIndirectCount;

        public float maxSamplerLodBias;

        public float maxSamplerAnisotropy;

        public uint maxViewports;

        public _maxViewportDimensions_e__FixedBuffer maxViewportDimensions;

        public _viewportBoundsRange_e__FixedBuffer viewportBoundsRange;

        public uint viewportSubPixelBits;

        public nuint minMemoryMapAlignment;

        public VkDeviceSize minTexelBufferOffsetAlignment;

        public VkDeviceSize minUniformBufferOffsetAlignment;

        public VkDeviceSize minStorageBufferOffsetAlignment;

        public int minTexelOffset;

        public uint maxTexelOffset;

        public int minTexelGatherOffset;

        public uint maxTexelGatherOffset;

        public float minInterpolationOffset;

        public float maxInterpolationOffset;

        public uint subPixelInterpolationOffsetBits;

        public uint maxFramebufferWidth;

        public uint maxFramebufferHeight;

        public uint maxFramebufferLayers;

        public VkSampleCountFlags framebufferColorSampleCounts;

        public VkSampleCountFlags framebufferDepthSampleCounts;

        public VkSampleCountFlags framebufferStencilSampleCounts;

        public VkSampleCountFlags framebufferNoAttachmentsSampleCounts;

        public uint maxColorAttachments;

        public VkSampleCountFlags sampledImageColorSampleCounts;

        public VkSampleCountFlags sampledImageIntegerSampleCounts;

        public VkSampleCountFlags sampledImageDepthSampleCounts;

        public VkSampleCountFlags sampledImageStencilSampleCounts;

        public VkSampleCountFlags storageImageSampleCounts;

        public uint maxSampleMaskWords;

        public VkBool32 timestampComputeAndGraphics;

        public float timestampPeriod;

        public uint maxClipDistances;

        public uint maxCullDistances;

        public uint maxCombinedClipAndCullDistances;

        public uint discreteQueuePriorities;

        public _pointSizeRange_e__FixedBuffer pointSizeRange;

        public _lineWidthRange_e__FixedBuffer lineWidthRange;

        public float pointSizeGranularity;

        public float lineWidthGranularity;

        public VkBool32 strictLines;

        public VkBool32 standardSampleLocations;

        public VkDeviceSize optimalBufferCopyOffsetAlignment;

        public VkDeviceSize optimalBufferCopyRowPitchAlignment;

        public VkDeviceSize nonCoherentAtomSize;
        #endregion

        #region Structs
        unsafe public /* blittable */ struct _maxComputeWorkGroupCount_e__FixedBuffer
        {
            #region Fields
            public uint e0;

            public uint e1;

            public uint e2;
            #endregion

            #region Properties
            public uint this[int index]
            {
                get
                {
                    if ((uint)(index) > 2) // (index < 0) || (index > 2)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((uint)(index) > 2) // (index < 0) || (index > 2)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }

        unsafe public /* blittable */ struct _maxComputeWorkGroupSize_e__FixedBuffer
        {
            #region Fields
            public uint e0;

            public uint e1;

            public uint e2;
            #endregion

            #region Properties
            public uint this[int index]
            {
                get
                {
                    if ((uint)(index) > 2) // (index < 0) || (index > 2)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((uint)(index) > 2) // (index < 0) || (index > 2)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }

        unsafe public /* blittable */ struct _maxViewportDimensions_e__FixedBuffer
        {
            #region Fields
            public uint e0;

            public uint e1;
            #endregion

            #region Properties
            public uint this[int index]
            {
                get
                {
                    if ((uint)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((uint)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (uint* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }

        unsafe public /* blittable */ struct _viewportBoundsRange_e__FixedBuffer
        {
            #region Fields
            public float e0;

            public float e1;
            #endregion

            #region Properties
            public float this[int index]
            {
                get
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }

        unsafe public /* blittable */ struct _pointSizeRange_e__FixedBuffer
        {
            #region Fields
            public float e0;

            public float e1;
            #endregion

            #region Properties
            public float this[int index]
            {
                get
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }

        unsafe public /* blittable */ struct _lineWidthRange_e__FixedBuffer
        {
            #region Fields
            public float e0;

            public float e1;
            #endregion

            #region Properties
            public float this[int index]
            {
                get
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    if ((float)(index) > 1) // (index < 0) || (index > 1)
                    {
                        ExceptionUtilities.ThrowArgumentOutOfRangeException(nameof(index), index);
                    }

                    fixed (float* e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}
