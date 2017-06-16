@ECHO OFF
setlocal

if "%BUILD_VERSION%" == "" set BUILD_VERSION=%APPVEYOR_BUILD_VERSION%
if "%BUILD_VERSION%" == "" echo BUILD_VERSION not set and APPVEYOR_BUILD_VERSION not set. & goto :ERROR

set NUSPECNAME=MvcInteropX
set PROJECTSRC=MvcInterop

set NUGETEXE=%~dp0packages\NuGet.CommandLine.2.8.5\tools\NuGet.exe
if not exist "%NUGETEXE%" echo --Warning, nuget.exe not found in packages. Using C:\bin\nuget.exe & set NUGETEXE=C:\bin\NuGet.exe

echo *Cleaning Nuget\Build directory
if exist "Build\Nuget\." rmdir /s /q Build\Nuget 2>NUL
mkdir Build\Nuget 2>NUL

echo *Copying Release build into Build\Nuget.
xcopy /I src\%PROJECTSRC%\bin\Release\Mvc* Build\Nuget\lib\net40 1>NUL
REM tree /f Build\Nuget
REM echo.

echo *Copying nuspec into Build\Nuget.
copy %NUSPECNAME%.nuspec Build\Nuget
if errorlevel 1 echo failed to copy %NUSPECNAME%.nuspec into Build\Nuget & goto :ERROR

pushd Build\Nuget
echo *Nuget pack %NUSPECNAME%.nuspec
echo "%NUGETEXE%" pack %NUSPECNAME%.nuspec -version %BUILD_VERSION% 
"%NUGETEXE%" pack %NUSPECNAME%.nuspec -version %BUILD_VERSION% 
if errorlevel 1 echo --Error: nuget pack command failed.
REM echo.

REM appveyor will deploy *.nupkg
REM "%NUGETEXE%" push MvcInterop.%BUILD_VERSION%.nupkg

goto :END

:ERROR
echo An error occurred.

:END
popd


