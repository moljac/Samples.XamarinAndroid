#!/bin/bash

JAR2XML_LOCATED=`locate jar2xml.jar`
TMP_FOLDER=~/Projects/tmp
echo $JAR2XML_LOCATED
ls -al $JAR2XML_LOCATED


cd $TMP_FOLDER
git clone --recursive https://github.com/xamarin/jar2xml.git
cd jar2xml/
make all
ls -al $JAR2XML_LOCATED
sudo cp -f jar2xml.jar $JAR2XML_LOCATED
ls -al $JAR2XML_LOCATED
