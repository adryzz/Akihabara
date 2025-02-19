// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using Mediapipe.Net.Native;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Native.Framework;
using UnsafeNativeMethods = Mediapipe.Net.Native.Framework.UnsafeNativeMethods;

namespace Mediapipe.Net.Framework.Packet
{
    public class FloatArrayPacket : Packet<float[]>
    {
        int _Length = -1;

        public int Length {
            get { return _Length; }
            set {
                if (_Length >= 0)
                {
                    throw new InvalidOperationException("Length is already set and cannot be changed");
                }

                _Length = value;
            }
        }

        public FloatArrayPacket() : base() { }

        public FloatArrayPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public FloatArrayPacket(float[] value) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket__Pf_i(value, value.Length, out var ptr).Assert();
            Ptr = ptr;
            Length = value.Length;
        }

        public FloatArrayPacket(float[] value, Timestamp timestamp) : base()
        {
            UnsafeNativeMethods.mp__MakeFloatArrayPacket_At__Pf_i_Rt(value, value.Length, timestamp.MpPtr, out var ptr).Assert();
            GC.KeepAlive(timestamp);
            Ptr = ptr;
            Length = value.Length;
        }

        public override float[] Get()
        {
            if (Length < 0)
            {
                throw new InvalidOperationException("The array's length is unknown, set Length first");
            }

            var result = new float[Length];

            unsafe
            {
                float* src = (float*)GetArrayPtr();

                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = *src++;
                }
            }

            return result;
        }

        public IntPtr GetArrayPtr()
        {
            UnsafeNativeMethods.mp_Packet__GetFloatArray(MpPtr, out var value).Assert();
            GC.KeepAlive(this);
            return value;
        }

        public override StatusOr<float[]> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            UnsafeNativeMethods.mp_Packet__ValidateAsFloatArray(MpPtr, out var statusPtr).Assert();

            GC.KeepAlive(this);
            return new Status(statusPtr);
        }
    }
}
