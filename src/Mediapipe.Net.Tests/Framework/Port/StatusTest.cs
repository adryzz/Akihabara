// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using Mediapipe.Net.Core;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class StatusTest
    {
        #region #code
        [Test]
        public void code_ShouldReturnStatusCode_When_StatusIsOk()
        {
            var status = Status.Ok();

            Assert.AreEqual(status.Code, Status.StatusCode.Ok);
        }

        [Test]
        public void code_ShouldReturnStatusCode_When_StatusIsFailedPrecondition()
        {
            var status = Status.FailedPrecondition();

            Assert.AreEqual(status.Code, Status.StatusCode.FailedPrecondition);
        }
        #endregion

        #region #isDisposed
        [Test]
        public void isDisposed_ShouldReturnFalse_When_NotDisposedYet()
        {
            var status = Status.Ok();

            Assert.False(status.IsDisposed);
        }

        [Test]
        public void isDisposed_ShouldReturnTrue_When_AlreadyDisposed()
        {
            var status = Status.Ok();
            status.Dispose();

            Assert.True(status.IsDisposed);
        }
        #endregion

        #region #rawCode
        [Test]
        public void rawCode_ShouldReturnRawCode_When_StatusIsOk()
        {
            var status = Status.Ok();

            Assert.AreEqual(status.RawCode, 0);
        }

        [Test]
        public void rawCode_ShouldReturnRawCode_When_StatusIsFailedPrecondition()
        {
            var status = Status.FailedPrecondition();

            Assert.AreEqual(status.RawCode, 9);
        }
        #endregion

        #region #ok
        [Test]
        public void IsOk_ShouldReturnTrue_When_StatusIsOk()
        {
            var status = Status.Ok();

            Assert.True(status.ok);
        }

        [Test]
        public void IsOk_ShouldReturnFalse_When_StatusIsFailedPrecondition()
        {
            var status = Status.FailedPrecondition();

            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"status.Code: {status.Code}");
            Console.WriteLine($"status.RawCode: {status.RawCode}");
            Console.WriteLine($"status.ok: {status.ok}");
            Console.WriteLine("HOW IS IT LITERALLY CONTRADICTING ITSELF");

            Assert.False(status.ok);
        }
        #endregion

        #region #AssertOk
        [Test]
        public void AssertOk_ShouldNotThrow_When_StatusIsOk()
        {
            var status = Status.Ok();

            Assert.DoesNotThrow(() => { status.AssertOk(); });
        }

        public void AssertOk_ShouldThrow_When_StatusIsNotOk()
        {
            var status = Status.FailedPrecondition();

            Assert.Throws<MediapipeException>(() => { status.AssertOk(); });
        }
        #endregion

        #region #ToString
        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsOk()
        {
            var status = Status.Ok();

            Assert.AreEqual(status.ToString(), "OK");
        }

        [Test]
        public void ToString_ShouldReturnMessage_When_StatusIsFailedPrecondition()
        {
            var message = "Some error";
            var status = Status.FailedPrecondition(message);

            Assert.AreEqual(status.ToString(), $"FAILED_PRECONDITION: {message}");
        }
        #endregion
    }
}
