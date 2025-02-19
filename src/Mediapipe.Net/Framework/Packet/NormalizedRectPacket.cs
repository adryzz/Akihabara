// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using Mediapipe.Net.Framework.Port;
using Mediapipe.Net.Framework.Protobuf;
using Mediapipe.Net.Native;

namespace Mediapipe.Net.Framework.Packet
{
    public class NormalizedRectPacket : Packet<NormalizedRect>
    {
        public NormalizedRectPacket() : base() { }
        public NormalizedRectPacket(IntPtr ptr, bool isOwner = true) : base(ptr, isOwner) { }

        public override NormalizedRect Get()
        {
            UnsafeNativeMethods.mp_Packet__GetNormalizedRect(MpPtr, out var serializedProtoPtr).Assert();
            GC.KeepAlive(this);

            var normalizedRect = External.Protobuf.DeserializeProto<NormalizedRect>(serializedProtoPtr, NormalizedRect.Parser);
            UnsafeNativeMethods.mp_api_SerializedProto__delete(serializedProtoPtr);

            return normalizedRect;
        }

        public override StatusOr<NormalizedRect> Consume()
        {
            throw new NotSupportedException();
        }

        public override Status ValidateAsType()
        {
            throw new NotImplementedException();
        }
    }
}
