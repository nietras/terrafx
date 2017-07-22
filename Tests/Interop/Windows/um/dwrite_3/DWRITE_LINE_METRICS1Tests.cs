// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace TerraFX.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="DWRITE_LINE_METRICS1" /> struct.</summary>
    public static class DWRITE_LINE_METRICS1Tests
    {
        /// <summary>Validates that the layout of the <see cref="DWRITE_LINE_METRICS1" /> struct is <see cref="LayoutKind.Sequential" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(DWRITE_LINE_METRICS1).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the size of the <see cref="DWRITE_LINE_METRICS1" /> struct is correct.</summary>
        [Test]
        public static void SizeOfTest()
        {
            Assert.That(Marshal.SizeOf<DWRITE_LINE_METRICS1>(), Is.EqualTo(32));
        }
    }
}
