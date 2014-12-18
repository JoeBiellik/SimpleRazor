@echo off

set version=
if not "%PackageVersion%" == "" (
	set version=-Version %PackageVersion%
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild SimpleRazor.sln /p:Configuration="Release-Net45" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild SimpleRazor.sln /p:Configuration="Release-Net40" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir nuget
cmd /c %nuget% pack "SimpleRazor\SimpleRazor.csproj" -symbols -o nuget -p Configuration=Release %version%
