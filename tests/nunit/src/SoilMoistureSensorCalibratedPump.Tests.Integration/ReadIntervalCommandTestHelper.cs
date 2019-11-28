using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
    public class ReadIntervalCommandTestHelper : SerialCommandTestHelper
    {
        public int ReadingInterval = 3;

        public void TestSetReadIntervalCommand ()
        {
            Key = "I";
            Value = ReadingInterval;
            Label = "reading interval";

            TestCommand ();
        }
    }
}
