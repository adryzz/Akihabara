// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Mediapipe.Net.Native.Framework
{
    public partial class SafeNativeMethods : NativeMethods
    {
        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern long mp_Timestamp__Value(IntPtr timestamp);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern double mp_Timestamp__Seconds(IntPtr timestamp);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern long mp_Timestamp__Microseconds(IntPtr timestamp);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsSpecialValue(IntPtr timestamp);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsRangeValue(IntPtr timestamp);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_Timestamp__IsAllowedInStream(IntPtr timestamp);
    }
}
