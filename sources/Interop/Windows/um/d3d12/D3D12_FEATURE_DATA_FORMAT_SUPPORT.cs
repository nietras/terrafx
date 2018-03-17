// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um\d3d12.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    public /* blittable */ struct D3D12_FEATURE_DATA_FORMAT_SUPPORT
    {
        #region Fields
        public DXGI_FORMAT Format;

        public D3D12_FORMAT_SUPPORT1 Support1;

        public D3D12_FORMAT_SUPPORT2 Support2;
        #endregion
    }
}