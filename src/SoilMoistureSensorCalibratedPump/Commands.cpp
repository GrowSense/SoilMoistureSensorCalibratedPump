#include <Arduino.h>
#include <EEPROM.h>

#include "Commands.h"
#include "Irrigation.h"
#include "DeviceName.h"
#include "SerialOutput.h"

void checkCommand()
{
  /*if (isDebugMode)
  {
    Serial.println("Checking incoming serial commands");
  }*/

  if (checkMsgReady())
  {
    char* msg = getMsg();
       
    handleCommand(msg);
  }
}

void handleCommand(char* msg)
{
  if (isDebugMode)
  {
    Serial.println("");
    Serial.println("Handling command...");  
  }

  Serial.print("Received message: ");
  Serial.println(msg);
        
  char letter = msg[0];

  Serial.print("Received message: ");
  Serial.println(msg);

  if (isKeyValue(msg))
  {
    Serial.println("  Is key value");
  
    char* key = getKey(msg);
  
    Serial.print("  Key: \"");
    Serial.print(key);
    Serial.println("\"");
  
    char* value = getValue(msg);
  
    Serial.print("  Value: \"");
    Serial.print(value);
    Serial.println("\"");

    if (strcmp(key, "Name") == 0)
    {
      if (isDebugMode)
        Serial.println("  Set device name");
      setDeviceName(value);
    }
  }
  else
  {
    switch (letter)
    {
      case '#':
        serialPrintDeviceInfo();
        break;
      case 'M':
        setPumpMode(msg);
        break;
      case 'T':
        setThreshold(msg);
        break;
      case 'D':
        setDrySoilMoistureCalibrationValue(msg);
        break;
      case 'W':
        setWetSoilMoistureCalibrationValue(msg);
        break;
      case 'I':
        setSoilMoistureSensorReadingInterval(msg);
        break;
      case 'B':
        setPumpBurstOnTime(msg);
        break;
      case 'O':
        setPumpBurstOffTime(msg);
        break;
      case 'X':
        restoreDefaultSettings();
        break;
      case 'N':
        Serial.println("Turning pump on");
        pumpMode = PUMP_MODE_ON;
        pumpOn();
        break;
      case 'F':
        Serial.println("Turning pump off");
        pumpMode = PUMP_MODE_OFF;
        pumpOff();
        break;
      case 'A':
        Serial.println("Turning pump to auto");
        pumpMode = PUMP_MODE_AUTO;
        irrigateIfNeeded();
        break;
      case 'Z':
        Serial.println("Toggling IsDebug");
        isDebugMode = !isDebugMode;
        break;
      case 'R':
        reverseSoilMoistureCalibrationValues();
        break;
	  }
  }
  
  forceSerialOutput();
  
  if (isDebugMode)
  {
    Serial.println("");
  }
}

void restoreDefaultSettings()
{
  Serial.println("Restoring default settings");

  restoreDefaultSoilMoistureSensorSettings();
  restoreDefaultIrrigationSettings();
  
  EEPROMReset();
}
