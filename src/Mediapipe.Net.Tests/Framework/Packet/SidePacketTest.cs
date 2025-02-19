// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using Mediapipe.Net.Framework.Packet;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Framework.Packet
{
    [TestFixture]
    public class SidePacketTest
    {
        #region #Size
        [Test]
        public void size_ShouldReturnZero_When_Initialized()
        {
            var sidePacket = new SidePacket();

            Assert.AreEqual(sidePacket.Size, 0);
        }

        [Test]
        public void size_ShouldReturnSize_When_AfterPacketsAreEmplaced()
        {
            var sidePacket = new SidePacket();
            var flagPacket = new BoolPacket(true);
            var valuePacket = new FloatPacket(1.0f);

            sidePacket.Emplace("flag", flagPacket);
            sidePacket.Emplace("value", valuePacket);

            Assert.AreEqual(sidePacket.Size, 2);
        }
        #endregion

        #region #Emplace
        [Test]
        public void Emplace_ShouldInsertAndDisposePacket()
        {
            var sidePacket = new SidePacket();
            Assert.AreEqual(sidePacket.Size, 0);
            Assert.IsNull(sidePacket.At<FloatPacket>("value"));

            var flagPacket = new FloatPacket(1.0f);
            sidePacket.Emplace("value", flagPacket);

            Assert.AreEqual(sidePacket.Size, 1);
            Assert.AreEqual(sidePacket.At<FloatPacket>("value").Get(), 1.0f);
            Assert.True(flagPacket.IsDisposed);
        }

        [Test]
        public void Emplace_ShouldIgnoreValue_When_KeyExists()
        {
            var sidePacket = new SidePacket();

            var oldValuePacket = new FloatPacket(1.0f);
            sidePacket.Emplace("value", oldValuePacket);
            Assert.AreEqual(sidePacket.At<FloatPacket>("value").Get(), 1.0f);

            var newValuePacket = new FloatPacket(2.0f);
            sidePacket.Emplace("value", newValuePacket);
            Assert.AreEqual(sidePacket.At<FloatPacket>("value").Get(), 1.0f);
        }
        #endregion

        #region #Erase
        [Test]
        public void Erase_ShouldDoNothing_When_KeyDoesNotExist()
        {
            var sidePacket = new SidePacket();
            sidePacket.Erase("value");

            Assert.AreEqual(sidePacket.Size, 0);
        }

        [Test]
        public void Erase_ShouldEraseKey_When_KeyExists()
        {
            var sidePacket = new SidePacket();
            sidePacket.Emplace("value", new BoolPacket(true));
            Assert.AreEqual(sidePacket.Size, 1);

            sidePacket.Erase("value");
            Assert.AreEqual(sidePacket.Size, 0);
        }
        #endregion

        #region #Clear
        [Test]
        public void Clear_ShouldDoNothing_When_SizeIsZero()
        {
            var sidePacket = new SidePacket();
            sidePacket.Clear();

            Assert.AreEqual(sidePacket.Size, 0);
        }

        [Test]
        public void Clear_ShouldClearAllKeys_When_SizeIsNotZero()
        {
            var sidePacket = new SidePacket();
            sidePacket.Emplace("flag", new BoolPacket(true));
            sidePacket.Emplace("value", new FloatPacket(1.0f));
            Assert.AreEqual(sidePacket.Size, 2);

            sidePacket.Clear();
            Assert.AreEqual(sidePacket.Size, 0);
        }
        #endregion
    }
}
