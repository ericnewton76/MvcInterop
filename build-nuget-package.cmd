@ECHO OFF

setlocal
set NUGETEXE=%~dp0\packages\NuGet.CommandLine.2.8.5\tools\NuGet.exe

if exist "Build\Nuget\." rmdir /s /q Build\Nuget 2>NUL
mkdir Build\Nuget 2>NUL

xcopy /I src\MvcInterop\bin\Release\Mvc* Build\Nuget\lib\net40

copy MvcInterop.nuspec Build\Nuget

pushd Build\Nuget
"%NUGETEXE%" pack MvcInterop.nuspec -version %APPVEYOR_BUILD_VERSION% 

REM appveyor will deploy *.nupkg
REM "%NUGETEXE%" push MvcInterop.%BUILD_VERSION%.nupkg


goto :END

:ERROR
echo An error occurred.

:END
popd


