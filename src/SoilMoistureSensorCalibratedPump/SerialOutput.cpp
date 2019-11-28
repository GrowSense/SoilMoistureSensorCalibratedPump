#include <Arduino.h>
#include <EEPROM.h>

#include "Common.h"
#include "SoilMoistureSensor.h"
#include "Irrigation.h"
#include "DeviceName.h"

void serialPrintDeviceInfo()
{
  Serial.println("");
  Serial.println("-- Start Device Info");
  Serial.println("Family: GrowSense");
  Serial.println("Group: irrigator");
  Serial.println("Project: SoilMoistureSensorCalibratedPump");
  Serial.print("Board: ");
  Serial.println(BOARD_TYPE);
  Serial.print("Device name: ");
  Serial.println(deviceName);
  Serial.print("Version: ");
  Serial.println(VERSION);
  Serial.println("ScriptCode: irrigator");
  Serial.println("-- End Device Info");
  Serial.println("");
}

void serialPrintData()
{
  bool isTimeToPrintData = millis() - lastSerialOutputTime >= secondsToMilliseconds(serialOutputIntervalInSeconds)
      || lastSerialOutputTime == 0;

  bool isReadyToPrintData = isTimeToPrintData && soilMoistureSensorReadingHasBeenTaken;

  if (isReadyToPrintData)
  {
    Serial.print("D;"); // This prefix indicates that the line contains data.
    Serial.print("R:");
    Serial.print(soilMoistureLevelRaw);
    Serial.print(";C:");
    Serial.print(soilMoistureLevelCalibrated);
    Serial.print(";T:");
    Serial.print(threshold);
    Serial.print(";M:");
    Serial.print(pumpMode);
    Serial.print(";I:");
    Serial.print(soilMoistureSensorReadingIntervalInSeconds);
    Serial.print(";B:");
    Serial.print(pumpBurstOnTime);
    Serial.print(";O:");
    Serial.print(pumpBurstOffTime);
    Serial.print(";WN:"); // Water needed
    Serial.print(soilMoistureLevelCalibrated < threshold);
    Serial.print(";PO:"); // Pump on
    Serial.print(pumpIsOn);
    Serial.print(";D:"); // Dry calibration value
    Serial.print(drySoilMoistureCalibrationValue);
    Serial.print(";W:"); // Wet calibration value
    Serial.print(wetSoilMoistureCalibrationValue);
    Serial.print(";V:");
    Serial.print(VERSION);
    Serial.print(";;");
    Serial.println();


/*    if (isDebugMode)
    {
      Serial.print("Last pump start time:");
      Serial.println(pumpStartTime);
      Serial.print("Last pump finish time:");
      Serial.println(lastPumpFinishTime);
    }*/

    lastSerialOutputTime = millis();
  }
/*  else
  {
    if (isDebugMode)
    {    
      Serial.println("Not ready to serial print data");

      Serial.print("  Is time to serial print data: ");
      Serial.println(isTimeToPrintData);
      if (!isTimeToPrintData)
      {
        Serial.print("    Time remaining before printing data: ");
        Serial.print(millisecondsToSecondsWithDecimal(lastSerialOutputTime + secondsToMilliseconds(serialOutputIntervalInSeconds) - millis()));
        Serial.println(" seconds");
      }
      Serial.print("  Soil moisture sensor reading has been taken: ");
      Serial.println(soilMoistureSensorReadingHasBeenTaken);
      Serial.print("  Is ready to print data: ");
      Serial.println(isReadyToPrintData);

    }
  }*/
}

void forceSerialOutput()
{
    // Reset the last serial output time 
    lastSerialOutputTime = 0;//millis()-secondsToMilliseconds(serialOutputIntervalInSeconds);
}
