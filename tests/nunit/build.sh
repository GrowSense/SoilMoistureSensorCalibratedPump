echo "Building project tests"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/SoilMoistureSensorCalibratedPump.sln /p:Configuration=Release /verbosity:quiet && \

echo "Finished building project tests." ||

echo "Failed building project tests!"
