// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

namespace Mediapipe.Net.Gpu
{
#if OPENGL_ES
    public class EglSurfaceHolder : MpResourceHandle
    {
        private UniquePtrHandle uniquePtrHandle;

        public EglSurfaceHolder(IntPtr ptr, bool isOwner = true) : base(isOwner)
        {
            uniquePtrHandle = new UniquePtr(ptr, isOwner);
            this.Ptr = uniquePtrHandle.Get();
        }

        public EglSurfaceHolder() : base()
        {
            UnsafeNativeMethods.mp_EglSurfaceHolderUniquePtr__(out var uniquePtr).Assert();
            uniquePtrHandle = new UniquePtr(uniquePtr);
            this.Ptr = uniquePtrHandle.Get();
        }

        protected override void DisposeManaged()
        {
            if (uniquePtrHandle != null)
            {
                uniquePtrHandle.Dispose();
                uniquePtrHandle = null;
            }
            base.DisposeManaged();
        }

        protected override void DeleteMpPtr()
        {
            // Do nothing
        }

        public IntPtr uniquePtr
        {
            get { return uniquePtrHandle == null ? IntPtr.Zero : uniquePtrHandle.MpPtr; }
        }

        public bool FlipY()
        {
            return SafeNativeMethods.mp_EglSurfaceHolder__flip_y(MpPtr);
        }

        public void SetFlipY(bool flipY)
        {
            SafeNativeMethods.mp_EglSurfaceHolder__SetFlipY__b(MpPtr, flipY);
            GC.KeepAlive(this);
        }

        public void SetSurface(IntPtr eglSurface, GlContext glContext)
        {
            UnsafeNativeMethods.mp_EglSurfaceHolder__SetSurface__P_Pgc(MpPtr, eglSurface, glContext.MpPtr, out var statusPtr).Assert();
        }

        private class UniquePtr : UniquePtrHandle
        {
            public UniquePtr(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

            protected override void DeleteMpPtr()
            {
                UnsafeNativeMethods.mp_EglSurfaceHolderUniquePtr__delete(Ptr);
            }

            public override IntPtr Get()
            {
                return SafeNativeMethods.mp_EglSurfaceHolderUniquePtr__get(MpPtr);
            }

            public override IntPtr Release()
            {
                return SafeNativeMethods.mp_EglSurfaceHolderUniquePtr__release(MpPtr);
            }
        }
    }
#endif
}
