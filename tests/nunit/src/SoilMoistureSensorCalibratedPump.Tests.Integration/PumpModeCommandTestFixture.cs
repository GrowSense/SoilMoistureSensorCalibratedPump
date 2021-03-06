﻿using NUnit.Framework;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
  [TestFixture (Category = "Integration")]
  public class PumpModeCommandTestFixture : BaseTestFixture
  {
    [Test]
    public void Test_SetPumpToOn ()
    {
      using (var helper = new PumpModeCommandTestHelper ()) {
        helper.PumpMode = PumpMode.On;

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestPumpModeCommand ();
      }
    }

    [Test]
    public void Test_SetPumpToOff ()
    {
      using (var helper = new PumpModeCommandTestHelper ()) {
        helper.PumpMode = PumpMode.Off;

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestPumpModeCommand ();
      }
    }

    [Test]
    public void Test_SetPumpToAuto ()
    {
      using (var helper = new PumpModeCommandTestHelper ()) {
        helper.PumpMode = PumpMode.Auto;

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestPumpModeCommand ();
      }
    }
  }
}
