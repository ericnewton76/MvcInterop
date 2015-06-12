@ECHO OFF

setlocal
set NUGETEXE=%~dp0\packages\NuGet.CommandLine.2.8.5\tools\NuGet.exe

if exist "Build\MvcInterop\." rmdir /s /q Build\MvcInterop 2>NUL
mkdir Build\MvcInterop 2>NUL

xcopy /I src\MvcInterop\bin\Release\Mvc* Build\MvcInterop\lib\net40

copy MvcInterop.nuspec Build\MvcInterop

pushd Build\MvcInterop
"%NUGETEXE%" pack MvcInterop.nuspec -version %APPVEYOR_BUILD_VERSION% -OutputDirectory ..\..

REM appveyor will deploy *.nupkg
REM "%NUGETEXE%" push MvcInterop.%BUILD_VERSION%.nupkg


goto :END

:ERROR
echo An error occurred.

:END
popd


