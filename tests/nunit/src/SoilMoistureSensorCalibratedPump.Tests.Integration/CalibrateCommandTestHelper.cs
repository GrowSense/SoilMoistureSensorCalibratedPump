﻿using System;
using System.Threading;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
    public class CalibrateCommandTestHelper : SerialCommandTestHelper
    {
        public int RawSoilMoistureValue = 0;

        public void TestCalibrateCommand ()
        {
            Value = RawSoilMoistureValue;
            TestCommand ();
        }
    }
}