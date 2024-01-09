java -jar selenium-server-standalone-3.141.59.jar -role hub
java -jar selenium-server-standalone-3.141.59.jar -role node -hub http://localhost:4444/grid/register

start.bat
SET USER=user
SET WD_PORT=5555

SET SSS=selenium-server-standalone-3.141.59.jar


SET IP=http://localhost
SET PORT=5555
SET THIS_FOLDER_PATH=%~dp0

for %%I in ("%THIS_FOLDER_PATH%\..\..") do set "AssemblyParent=%%~fI"
echo %AssemblyParent%

SET CHROME_DRIVER_FILE_PATH=%AssemblyParent%\<PROJECT_NAME>\bin\Debug\chromedriver.exe

cd %THIS_FOLDER_PATH%
start cmd /k "hub.bat"
call "node.bat"
--------------------------------------------------------------------------------------------

hub.bat

title hub
TIMEOUT /T 5
java -jar %SSS% -role hub -hubConfig hub-conf.json -port %PORT%
TIMEOUT /T 15
--------------------------------------------------------------------------------------------

node.bat

title node
TIMEOUT /T 5
java -Dwebdriver.chrome.driver=%CHROME_DRIVER_FILE_PATH% -jar %SSS% -role webdriver -nodeConfig win-node-conf.json -hub %IP%:%WD_PORT%/grid/register
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------