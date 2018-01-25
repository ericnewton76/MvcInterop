@ECHO OFF
setlocal

if "%BUILD_VERSION%" == "" set BUILD_VERSION=%APPVEYOR_BUILD_VERSION%
if "%BUILD_VERSION%" == "" echo BUILD_VERSION not set and APPVEYOR_BUILD_VERSION not set. & goto :ERROR

set NUSPECNAME=MvcInteropX
set PROJECTSRC=MvcInteropX

set NUGETEXE=nuget
where nuget&if errorlevel 0 if not errorlevel 1 goto :NUGET_INSTALLED
set NUGETEXE=%~dp0packages\NuGet.CommandLine.2.8.5\tools\NuGet.exe
if not exist "%NUGETEXE%" echo --Warning, nuget.exe not found in packages.&set NUGETEXE=c:\bin\nuget.exe


:NUGET_INSTALLED
echo.
echo *Cleaning Release\Build directory
if exist "Release\Nuget\." rmdir /s /q Release\Nuget 2>NUL
mkdir /s /q Release\Nuget 2>NUL

echo.
echo *Copying Release build into Release\Nuget.
robocopy /mir src\%PROJECTSRC%\bin\Release Release\Nuget\net40 1>NUL
REM tree /f Release\Nuget
REM echo.

echo.
echo *Copying nuspec into Release
copy %NUSPECNAME%.nuspec Release\Nuget
if errorlevel 1 echo failed to copy %NUSPECNAME%.nuspec into Release\Nuget & goto :ERROR

echo.
pushd Release\Nuget

echo *Nuget pack %NUSPECNAME%.nuspec
echo "%NUGETEXE%" pack %NUSPECNAME%.nuspec -version %BUILD_VERSION% 
"%NUGETEXE%" pack %NUSPECNAME%.nuspec -version %BUILD_VERSION% 
if errorlevel 1 popd&echo --Error: nuget pack command failed.
REM echo.

REM appveyor will deploy *.nupkg
REM "%NUGETEXE%" push MvcInterop.%BUILD_VERSION%.nupkg

goto :END

:ERROR
echo An error occurred.

:END
popd


