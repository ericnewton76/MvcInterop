
mkdir Build 2>NUL

msbuild src/MvcInterop.sln /p:Configuration=Release /p:OutputPath=%~dp0\Build\MvcInterop\bin


