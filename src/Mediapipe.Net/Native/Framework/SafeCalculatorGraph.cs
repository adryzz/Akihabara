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
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__HasError(IntPtr graph);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__HasInputStream__PKc(IntPtr graph, string name);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__GraphInputStreamsClosed(IntPtr graph);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__IsNodeThrottled__i(IntPtr graph, int nodeId);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_CalculatorGraph__UnthrottleSources(IntPtr graph);
    }
}
