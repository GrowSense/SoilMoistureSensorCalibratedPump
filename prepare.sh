#!/bin/bash

# Note: You may need to run this script with sudo

echo "Preparing this system for the project"

DIR=$PWD

sudo apt-get update -qq

# curl
if ! type "curl" > /dev/null; then
  sudo apt-get install -qq -y curl
fi

# unzip
if ! type "unzip" > /dev/null; then
  sudo apt-get install -qq -y unzip
fi

# git
if ! type "git" > /dev/null; then
  sudo apt-get install -qq -y git
fi

sh prepare-sketch.sh
sh prepare-tests.sh


cd $DIR

echo "Finished preparing your project."
