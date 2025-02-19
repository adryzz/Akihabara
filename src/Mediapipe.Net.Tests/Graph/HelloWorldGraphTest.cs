// Copyright (c) homuler & The Vignette Authors. Licensed under the MIT license.
// See the LICENSE file in the repository root for more details.

using Mediapipe.Net.Framework;
using Mediapipe.Net.Framework.Packet;
using Mediapipe.Net.Framework.Port;
using NUnit.Framework;

namespace Mediapipe.Net.Tests.Graph
{
    [TestFixture]
    class HelloWorldTest
    {
        private const string inputStream = "in";
        private const string outputStream = "out";
        private const string graphConfigText = @"
input_stream: ""in""
output_stream: ""out""
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""in""
  output_stream: ""out1""
}
node {
  calculator: ""PassThroughCalculator""
  input_stream: ""out1""
  output_stream: ""out""
}
";

        private static CalculatorGraph helloWorldGraph;
        private static OutputStreamPoller<string> outputStreamPoller;
        private static StringPacket outputPacket;
        private static Status pushedInput;

        [Test]
        public static void MainTest()
        {
            Assert.DoesNotThrow(() => {
                helloWorldGraph = new CalculatorGraph(graphConfigText);
                outputStreamPoller = helloWorldGraph.AddOutputStreamPoller<string>(outputStream).Value();
            });

            Status graphStartResult = helloWorldGraph.StartRun();
            Assert.True(graphStartResult.ok);

            Assert.DoesNotThrow(() => {
                int timestamp = System.Environment.TickCount & int.MaxValue;
                var inputPacket = new StringPacket("Hello World", new Timestamp(timestamp));
                outputPacket = new StringPacket();
                pushedInput = helloWorldGraph.AddPacketToInputStream(inputStream, inputPacket);
            });

            if (outputStreamPoller.Next(outputPacket))
                Assert.AreEqual(outputPacket.Get(), "Hello World");
        }
    }
}
