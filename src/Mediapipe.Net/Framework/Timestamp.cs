// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using Mediapipe.Net.Native;
using Mediapipe.Net.Core;
using Mediapipe.Net.Native.Framework;
using SafeNativeMethods = Mediapipe.Net.Native.Framework.SafeNativeMethods;
using UnsafeNativeMethods = Mediapipe.Net.Native.Framework.UnsafeNativeMethods;

namespace Mediapipe.Net.Framework
{
    public class Timestamp : MpResourceHandle, IEquatable<Timestamp>
    {
        public Timestamp(IntPtr ptr) : base(ptr)
        {
        }

        public Timestamp(long val) : base()
        {
            UnsafeNativeMethods.mp_Timestamp__l(val, out var ptr);
            Ptr = ptr;
        }

        protected override void DeleteMpPtr()
        {
            UnsafeNativeMethods.mp_Timestamp__delete(Ptr);
        }

        #region IEquatable<Timestamp>
        public bool Equals(Timestamp other)
        {
            if (other == null) { return false; }

            return Microseconds() == other.Microseconds();
        }

        public override bool Equals(object obj)
        {
            Timestamp timestampObj = obj == null ? null : (obj as Timestamp);

            return timestampObj != null && Equals(timestampObj);
        }

        public static bool operator ==(Timestamp x, Timestamp y)
        {
            if (((object)x) == null || ((object)y) == null)
            {
                return Object.Equals(x, y);
            }

            return x.Equals(y);
        }

        public static bool operator !=(Timestamp x, Timestamp y)
        {
            if (((object)x) == null || ((object)y) == null)
            {
                return !Object.Equals(x, y);
            }

            return !(x.Equals(y));
        }

        public override int GetHashCode()
        {
            return Microseconds().GetHashCode();
        }
        #endregion

        public long Value() => SafeNativeMethods.mp_Timestamp__Value(MpPtr);
        public double Seconds() => SafeNativeMethods.mp_Timestamp__Seconds(MpPtr);
        public long Microseconds() => SafeNativeMethods.mp_Timestamp__Microseconds(MpPtr);
        public bool IsSpecialValue() => SafeNativeMethods.mp_Timestamp__IsSpecialValue(MpPtr);
        public bool IsRangeValue() => SafeNativeMethods.mp_Timestamp__IsRangeValue(MpPtr);
        public bool IsAllowedInStream() => SafeNativeMethods.mp_Timestamp__IsAllowedInStream(MpPtr);
        public string DebugString() => MarshalStringFromNative(UnsafeNativeMethods.mp_Timestamp__DebugString);

        public Timestamp NextAllowedInStream()
        {
            UnsafeNativeMethods.mp_Timestamp__NextAllowedInStream(MpPtr, out var nextPtr).Assert();

            GC.KeepAlive(this);
            return new Timestamp(nextPtr);
        }

        public Timestamp PreviousAllowedInStream()
        {
            UnsafeNativeMethods.mp_Timestamp__PreviousAllowedInStream(MpPtr, out var prevPtr).Assert();

            GC.KeepAlive(this);
            return new Timestamp(prevPtr);
        }

        public static Timestamp FromSeconds(double seconds)
        {
            UnsafeNativeMethods.mp_Timestamp_FromSeconds__d(seconds, out var ptr).Assert();

            return new Timestamp(ptr);
        }

        #region Special Values

        public static Timestamp Unset()
        {
            UnsafeNativeMethods.mp_Timestamp_Unset(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp Unstarted()
        {
            UnsafeNativeMethods.mp_Timestamp_Unstarted(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp PreStream()
        {
            UnsafeNativeMethods.mp_Timestamp_PreStream(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp Min()
        {
            UnsafeNativeMethods.mp_Timestamp_Min(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp Max()
        {
            UnsafeNativeMethods.mp_Timestamp_Max(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp PostStream()
        {
            UnsafeNativeMethods.mp_Timestamp_PostStream(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp OneOverPostStream()
        {
            UnsafeNativeMethods.mp_Timestamp_OneOverPostStream(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        public static Timestamp Done()
        {
            UnsafeNativeMethods.mp_Timestamp_Done(out var ptr).Assert();

            return new Timestamp(ptr);
        }

        #endregion
    }
}
