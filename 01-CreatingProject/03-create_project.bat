@echo off

set ANDROID="%PROGRAMFILES(X86)%\Android\android-sdk\tools\android.bat"
set TARGET_ID=3

%ANDROID% create project ^
	--target %TARGET_ID% ^
	--name Application01 ^
	--path ./Application01 ^
	--activity MainActivity ^
	--package net.holisticware.application01
	
	
pause
