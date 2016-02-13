#!/bin/bash


adb -e shell \
	"
	getprop persist.sys.language
	getprop persist.sys.country

	setprop persist.sys.language hr
	setprop persist.sys.country HR
	setprop ctl.restart zygote
	stop
	sleep 5
	start
	getprop persist.sys.language
	getprop persist.sys.country

	"

