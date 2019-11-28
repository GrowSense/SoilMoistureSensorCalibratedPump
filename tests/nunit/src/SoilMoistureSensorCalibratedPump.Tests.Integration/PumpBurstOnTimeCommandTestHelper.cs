﻿using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
    public class PumpBurstOnTimeCommandTestHelper : SerialCommandTestHelper
    {
        public int PumpBurstOnTime = 1;

        public void TestPumpBurstOnTimeCommand ()
        {
            Key = "O";
            Value = PumpBurstOnTime;
            Label = "pump burst on time";

            TestCommand ();
        }
    }
}