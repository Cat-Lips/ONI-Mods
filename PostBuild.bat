SET ProjectName=%~1
SET BuildDir="%~2\"
SET RootDir=%~dp0

SET ModDir="%RootDir%links\mods\Dev\%ProjectName%\\"

ECHO [PostBuild] Starting ROBOCOPY...
ECHO [PostBuild]   Source=%BuildDir%
ECHO [PostBuild]   Target=%ModDir%

IF NOT EXIST %ModDir% MKDIR %ModDir%
ROBOCOPY %BuildDir% %ModDir% /MIR /R:0 /W:0 > NUL
SET RC=%ERRORLEVEL%

ECHO [PostBuild] ROBOCOPY exit code=%RC%

IF %RC% LEQ 7 EXIT /B 0
EXIT /B %RC%
