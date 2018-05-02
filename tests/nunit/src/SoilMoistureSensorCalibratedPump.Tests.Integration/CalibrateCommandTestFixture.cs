using System;
using NUnit.Framework;
using duinocom;
using System.Threading;
using ArduinoSerialControllerClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
	[TestFixture(Category="Integration")]
	public class CalibrateCommandTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_CalibrateDryToCurrentValueCommand()
		{
			var percentage = 20;

			var raw = 218;

			TestCalibrateToCurrentValueCommand ("dry", "D", percentage, raw);
		}

		[Test]
		public void Test_CalibrateDryToSpecifiedValueCommand()
		{
			var percentage = 20;

			var raw = 220;

			TestCalibrateToCurrentValueCommand ("dry", "D" + raw, -1, raw);
		}

		[Test]
		public void Test_CalibrateWetToCurrentValueCommand()
		{
			var percentage = 80;

			var raw = 880;

			TestCalibrateToCurrentValueCommand ("wet", "W", percentage, raw);
		}

		[Test]
		public void Test_CalibrateWetToSpecifiedValueCommand()
		{
			var raw = 880;

			TestCalibrateToCurrentValueCommand ("wet", "W" + raw, -1, raw);
		}

		public void TestCalibrateToCurrentValueCommand(string label, string command, int simulatedSoilMoisturePercentage, int expectedRaw)
		{

			Console.WriteLine ("");
			Console.WriteLine ("==============================");
			Console.WriteLine ("Starting calibrate " + label + " command test");
			Console.WriteLine ("");
			Console.WriteLine ("Percentage in: " + simulatedSoilMoisturePercentage);
			Console.WriteLine ("Expected raw: " + expectedRaw);

			SerialClient irrigator = null;
			ArduinoSerialDevice soilMoistureSimulator = null;

			try {
				irrigator = new SerialClient (GetDevicePort(), GetSerialBaudRate());
				soilMoistureSimulator = new ArduinoSerialDevice (GetSimulatorPort(), GetSerialBaudRate());

				Console.WriteLine("");
				Console.WriteLine("Connecting to serial devices...");
				Console.WriteLine("");

				irrigator.Open ();
				soilMoistureSimulator.Connect ();

				Thread.Sleep (2000);

				Console.WriteLine("");
				Console.WriteLine("Reading the output from the device...");
				Console.WriteLine("");

				// Read the output
				var output = irrigator.Read ();

				Console.WriteLine (output);
				Console.WriteLine ("");

				Console.WriteLine("");
				Console.WriteLine("Sending 'X' command to device to reset to defaults...");
				Console.WriteLine("");

				// Reset defaults
				irrigator.WriteLine ("X");

				// Set read interval to 1
				irrigator.WriteLine ("V1");

				Thread.Sleep(1000);

				Console.WriteLine("");
				Console.WriteLine("Reading the output from the device...");
				Console.WriteLine("");

				// Read the output
				output = irrigator.Read ();

				Console.WriteLine (output);
				Console.WriteLine ("");

				// If a percentage is specified for the simulator then set the simulated soil moisture value (otherwise skip)
				if (simulatedSoilMoisturePercentage > -1)
				{
					Console.WriteLine("");
					Console.WriteLine("Sending analog percentage to simulator: " + simulatedSoilMoisturePercentage);
					Console.WriteLine("");

					// Set the simulated soil moisture
					soilMoistureSimulator.AnalogWritePercentage (9, simulatedSoilMoisturePercentage);

					Thread.Sleep(4000);

					Console.WriteLine("");
					Console.WriteLine("Reading output from the device...");
					Console.WriteLine("");

					// Read the output
					output = irrigator.Read ();

					Console.WriteLine (output);
					Console.WriteLine ("");

					// Parse the values in the data line
					var values = ParseOutputLine(GetLastDataLine(output));

					// Get the raw soil moisture value
					var rawValue = Convert.ToInt32(values["R"]);

					Console.WriteLine("");
					Console.WriteLine("Checking the device...");
					Console.WriteLine("");

					Console.WriteLine("Expected raw: " + expectedRaw);

					// Ensure the raw value is in the valid range
					Assert.IsTrue(IsWithinRange(expectedRaw, rawValue, 15), "Raw value is outside the valid range: " + rawValue);
				}

				Console.WriteLine("");
				Console.WriteLine("Sending '" + command + "' to the device...");
				Console.WriteLine("");

				// Send the command
				irrigator.WriteLine (command);

				Thread.Sleep(2000);

				Console.WriteLine("");
				Console.WriteLine("Reading the output from the device...");
				Console.WriteLine("");

				// Read the output
				output = irrigator.Read ();

				Console.WriteLine (output);
				Console.WriteLine ("");

				Console.WriteLine("");
				Console.WriteLine("Checking the output...");
				Console.WriteLine("");

				var data = ParseOutputLine(GetLastDataLine(output));

				var value = Convert.ToInt32(data[command.Substring(0, 1)]);

				Console.WriteLine("Value: " + value);

				Assert.IsTrue(IsWithinRange(expectedRaw, value, 15), "Calibration value is outside the valid range: " + value);


			} catch (IOException ex) {
				Console.WriteLine (ex.ToString ());
				Assert.Fail ();
			} finally {
				if (irrigator != null)
					irrigator.Close ();

				if (soilMoistureSimulator != null)
					soilMoistureSimulator.Disconnect ();
			}
		}
	}
}