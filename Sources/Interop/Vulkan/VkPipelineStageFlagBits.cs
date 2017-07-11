// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from src\spec\vk.xml in the Vulkan-Docs repository for tag v1.0.51-core
// Original source is Copyright © 2015-2017 The Khronos Group Inc.

using System;

namespace TerraFX.Interop
{
    [Flags]
    public enum VkPipelineStageFlagBits : uint
    {
        VK_PIPELINE_STAGE_TOP_OF_PIPE_BIT = 0x00000001,

        VK_PIPELINE_STAGE_DRAW_INDIRECT_BIT = 0x00000002,

        VK_PIPELINE_STAGE_VERTEX_INPUT_BIT = 0x00000004,

        VK_PIPELINE_STAGE_VERTEX_SHADER_BIT = 0x00000008,

        VK_PIPELINE_STAGE_TESSELLATION_CONTROL_SHADER_BIT = 0x00000010,

        VK_PIPELINE_STAGE_TESSELLATION_EVALUATION_SHADER_BIT = 0x00000020,

        VK_PIPELINE_STAGE_GEOMETRY_SHADER_BIT = 0x00000040,

        VK_PIPELINE_STAGE_FRAGMENT_SHADER_BIT = 0x00000080,

        VK_PIPELINE_STAGE_EARLY_FRAGMENT_TESTS_BIT = 0x00000100,

        VK_PIPELINE_STAGE_LATE_FRAGMENT_TESTS_BIT = 0x00000200,

        VK_PIPELINE_STAGE_COLOR_ATTACHMENT_OUTPUT_BIT = 0x00000400,

        VK_PIPELINE_STAGE_COMPUTE_SHADER_BIT = 0x00000800,

        VK_PIPELINE_STAGE_TRANSFER_BIT = 0x00001000,

        VK_PIPELINE_STAGE_BOTTOM_OF_PIPE_BIT = 0x00002000,

        VK_PIPELINE_STAGE_HOST_BIT = 0x00004000,

        VK_PIPELINE_STAGE_ALL_GRAPHICS_BIT = 0x00008000,

        VK_PIPELINE_STAGE_ALL_COMMANDS_BIT = 0x00010000,

        VK_PIPELINE_STAGE_COMMAND_PROCESS_BIT_NVX = 0x00020000,

        VK_PIPELINE_STAGE_FLAG_BITS_MAX_ENUM = 0x7FFFFFFF
    }
}
