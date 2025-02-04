set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\LubanConfig\LuBan\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -c cs-simple-json ^
    -d json  ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputDataDir=%WORKSPACE%\Assets\LubanData\Data^
   -x outputCodeDir=%WORKSPACE%\Assets\LubanData\Code

pause