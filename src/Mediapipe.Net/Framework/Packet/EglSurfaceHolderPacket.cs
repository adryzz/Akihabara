// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

namespace Mediapipe.Net.Framework.Packet
{
    /*
     * defined on Linux, but useful only with OpenGL ES
     * (https://github.com/homuler/MediaPipeUnityPlugin/blob/master/Packages/com.github.homuler.mediapipe/Runtime/Scripts/Framework/Packet/EglSurfaceHolderPacket.cs)
     */
#if OPENGL_ES
    public class EglSurfaceHolderPacket : Packet<EglSurfaceHolder>
    {
        public EglSurfaceHolderPacket() : base() { }

        public EglSurfaceHolderPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public EglSurfaceHolderPacket(EglSurfaceHolder eglSurfaceHolder) : base()
        {
            ng.UnsafeNativeMethods.mp_MakeEglSurfaceHolderUniquePtrPacket__Reshup(eglSurfaceHolder.uniquePtr, out var ptr).Assert();
            eglSurfaceHolder.Dispose(); // respect move semantics
            this.Ptr = ptr;
        }

        public override EglSurfaceHolder Get()
        {
            ng.UnsafeNativeMethods.mp_Packet__GetEglSurfaceHolderUniquePtr(MpPtr, out var eglSurfaceHolderPtr).Assert();

            GC.KeepAlive(this);
            return new EglSurfaceHolder(eglSurfaceHolderPtr, false);
        }

        public override StatusOr<EglSurfaceHolder> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            ng.UnsafeNativeMethods.mp_Packet__ValidateAsEglSurfaceHolderUniquePtr(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
#endif
}
