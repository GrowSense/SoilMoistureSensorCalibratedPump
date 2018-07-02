#pio run --target clean

SOURCE_FILE="src/SoilMoistureSensorCalibratedPump/SoilMoistureSensorCalibratedPump.ino"

sed -i "s/#define VERSION .*/#define VERSION \"1-0-0-0\"/" $SOURCE_FILE
