using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO.Ports;

namespace SoilMoistureSensorCalibratedPump.Tests.Integration
{
	public class BaseTestFixture
	{
		public BaseTestFixture()
		{
		}

		[SetUp]
		public void Initialize()
		{
		}

		[TearDown]
		public void Finish()
		{
		}

		public string GetDevicePort()
		{
			var devicePort = Environment.GetEnvironmentVariable("IRRIGATOR_PORT");

			if (String.IsNullOrEmpty(devicePort))
				devicePort = "/dev/ttyUSB0";

			Console.WriteLine("Device port: " + devicePort);

			return devicePort;
		}

		public string GetSimulatorPort()
		{
			var simulatorPort = Environment.GetEnvironmentVariable("IRRIGATOR_SIMULATOR_PORT");

			if (String.IsNullOrEmpty(simulatorPort))
				simulatorPort = "/dev/ttyUSB1";

			Console.WriteLine("Simulator port: " + simulatorPort);

			return simulatorPort;
		}

		public int GetDeviceSerialBaudRate()
		{
			var baudRateString = Environment.GetEnvironmentVariable ("IRRIGATOR_ESP_BAUD_RATE");
			
			var baudRate = 0;
			
			if (String.IsNullOrEmpty(baudRateString))
				baudRate = 9600;
			else
				baudRate = Convert.ToInt32(baudRateString);
			
			Console.WriteLine ("Device baud rate: " + baudRate);
			
			return baudRate;
		}

		public int GetSimulatorSerialBaudRate()
		{
			var baudRateString = Environment.GetEnvironmentVariable ("IRRIGATOR_ESP_SIMULATOR_BAUD_RATE");
			
			var baudRate = 0;
			
			if (String.IsNullOrEmpty(baudRateString))
				baudRate = 9600;
			else
				baudRate = Convert.ToInt32(baudRateString);
			
			Console.WriteLine ("Simulator baud rate: " + baudRate);
			
			return baudRate;
		}
	}
}
