// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Mediapipe.Net.Gpu;

namespace Mediapipe.Net.Native.Gpu
{
    public partial class SafeNativeMethods : NativeMethods
    {
        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern IntPtr mp_GpuBuffer__GetGlTextureBufferSharedPtr(IntPtr gpuBuffer);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_GpuBuffer__width(IntPtr gpuBuffer);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern int mp_GpuBuffer__height(IntPtr gpuBuffer);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        public static extern GpuBufferFormat mp_GpuBuffer__format(IntPtr gpuBuffer);

        [Pure, DllImport(MediaPipeLibrary, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool mp_StatusOrGpuBuffer__ok(IntPtr statusOrGpuBuffer);
    }
}
