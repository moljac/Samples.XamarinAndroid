@echo off

set ANDROID="%PROGRAMFILES(X86)%\Android\android-sdk\tools\android.bat"

%ANDROID% list targets > targets.txt

pause
