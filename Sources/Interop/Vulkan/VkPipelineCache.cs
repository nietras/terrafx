// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from src\spec\vk.xml in the Vulkan-Docs repository for tag v1.0.51-core
// Original source is Copyright © 2015-2017 The Khronos Group Inc.

using System;

namespace TerraFX.Interop
{
    unsafe public /* blittable */ struct VkPipelineCache : IEquatable<VkPipelineCache>, IFormattable
    {
        #region Fields
        internal ulong _value;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="VkPipelineCache" /> struct.</summary>
        /// <param name="value">The <see cref="ulong" /> used to initialize the instance.</param>
        public VkPipelineCache(ulong value)
        {
            _value = value;
        }
        #endregion

        #region Comparison Operators
        /// <summary>Compares two <see cref="VkPipelineCache" /> instances to determine equality.</summary>
        /// <param name="left">The <see cref="VkPipelineCache" /> to compare with <paramref name="right" />.</param>
        /// <param name="right">The <see cref="VkPipelineCache" /> to compare with <paramref name="left" />.</param>
        /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(VkPipelineCache left, VkPipelineCache right)
        {
            return (left._value == right._value);
        }

        /// <summary>Compares two <see cref="VkPipelineCache" /> instances to determine inequality.</summary>
        /// <param name="left">The <see cref="VkPipelineCache" /> to compare with <paramref name="right" />.</param>
        /// <param name="right">The <see cref="VkPipelineCache" /> to compare with <paramref name="left" />.</param>
        /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(VkPipelineCache left, VkPipelineCache right)
        {
            return (left._value != right._value);
        }
        #endregion

        #region Cast Operators
        /// <summary>Implicitly converts a <see cref="VkPipelineCache" /> value to a <see cref="ulong" /> value.</summary>
        /// <param name="value">The <see cref="VkPipelineCache" /> value to convert.</param>
        public static implicit operator ulong(VkPipelineCache value)
        {
            return value._value;
        }

        /// <summary>Implicitly converts a <see cref="ulong" /> value to a <see cref="VkPipelineCache" /> value.</summary>
        /// <param name="value">The <see cref="ulong" /> value to convert.</param>
        public static implicit operator VkPipelineCache(ulong value)
        {
            return new VkPipelineCache(value);
        }
        #endregion

        #region System.IEquatable<VkPipelineCache> Methods
        /// <summary>Compares a <see cref="VkPipelineCache" /> with the current instance to determine equality.</summary>
        /// <param name="other">The <see cref="VkPipelineCache" /> to compare with the current instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is equal to the current instance; otherwise, <c>false</c>.</returns>
        public bool Equals(VkPipelineCache other)
        {
            var otherValue = other._value;
            return _value.Equals(otherValue);
        }
        #endregion

        #region System.IFormattable Methods
        /// <summary>Converts the current instance to an equivalent <see cref="string" /> value.</summary>
        /// <param name="format">The format to use or <c>null</c> to use the default format.</param>
        /// <param name="formatProvider">The provider to use when formatting the current instance or <c>null</c> to use the default provider.</param>
        /// <returns>An equivalent <see cref="string" /> value for the current instance.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }
        #endregion

        #region System.Object Methods
        /// <summary>Compares a <see cref="object" /> with the current instance to determine equality.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with the current instance.</param>
        /// <returns><c>true</c> if <paramref name="obj" /> is an instance of <see cref="VkPipelineCache" /> and is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return (obj is VkPipelineCache other)
                && Equals(other);
        }

        /// <summary>Gets a hash code for the current instance.</summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>Converts the current instance to an equivalent <see cref="string" /> value.</summary>
        /// <returns>An equivalent <see cref="string" /> value for the current instance.</returns>
        public override string ToString()
        {
            return _value.ToString();
        }
        #endregion
    }
}
