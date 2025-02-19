// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using System;
using System.Linq;
using Mediapipe.Net.Core;
using Mediapipe.Net.External;
using Mediapipe.Net.Framework.ImageFormat;
using NUnit.Framework;
using UnmanageUtility;

namespace Mediapipe.Net.Tests.Format
{
    [TestFixture]
    public class ImageFrameTest
    {
        #region Constructor
        [Test, SignalAbort]
        public void Ctor_ShouldInstantiateImageFrame_When_CalledWithNoArguments()
        {
            using (var imageFrame = new ImageFrame())
            {
                Assert.AreEqual(imageFrame.Format(), ImageFormat.Format.Unknown);
                Assert.AreEqual(imageFrame.Width(), 0);
                Assert.AreEqual(imageFrame.Height(), 0);
                Assert.Throws<FormatException>(() => { imageFrame.ChannelSize(); });
                Assert.Throws<FormatException>(() => { imageFrame.NumberOfChannels(); });
                Assert.Throws<FormatException>(() => { imageFrame.ByteDepth(); });
                Assert.AreEqual(imageFrame.WidthStep(), 0);
                Assert.AreEqual(imageFrame.PixelDataSize(), 0);
                Assert.Throws<FormatException>(() => { imageFrame.PixelDataSizeStoredContiguously(); });
                Assert.True(imageFrame.IsEmpty());
                Assert.False(imageFrame.IsContiguous());
                Assert.False(imageFrame.IsAligned(16));
                Assert.AreEqual(imageFrame.MutablePixelData(), IntPtr.Zero);
            }
        }

        [Test]
        public void Ctor_ShouldInstantiateImageFrame_When_CalledWithFormat()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Sbgra, 640, 480))
            {
                Assert.AreEqual(imageFrame.Format(), ImageFormat.Format.Sbgra);
                Assert.AreEqual(imageFrame.Width(), 640);
                Assert.AreEqual(imageFrame.Height(), 480);
                Assert.AreEqual(imageFrame.ChannelSize(), 1);
                Assert.AreEqual(imageFrame.NumberOfChannels(), 4);
                Assert.AreEqual(imageFrame.ByteDepth(), 1);
                Assert.AreEqual(imageFrame.WidthStep(), 640 * 4);
                Assert.AreEqual(imageFrame.PixelDataSize(), 640 * 480 * 4);
                Assert.AreEqual(imageFrame.PixelDataSizeStoredContiguously(), 640 * 480 * 4);
                Assert.False(imageFrame.IsEmpty());
                Assert.True(imageFrame.IsContiguous());
                Assert.True(imageFrame.IsAligned(16));
                Assert.AreNotEqual(imageFrame.MutablePixelData(), IntPtr.Zero);
            }
        }

        [Test]
        public void Ctor_ShouldInstantiateImageFrame_When_CalledWithFormatAndAlignmentBoundary()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray8, 100, 100, 8))
            {
                Assert.AreEqual(imageFrame.Width(), 100);
                Assert.AreEqual(imageFrame.NumberOfChannels(), 1);
                Assert.AreEqual(imageFrame.WidthStep(), 104);
            }
        }

        [Test]
        public void Ctor_ShouldInstantiateImageFrame_When_CalledWithPixelData()
        {
            var pixelData = new UnmanagedArray<byte>(32);
            var srcBytes = new byte[] {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
            };
            pixelData.CopyFrom(srcBytes);

            using (var imageFrame = new ImageFrame(ImageFormat.Format.Sbgra, 4, 2, 16, pixelData))
            {
                Assert.AreEqual(imageFrame.Width(), 4);
                Assert.AreEqual(imageFrame.Height(), 2);
                Assert.False(imageFrame.IsEmpty());

                var bytes = imageFrame.CopyToByteBuffer(32);
                Assert.IsEmpty(bytes.Where((x, i) => x != srcBytes[i]));
            }
        }

        [Test, SignalAbort]
        public void Ctor_ShouldThrowMediaPipeException_When_CalledWithInvalidArgument()
        {
            Assert.Throws<MediapipeException>(() => { new ImageFrame(ImageFormat.Format.Sbgra, 640, 480, 0); });
        }
        #endregion

        #region #isDisposed
        [Test]
        public void isDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            using (var imageFrame = new ImageFrame())
            {
                Assert.False(imageFrame.IsDisposed);
            }
        }

        [Test]
        public void isDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var imageFrame = new ImageFrame();
            imageFrame.Dispose();

            Assert.True(imageFrame.IsDisposed);
        }
        #endregion

        #region #SetToZero
        [Test]
        public void SetToZero_ShouldSetZeroToAllBytes()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray8, 10, 10))
            {
                var origBytes = imageFrame.CopyToByteBuffer(100);

                imageFrame.SetToZero();
                var bytes = imageFrame.CopyToByteBuffer(100);
                Assert.True(bytes.All((x) => x == 0));
            }
        }
        #endregion

        #region #SetAlignmentPaddingAreas
        [Test]
        public void SetAlignmentPaddingAreas_ShouldNotThrow()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray8, 10, 10, 16))
            {
                Assert.DoesNotThrow(() => { imageFrame.SetAlignmentPaddingAreas(); });
            }
        }
        #endregion

        #region CopyToBuffer
        [Test]
        public void CopyToByteBuffer_ShouldReturnByteArray_When_BufferSizeIsLargeEnough()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray8, 10, 10))
            {
                var normalBuffer = imageFrame.CopyToByteBuffer(100);
                var largeBuffer = imageFrame.CopyToByteBuffer(120);

                Assert.IsEmpty(normalBuffer.Where((x, i) => x != largeBuffer[i]));
            }
        }

        [Test, SignalAbort]
        public void CopyToByteBuffer_ShouldThrowException_When_BufferSizeIsTooSmall()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray8, 10, 10))
            {
                Assert.Throws<MediapipeException>(() => { imageFrame.CopyToByteBuffer(99); });
            }
        }

        [Test]
        public void CopyToUshortBuffer_ShouldReturnUshortArray_When_BufferSizeIsLargeEnough()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray16, 10, 10))
            {
                var normalBuffer = imageFrame.CopyToUshortBuffer(100);
                var largeBuffer = imageFrame.CopyToUshortBuffer(120);

                Assert.IsEmpty(normalBuffer.Where((x, i) => x != largeBuffer[i]));
            }
        }

        [Test, SignalAbort]
        public void CopyToUshortBuffer_ShouldThrowException_When_BufferSizeIsTooSmall()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Gray16, 10, 10))
            {
                Assert.Throws<MediapipeException>(() => { imageFrame.CopyToUshortBuffer(99); });
            }
        }

        [Test]
        public void CopyToFloatBuffer_ShouldReturnFloatArray_When_BufferSizeIsLargeEnough()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Vec32F1, 10, 10))
            {
                var normalBuffer = imageFrame.CopyToFloatBuffer(100);
                var largeBuffer = imageFrame.CopyToFloatBuffer(120);

                Assert.IsEmpty(normalBuffer.Where((x, i) => Math.Abs(x - largeBuffer[i]) > 1e-9));
            }
        }

        [Test, SignalAbort]
        public void CopyToFloatBuffer_ShouldThrowException_When_BufferSizeIsTooSmall()
        {
            using (var imageFrame = new ImageFrame(ImageFormat.Format.Vec32F1, 10, 10))
            {
                Assert.Throws<MediapipeException>(() => { imageFrame.CopyToFloatBuffer(99); });
            }
        }
        #endregion
    }
}
