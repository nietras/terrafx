// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um\dwrite.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;
using static TerraFX.Utilities.InteropUtilities;

namespace TerraFX.Interop
{
    /// <summary>The IDWriteFontFamily interface represents a set of fonts that share the same design but are differentiated by weight, stretch, and style.</summary>
    [Guid("DA20D8EF-812A-4C43-9802-62EC4ABD7ADD")]
    public /* blittable */ unsafe struct IDWriteFontFamily
    {
        #region Fields
        public readonly Vtbl* lpVtbl;
        #endregion

        #region IUnknown Delegates
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _QueryInterface(
            [In] IDWriteFontFamily* This,
            [In, ComAliasName("REFIID")] Guid* riid,
            [Out] void** ppvObject
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("ULONG")]
        public /* static */ delegate uint _AddRef(
            [In] IDWriteFontFamily* This
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("ULONG")]
        public /* static */ delegate uint _Release(
            [In] IDWriteFontFamily* This
        );
        #endregion

        #region IDWriteFontList Delegates
        /// <summary>Gets the font collection that contains the fonts.</summary>
        /// <param name="fontCollection">Receives a pointer to the font collection object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _GetFontCollection(
            [In] IDWriteFontFamily* This,
            [Out] IDWriteFontCollection** fontCollection
        );

        /// <summary>Gets the number of fonts in the font list.</summary>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("UINT32")]
        public /* static */ delegate uint _GetFontCount(
            [In] IDWriteFontFamily* This
        );

        /// <summary>Gets a font given its zero-based index.</summary>
        /// <param name="index">Zero-based index of the font in the font list.</param>
        /// <param name="font">Receives a pointer to the newly created font object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _GetFont(
            [In] IDWriteFontFamily* This,
            [In, ComAliasName("UINT32")] uint index,
            [Out] IDWriteFont** font
        );
        #endregion

        #region Delegates
        /// <summary>Creates a localized strings object that contains the family names for the font family, indexed by locale name.</summary>
        /// <param name="names">Receives a pointer to the newly created localized strings object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _GetFamilyNames(
            [In] IDWriteFontFamily* This,
            [Out] IDWriteLocalizedStrings** names
        );

        /// <summary>Gets the font that best matches the specified properties.</summary>
        /// <param name="weight">Requested font weight.</param>
        /// <param name="stretch">Requested font stretch.</param>
        /// <param name="style">Requested font style.</param>
        /// <param name="matchingFont">Receives a pointer to the newly created font object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _GetFirstMatchingFont(
            [In] IDWriteFontFamily* This,
            [In] DWRITE_FONT_WEIGHT weight,
            [In] DWRITE_FONT_STRETCH stretch,
            [In] DWRITE_FONT_STYLE style,
            [Out] IDWriteFont** matchingFont
        );

        /// <summary>Gets a list of fonts in the font family ranked in order of how well they match the specified properties.</summary>
        /// <param name="weight">Requested font weight.</param>
        /// <param name="stretch">Requested font stretch.</param>
        /// <param name="style">Requested font style.</param>
        /// <param name="matchingFonts">Receives a pointer to the newly created font list object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int _GetMatchingFonts(
            [In] IDWriteFontFamily* This,
            [In] DWRITE_FONT_WEIGHT weight,
            [In] DWRITE_FONT_STRETCH stretch,
            [In] DWRITE_FONT_STYLE style,
            [Out] IDWriteFontList** matchingFonts
        );
        #endregion

        #region IUnknown Methods
        [return: ComAliasName("HRESULT")]
        public int QueryInterface(
            [In, ComAliasName("REFIID")] Guid* riid,
            [Out] void** ppvObject
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_QueryInterface>(lpVtbl->QueryInterface)(
                    This,
                    riid,
                    ppvObject
                );
            }
        }

        [return: ComAliasName("ULONG")]
        public uint AddRef()
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_AddRef>(lpVtbl->AddRef)(
                    This
                );
            }
        }

        [return: ComAliasName("ULONG")]
        public uint Release()
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_Release>(lpVtbl->Release)(
                    This
                );
            }
        }
        #endregion

        #region IDWriteFontList Methods
        [return: ComAliasName("HRESULT")]
        public int GetFontCollection(
            [Out] IDWriteFontCollection** fontCollection
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetFontCollection>(lpVtbl->GetFontCollection)(
                    This,
                    fontCollection
                );
            }
        }

        [return: ComAliasName("UINT32")]
        public uint GetFontCount()
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetFontCount>(lpVtbl->GetFontCount)(
                    This
                );
            }
        }

        [return: ComAliasName("HRESULT")]
        public int GetFont(
            [In, ComAliasName("UINT32")] uint index,
            [Out] IDWriteFont** font
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetFont>(lpVtbl->GetFont)(
                    This,
                    index,
                    font
                );
            }
        }
        #endregion

        #region Methods
        [return: ComAliasName("HRESULT")]
        public int GetFamilyNames(
            [Out] IDWriteLocalizedStrings** names
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetFamilyNames>(lpVtbl->GetFamilyNames)(
                    This,
                    names
                );
            }
        }

        [return: ComAliasName("HRESULT")]
        public int GetFirstMatchingFont(
            [In] DWRITE_FONT_WEIGHT weight,
            [In] DWRITE_FONT_STRETCH stretch,
            [In] DWRITE_FONT_STYLE style,
            [Out] IDWriteFont** matchingFont
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetFirstMatchingFont>(lpVtbl->GetFirstMatchingFont)(
                    This,
                    weight,
                    stretch,
                    style,
                    matchingFont
                );
            }
        }

        [return: ComAliasName("HRESULT")]
        public int GetMatchingFonts(
            [In] DWRITE_FONT_WEIGHT weight,
            [In] DWRITE_FONT_STRETCH stretch,
            [In] DWRITE_FONT_STYLE style,
            [Out] IDWriteFontList** matchingFonts
        )
        {
            fixed (IDWriteFontFamily* This = &this)
            {
                return MarshalFunction<_GetMatchingFonts>(lpVtbl->GetMatchingFonts)(
                    This,
                    weight,
                    stretch,
                    style,
                    matchingFonts
                );
            }
        }
        #endregion

        #region Structs
        public /* blittable */ struct Vtbl
        {
            #region IUnknown Fields
            public IntPtr QueryInterface;

            public IntPtr AddRef;

            public IntPtr Release;
            #endregion

            #region IDWriteFontList Fields
            public IntPtr GetFontCollection;

            public IntPtr GetFontCount;

            public IntPtr GetFont;
            #endregion

            #region Fields
            public IntPtr GetFamilyNames;

            public IntPtr GetFirstMatchingFont;

            public IntPtr GetMatchingFonts;
            #endregion
        }
        #endregion
    }
}
