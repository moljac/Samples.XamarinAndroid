#!/bin/bash

export DIR_OUTPUT=../../../external/binaries-xtensive/docs
export JAR=../../../external/binaries-minimal/android/android-sdk-4.4.2.jar

export CLASSES=\
"
 	com/brightcove/player/view/BrightcoveVideoView
 	com/brightcove/player/view/BaseVideoView
"

export CHAR_FORWARD_SLASH=/
export CHAR_DOT=.


[ ! -d DIR_OUTPUT ] && mkdir $DIR_OUTPUT

for c in $CLASSES; 
do 
	echo "==========================================================================================="
	echo "c                  =$c";
	export CLASS=`echo $c | awk -v srch="$CHAR_FORWARD_SLASH" -v repl="$CHAR_DOT" '{ gsub(srch,repl,$0); print $0 }'`
	echo "CLASS              =$CLASS";
	javap -classpath \
 		$JAR \
 		$c \
 		> \
 		$DIR_OUTPUT/$CLASS.class.txt

 		cat $DIR_OUTPUT/$CLASS.class.txt
done
