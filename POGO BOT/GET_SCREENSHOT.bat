@echo off
rem Proccessing
if not exist "tmp" mkdir tmp
cd tmp
if exist screen.png del screen.png /q /f
adb shell screencap -p /sdcard/screen.png
adb pull -p -a /sdcard/screen.png
adb shell rm /sdcard/screen.png
exit