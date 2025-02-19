// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Runtime.InteropServices;
using Mediapipe.Net.Framework.ImageFormat;

namespace Mediapipe.Net.Native.Framework.Format
{
    public partial class UnsafeNativeMethods : NativeMethods
    {
        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__(out IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ui_i_i_ui(
            ImageFormat.Format format, int width, int height, uint alignmentBoundary, out IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__ui_i_i_i_Pui8_PF(
            ImageFormat.Format format, int width, int height, int widthStep, IntPtr pixelData,
            [MarshalAs(UnmanagedType.FunctionPtr)] ImageFrame.Deleter deleter, out IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern void mp_ImageFrame__delete(IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__SetToZero(IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__SetAlignmentPaddingAreas(IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pui8_i(IntPtr imageFrame, IntPtr buffer,
            int bufferSize);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pui16_i(IntPtr imageFrame, IntPtr buffer,
            int bufferSize);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_ImageFrame__CopyToBuffer__Pf_i(IntPtr imageFrame, IntPtr buffer,
            int bufferSize);

        #region StatusOr

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern void mp_StatusOrImageFrame__delete(IntPtr statusOrImageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_StatusOrImageFrame__status(IntPtr statusOrImageFrame, out IntPtr status);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode
            mp_StatusOrImageFrame__value(IntPtr statusOrImageFrame, out IntPtr imageFrame);

        #endregion

        #region Packet

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeImageFramePacket__Pif(IntPtr imageFrame, out IntPtr packet);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp__MakeImageFramePacket_At__Pif_Rt(IntPtr imageFrame, IntPtr timestamp,
            out IntPtr packet);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ConsumeImageFrame(IntPtr packet, out IntPtr statusOrImageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__GetImageFrame(IntPtr packet, out IntPtr imageFrame);

        [DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern MpReturnCode mp_Packet__ValidateAsImageFrame(IntPtr packet, out IntPtr status);

        #endregion
    }
}
