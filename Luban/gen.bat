set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Luban\Tools\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=%WORKSPACE%/Assets/Scripts/Gen ^
    -x outputDataDir=%WORKSPACE%\Assets\StreamingAssets\GenerateDatas\json

pause