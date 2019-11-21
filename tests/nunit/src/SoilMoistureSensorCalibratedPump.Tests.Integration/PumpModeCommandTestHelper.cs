using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
  public class PumpModeCommandTestHelper : SerialCommandTestHelper
  {
    public PumpMode PumpMode = PumpMode.Auto;

    public void TestPumpModeCommand ()
    {
      Letter = "M";
      Value = (int)PumpMode;
      Label = "pump mode";
      ValueIsSavedInEEPROM = false;

      TestCommand ();
    }
  }
}