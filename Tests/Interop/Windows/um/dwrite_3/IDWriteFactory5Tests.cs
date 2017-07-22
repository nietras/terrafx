// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using static TerraFX.Interop.DWrite;

namespace TerraFX.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="IDWriteFactory5" /> struct.</summary>
    public static class IDWriteFactory5Tests
    {
        /// <summary>Validates that the <see cref="Guid" /> of the <see cref="IDWriteFactory5" /> struct is correct.</summary>
        [Test]
        public static void GuidOfTest()
        {
            Assert.That(typeof(IDWriteFactory5).GUID, Is.EqualTo(IID_IDWriteFactory5));
        }

        /// <summary>Validates that the layout of the <see cref="IDWriteFactory5" /> struct is <see cref="LayoutKind.Sequential" />.</summary>
        [Test]
        public static void IsLayoutSequentialTest()
        {
            Assert.That(typeof(IDWriteFactory5).IsLayoutSequential, Is.True);
        }

        /// <summary>Validates that the size of the <see cref="IDWriteFactory5" /> struct is correct.</summary>
        [Test]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.That(Marshal.SizeOf<IDWriteFactory5>(), Is.EqualTo(8));
            }
            else
            {
                Assert.That(Marshal.SizeOf<IDWriteFactory5>(), Is.EqualTo(4));
            }
        }

        /// <summary>Provides validation of the <see cref="IDWriteFactory5.Vtbl" /> struct.</summary>
        public static class VtblTests
        {
            /// <summary>Validates that the layout of the <see cref="IDWriteFactory5" /> struct is <see cref="LayoutKind.Sequential" />.</summary>
            [Test]
            public static void IsLayoutSequentialTest()
            {
                Assert.That(typeof(IDWriteFactory5.Vtbl).IsLayoutSequential, Is.True);
            }

            /// <summary>Validates that the size of the <see cref="IDWriteFactory5" /> struct is correct.</summary>
            [Test]
            public static void SizeOfTest()
            {
                if (Environment.Is64BitProcess)
                {
                    Assert.That(Marshal.SizeOf<IDWriteFactory5.Vtbl>(), Is.EqualTo(384));
                }
                else
                {
                    Assert.That(Marshal.SizeOf<IDWriteFactory5.Vtbl>(), Is.EqualTo(192));
                }
            }
        }
    }
}
