// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from src\spec\vk.xml in the Vulkan-Docs repository for tag v1.0.51-core
// Original source is Copyright © 2015-2017 The Khronos Group Inc.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TerraFX.Interop
{
    public static unsafe partial class Vulkan
    {
        #region Constants
        public const uint VK_KHR_mir_surface = 1;

        public const uint VK_KHR_MIR_SURFACE_SPEC_VERSION = 4;

        public const string VK_KHR_MIR_SURFACE_EXTENSION_NAME = "VK_KHR_mir_surface";
        #endregion

        #region External Methods
        [DllImport("Vulkan-1", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, EntryPoint = "vkCreateMirSurfaceKHR", ExactSpelling = true, PreserveSig = true, SetLastError = false, ThrowOnUnmappableChar = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern VkResult vkCreateMirSurfaceKHR(
            [In, ComAliasName("VkInstance")] IntPtr instance,
            [In] VkMirSurfaceCreateInfoKHR* pCreateInfo,
            [In, Optional] VkAllocationCallbacks* pAllocator,
            [Out, ComAliasName("VkSurfaceKHR")] IntPtr* pSurface
        );

        [DllImport("Vulkan-1", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, EntryPoint = "vkGetPhysicalDeviceMirPresentationSupportKHR", ExactSpelling = true, PreserveSig = true, SetLastError = false, ThrowOnUnmappableChar = false)]
        [SuppressUnmanagedCodeSecurity]
        [return: ComAliasName("VkBool32")]
        public static extern uint vkGetPhysicalDeviceMirPresentationSupportKHR(
            [In, ComAliasName("VkPhysicalDevice")] IntPtr physicalDevice,
            [In] uint queueFamilyIndex,
            [In, ComAliasName("MirConnection")] IntPtr connection
        );
        #endregion
    }
}