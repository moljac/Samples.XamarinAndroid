#!/bin/bash

. ./project-binding-specific-data.sh

echo XBUILD=$XBUILD
echo MDTOOL=$MDTOOL

$XBUILD \
	/p:Configuration=Release \
	../$PROJECT
$XBUILD \
	/p:Configuration=Debug \
	../$PROJECT


ls -al	../obj/Debug/api.xml
cat		../obj/Debug/api.xml

# <attr 
#	path="/api/package[@name='com.mycompany.myapi]" 
#	name="managedName"
#	>
#	MyCompany.MyAPI
# </attr>
# grep "<package name=*" ../obj/Debug/api.xml
# sed '/package name=/' ../obj/Debug/api.xml
awk '/package name=/' ../obj/Debug/api.xml

