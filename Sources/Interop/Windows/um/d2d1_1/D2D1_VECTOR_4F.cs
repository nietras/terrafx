// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from um\d2d1_1.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    public /* blittable */ struct D2D1_VECTOR_4F
    {
        #region Fields
        internal D2D_VECTOR_4F _value;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="D2D1_VECTOR_4F" /> struct.</summary>
        /// <param name="value">The <see cref="D2D_VECTOR_4F" /> used to initialize the instance.</param>
        public D2D1_VECTOR_4F(D2D_VECTOR_4F value)
        {
            _value = value;
        }
        #endregion

        #region Operators
        /// <summary>Implicitly converts a <see cref="D2D1_VECTOR_4F" /> value to a <see cref="D2D_VECTOR_4F" /> value.</summary>
        /// <param name="value">The <see cref="D2D1_VECTOR_4F" /> value to convert.</param>
        public static implicit operator D2D_VECTOR_4F(D2D1_VECTOR_4F value)
        {
            return value._value;
        }

        /// <summary>Implicitly converts a <see cref="D2D_VECTOR_4F" /> value to a <see cref="D2D1_VECTOR_4F" /> value.</summary>
        /// <param name="value">The <see cref="D2D_VECTOR_4F" /> value to convert.</param>
        public static implicit operator D2D1_VECTOR_4F(D2D_VECTOR_4F value)
        {
            return new D2D1_VECTOR_4F(value);
        }
        #endregion
    }
}