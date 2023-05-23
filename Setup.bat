@echo off
setlocal

set libs="%ProgramFiles(x86)%\Steam\steamapps\common\OxygenNotIncluded\OxygenNotIncluded_Data\Managed"
set logs="%UserProfile%\AppData\LocalLow\Klei\Oxygen Not Included"
set mods="%UserProfile%\Documents\Klei\OxygenNotIncluded\mods"

if not exist "links\" mkdir links

if not exist "links\libs" if exist %libs% mklink /j "links\libs" %libs%
if not exist "links\logs" if exist %logs% mklink /j "links\logs" %logs%
if not exist "links\mods" if exist %mods% mklink /j "links\mods" %mods%

endlocal
