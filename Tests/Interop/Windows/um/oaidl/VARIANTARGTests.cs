// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace TerraFX.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="VARIANTARG" /> struct.</summary>
    public static class VARIANTARGTests
    {
        /// <summary>Validates that the layout of the <see cref="VARIANTARG" /> struct is <see cref="LayoutKind.Explicit" />.</summary>
        [Test]
        public static void IsLayoutExplicitTest()
        {
            Assert.That(typeof(VARIANTARG).IsExplicitLayout, Is.True);
        }

        /// <summary>Validates that the size of the <see cref="VARIANTARG" /> struct is correct.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(Marshal.SizeOf<VARIANTARG>(), Is.EqualTo(24));
            }
            else
            {
                Assert.That(Marshal.SizeOf<VARIANTARG>(), Is.EqualTo(16));
            }
        }
    }
}
