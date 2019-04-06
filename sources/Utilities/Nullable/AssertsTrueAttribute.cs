// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

namespace System.Runtime.CompilerServices
{
    /// <summary>Defines an attribute which specifies the attached parameter is validated to be true.</summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class AssertsTrueAttribute : Attribute
    {
    }
}
