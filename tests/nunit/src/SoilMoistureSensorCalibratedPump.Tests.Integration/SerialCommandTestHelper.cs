﻿using System;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
  public class SerialCommandTestHelper : GrowSenseIrrigatorHardwareTestHelper
  {
    public string Key = "";
    public int Value = 0;
    public string Label = "";
    public bool ValueIsSavedInEEPROM = true;

    public void TestCommand ()
    {
      WriteTitleText ("Starting " + Label + " command test");

      Console.WriteLine ("Value for " + Label + ": " + Value);
      Console.WriteLine ("");

      ConnectDevices ();

      SendCommand ();

      if (ValueIsSavedInEEPROM)
        ResetAndCheckSettingIsPreserved ();
    }

    public void SendCommand ()
    {
      WriteParagraphTitleText ("Sending " + Label + " command...");

      var command = Key + Value;

      SendDeviceCommand (command);

      WriteParagraphTitleText ("Checking " + Label + " value was set...");

      var dataEntry = WaitForDataEntry ();

      AssertDataValueEquals (dataEntry, Key, Value);
    }

    public void ResetAndCheckSettingIsPreserved ()
    {
      ResetDeviceViaPin ();

      WriteParagraphTitleText ("Checking " + Label + " value is preserved after reset...");

      var dataEntry = WaitForDataEntry ();

      AssertDataValueEquals (dataEntry, Key, Value);
    }
  }
}

