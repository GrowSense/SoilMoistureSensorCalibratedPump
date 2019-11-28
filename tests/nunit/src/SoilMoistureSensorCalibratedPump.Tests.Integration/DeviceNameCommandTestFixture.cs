using System;
using NUnit.Framework;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
  [TestFixture(Category="Integration")]
  public class DeviceNameCommandTestFixture : BaseTestFixture
  {
    [Test]
    public void Test_SetMqttDeviceNameCommand ()
    {
      using (var helper = new SerialCommandTestHelper ()) {
        helper.Label = "Device name";
        helper.Value = "device" + new Random ().Next (100).ToString ();
        helper.Key = "Name";
        // TODO: Remove if not needed
        /*helper.ValueIsOutputAsData = false;
        helper.ValueIsOutputAsData = false;
        helper.RequiresResetSettings = false;
        helper.SeparateKeyValueWithColon = true;
        helper.CheckExpectedSerialOutput = true;*/

        helper.DevicePort = GetDevicePort ();
        helper.DeviceBaudRate = GetDeviceSerialBaudRate ();

        helper.SimulatorPort = GetSimulatorPort ();
        helper.SimulatorBaudRate = GetSimulatorSerialBaudRate ();

        helper.TestCommand ();
      }
    }
  }
}

