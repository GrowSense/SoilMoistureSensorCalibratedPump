using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
	public class PumpTestHelper : GreenSenseIrrigatorHardwareTestHelper
	{
		public PumpStatus PumpCommand = PumpStatus.Auto;
		public int SimulatedSoilMoisturePercentage = 50;
		public int BurstOnTime = 3;
		public int BurstOffTime = 3;
		public int Threshold = 30;
		public int DurationToCheckPump = 5;

		public void TestPump()
		{
			WriteTitleText("Starting pump test");

			Console.WriteLine("Pump command: " + PumpCommand);
			Console.WriteLine("Simulated soil moisture: " + SimulatedSoilMoisturePercentage + "%");
			Console.WriteLine("");

			EnableDevices();

			var cmd = "P" + (int)PumpCommand;

			SendDeviceCommand(cmd);

			SendDeviceCommand("B" + BurstOnTime);
			SendDeviceCommand("O" + BurstOffTime);
			SendDeviceCommand("T" + Threshold);

			SimulateSoilMoisture(SimulatedSoilMoisturePercentage);

			var data = WaitForData(3);

			CheckDataValues(data[data.Length - 1]);
		}

		public void CheckDataValues(Dictionary<string, string> dataEntry)
		{
			AssertDataValueEquals(dataEntry, "P", (int)PumpCommand);
			AssertDataValueEquals(dataEntry, "B", BurstOnTime);
			AssertDataValueEquals(dataEntry, "O", BurstOffTime);
			AssertDataValueEquals(dataEntry, "T", Threshold);

			// TODO: Check PO value matches the pump

			AssertDataValueIsWithinRange(dataEntry, "C", SimulatedSoilMoisturePercentage, CalibratedValueMarginOfError);

			switch (PumpCommand)
			{
				case PumpStatus.Off:
					CheckPumpIsOff();
					break;
				case PumpStatus.On:
					CheckPumpIsOn();
					break;
				case PumpStatus.Auto:
					CheckPumpIsAuto();
					break;
			}
		}

		public void CheckPumpIsOff()
		{
			AssertSimulatorPinForDuration("pump", SoilMoistureSensorSimulatorPumpPin, false, DurationToCheckPump);
		}

		public void CheckPumpIsOn()
		{
			AssertSimulatorPinForDuration("pump", SoilMoistureSensorSimulatorPumpPin, true, DurationToCheckPump);
		}

		public void CheckPumpIsAuto()
		{
			if (SimulatedSoilMoisturePercentage < Threshold)
			{
				if (BurstOffTime == 0)
				{
					CheckPumpIsOn();
				}
				else
				{
					// Wait for the pump to turn on for the first time
					WaitUntilSimulatorPinIs("pump", SoilMoistureSensorSimulatorPumpPin, true);

					// Check on time     
					var timeOn = WaitWhileSimulatorPinIs("pump", SoilMoistureSensorSimulatorPumpPin, true);
					AssertIsWithinRange("pump", BurstOnTime, timeOn, TimeErrorMargin);

					// Check off time
					var timeOff = WaitWhileSimulatorPinIs("pump", SoilMoistureSensorSimulatorPumpPin, false);
					AssertIsWithinRange("pump", BurstOffTime, timeOff, TimeErrorMargin);

					// Check on time
					timeOn = WaitWhileSimulatorPinIs("pump", SoilMoistureSensorSimulatorPumpPin, true);
					AssertIsWithinRange("pump", BurstOnTime, timeOn, TimeErrorMargin);

					// Check off time
					timeOff = WaitWhileSimulatorPinIs("pump", SoilMoistureSensorSimulatorPumpPin, false);
					AssertIsWithinRange("pump", BurstOffTime, timeOff, TimeErrorMargin);
				}
			}
			else
			{
				CheckPumpIsOff();
			}
		}
	}
}