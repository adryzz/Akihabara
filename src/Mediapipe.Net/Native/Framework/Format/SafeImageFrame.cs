// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Mediapipe.Net.Framework.ImageFormat;

namespace Mediapipe.Net.Native.Framework.Format
{
    public partial class SafeNativeMethods : NativeMethods
    {
        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_ImageFrame__IsEmpty(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_ImageFrame__IsContiguous(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__IsAligned__ui(
            IntPtr imageFrame, uint alignmentBoundary, [MarshalAs(UnmanagedType.I1)] out bool value);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern ImageFormat.Format mp_ImageFrame__Format(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_ImageFrame__Width(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_ImageFrame__Height(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ChannelSize(IntPtr imageFrame, out int value);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__NumberOfChannels(IntPtr imageFrame, out int value);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ByteDepth(IntPtr imageFrame, out int value);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_ImageFrame__WidthStep(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern IntPtr mp_ImageFrame__MutablePixelData(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_ImageFrame__PixelDataSize(IntPtr imageFrame);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__PixelDataSizeStoredContiguously(IntPtr imageFrame,
            out int value);

        #region StatusOr

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_StatusOrImageFrame__ok(IntPtr statusOrImageFrame);

        #endregion
    }
}
