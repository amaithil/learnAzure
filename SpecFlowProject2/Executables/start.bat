SET USER=user
SET WD_PORT=4444

SET SSS=selenium-server-standalone-3.141.59.jar


SET IP=http://localhost
SET PORT=4444
SET THIS_FOLDER_PATH=%~dp0

for %%I in ("%THIS_FOLDER_PATH%\..\..") do set "AssemblyParent=%%~fI"
echo %AssemblyParent%

SET CHROME_DRIVER_FILE_PATH=%AssemblyParent%\<PROJECT_NAME>\bin\Debug\chromedriver.exe

cd %THIS_FOLDER_PATH%

start cmd /k "hub.bat"
start cmd /k "node.bat"