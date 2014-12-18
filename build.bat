@echo off

set version=
if not "%PackageVersion%" == "" (
	set version=-Version %PackageVersion%
)

mkdir build

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild SimpleRazor.sln /p:Configuration="Release-Net45" /m /v:Q /fl /flp:LogFile=build/msbuild.log;Verbosity=Normal /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild SimpleRazor.sln /p:Configuration="Release-Net40" /m /v:Q /fl /flp:LogFile=build/msbuild.log;Verbosity=Normal /nr:false

cmd /c %nuget% pack "SimpleRazor\SimpleRazor.nuspec" -symbols -o build %version% -verbosity detailed
