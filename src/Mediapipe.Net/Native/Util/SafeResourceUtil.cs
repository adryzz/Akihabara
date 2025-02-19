// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native.Util
{
    public partial class SafeNativeMethods : NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string PathResolver(string path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool ResourceProvider(string path, IntPtr output);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalPathResolver__P([MarshalAs(UnmanagedType.FunctionPtr)] PathResolver path);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern void mp__SetCustomGlobalResourceProvider__P([MarshalAs(UnmanagedType.FunctionPtr)] ResourceProvider provider);
    }
}
