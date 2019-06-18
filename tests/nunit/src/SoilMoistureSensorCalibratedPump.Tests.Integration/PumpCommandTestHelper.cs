using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
    public class PumpCommandTestHelper : SerialCommandTestHelper
    {
        public PumpMode PumpCommand = PumpMode.Auto;

        public void TestPumpCommand ()
        {
            Letter = "P";
            Value = (int)PumpCommand;
            Label = "pump mode";
            ValueIsSavedInEEPROM = false;

            TestCommand ();
        }
    }
}