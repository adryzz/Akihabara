// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using Mediapipe.Net.Core;
using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    [TestFixture]
    public class FloatPacketTest
    {
        #region Constructor
        [Test, SignalAbort]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithNoArguments()
        {
            var packet = new FloatPacket();

            Assert.AreEqual(packet.ValidateAsType().Code, Status.StatusCode.Internal);
            Assert.Throws<MediapipeException>(() => { packet.Get(); });
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValue()
        {
            var packet = new FloatPacket(0.01f);

            Assert.True(packet.ValidateAsType().ok);
            Assert.AreEqual(packet.Get(), 0.01f);
            Assert.AreEqual(packet.Timestamp(), Timestamp.Unset());
        }

        [Test]
        public void Ctor_ShouldInstantiatePacket_When_CalledWithValueAndTimestamp()
        {
            var timestamp = new Timestamp(1);
            var packet = new FloatPacket(0.01f, timestamp);

            Assert.True(packet.ValidateAsType().ok);
            Assert.AreEqual(packet.Get(), 0.01f);
            Assert.AreEqual(packet.Timestamp(), timestamp);
        }
        #endregion

        #region #isDisposed
        [Test]
        public void IsDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            var packet = new FloatPacket();

            Assert.False(packet.IsDisposed);
        }

        [Test]
        public void IsDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var packet = new FloatPacket();
            packet.Dispose();

            Assert.True(packet.IsDisposed);
        }
        #endregion

        #region #Consume
        [Test]
        public void Consume_ShouldThrowNotSupportedException()
        {
            var packet = new FloatPacket();

            Assert.Throws<NotSupportedException>(() => { packet.Consume(); });
        }
        #endregion

        #region #DebugTypeName
        [Test]
        public void DebugTypeName_ShouldReturnFloat_When_ValueIsSet()
        {
            var packet = new FloatPacket(0.01f);

            Assert.AreEqual(packet.DebugTypeName(), "float");
        }
        #endregion
    }
}
