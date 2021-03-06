﻿using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
    public class ThresholdToCurrentCommandTestHelper : GrowSenseIrrigatorHardwareTestHelper
    {
        public int Threshold = 30;
        public int SimulatedSoilMoisturePercentage = -1;

        public void TestThresholdCommand ()
        {
            WriteTitleText ("Starting threshold command test");

            Console.WriteLine ("Simulated soil moisture: " + SimulatedSoilMoisturePercentage + "%");
            Console.WriteLine ("Threshold: " + Threshold + "%");
            Console.WriteLine ("");

            var simulatorIsNeeded = SimulatedSoilMoisturePercentage > -1;

            ConnectDevices (simulatorIsNeeded);

            if (simulatorIsNeeded) {
                SimulateSoilMoisture (SimulatedSoilMoisturePercentage);

                // Skip some data
                WaitForData (2);

                var dataEntry = WaitForDataEntry ();

                AssertDataValueIsWithinRange (dataEntry, "C", SimulatedSoilMoisturePercentage, CalibratedValueMarginOfError);
            }

            SendThresholdCommand ();
        }

        public void SendThresholdCommand ()
        {
            var simulatorIsNeeded = SimulatedSoilMoisturePercentage > -1;

            var command = "T";
            // If the simulator isn't enabled then the raw value is passed as part of the command to specify it directly
            if (!simulatorIsNeeded)
                command = command + Threshold;

            WriteParagraphTitleText ("Sending threshold command...");

            SendDeviceCommand (command);

            var dataEntry = WaitForDataEntry ();

            WriteParagraphTitleText ("Checking threshold value...");

            // If using the soil moisture simulator then the value needs to be within a specified range
            if (simulatorIsNeeded) {
                AssertDataValueIsWithinRange (dataEntry, "T", Threshold, CalibratedValueMarginOfError);
            } else { // Otherwise it needs to be exact
                AssertDataValueEquals (dataEntry, "T", Threshold);
            }
        }
    }
}

