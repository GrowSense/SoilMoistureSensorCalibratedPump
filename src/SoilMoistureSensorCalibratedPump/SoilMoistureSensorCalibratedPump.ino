#include <Arduino.h>
#include <EEPROM.h>
#include <duinocom2.h>

#include "Common.h"
#include "SoilMoistureSensor.h"
#include "Irrigation.h"
#include "DeviceName.h"
#include "Commands.h"
#include "SerialOutput.h"

void setup()
{
  Serial.begin(9600);

  Serial.println("Starting irrigator");
  
  serialPrintDeviceInfo();

  setupSoilMoistureSensor();

  setupIrrigation();

  serialOutputIntervalInSeconds = soilMoistureSensorReadingIntervalInSeconds;
  
  loadDeviceNameFromEEPROM();

  Serial.println("Online");
}

void loop()
{
// Disabled. Used for debugging
//  Serial.print(".");

  if (isDebugMode)
    loopNumber++;

  serialPrintLoopHeader();

  checkCommand();

  takeSoilMoistureSensorReading();

  serialPrintData();

  irrigateIfNeeded();

  serialPrintLoopFooter();

  delay(1);
}
