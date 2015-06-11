@ECHO OFF

setlocal
set NUGETEXE=%~dp0\packages\NuGet.CommandLine.2.8.5\tools\NuGet.exe

mkdir Build 2>NUL

mkdir Build\Nupkg 2>NUL

copy *.nuspec Build\Nupkg

pushd Build\Nupkg

"%NUGETEXE%" pack MvcInterop.nuspec -version %BUILD_VERSION%

REM appveyor will deploy *.nupkg
REM "%NUGETEXE%" push MvcInterop.%BUILD_VERSION%.nupkg

goto :END

:ERROR
echo An error occurred.

:END
popd


