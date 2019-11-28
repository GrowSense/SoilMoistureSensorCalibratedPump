DEVICE_NAME=$1
SERIAL_PORT=$2

echo "Sending device name name as a command to device..."

if [ ! $DEVICE_NAME ]; then
  echo "Please provide a device name as an argument."
  exit 1
fi

if [ ! $SERIAL_PORT ]; then
  echo "Please provide a serial port as an argument."
  exit 1
fi

echo "  Device name: $DEVICE_NAME"
echo "  Device port: $SERIAL_PORT"

sh send-serial-command.sh "Name:$MQTT_DEVICE_NAME" $SERIAL_PORT

echo "Finished sending device name command"
