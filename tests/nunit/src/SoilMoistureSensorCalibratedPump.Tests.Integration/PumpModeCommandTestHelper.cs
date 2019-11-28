using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
  public class PumpModeCommandTestHelper : SerialCommandTestHelper
  {
    public PumpMode PumpMode = PumpMode.Auto;

    public void TestPumpModeCommand ()
    {
      Key = "M";
      Value = ((int)PumpMode).ToString ();
      Label = "pump mode";
      ValueIsSavedInEEPROM = false;

      TestCommand ();
    }
  }
}