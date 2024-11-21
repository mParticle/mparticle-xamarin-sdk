#!/bin/bash

# .NET MAUI
# 

# Restore packages
dotnet restore

# Build bindings
dotnet build Bindings/mParticle.MAUI.AndroidBinding/mParticle.MAUI.AndroidBinding.csproj /p:Configuration=Release /t:Rebuild
dotnet build Bindings/mParticle.MAUI.iOSBinding/mParticle.MAUI.iOSBinding.csproj /p:Configuration=Release /t:Rebuild

# Build Libraries
dotnet build Library/mParticle.MAUI.Abstractions/mParticle.MAUI.Abstractions.csproj /p:Configuration=Release /t:Rebuild
dotnet build Library/mParticle.MAUI.Android/mParticle.MAUI.Android.csproj /p:Configuration=Release /t:Rebuild
dotnet build Library/mParticle.MAUI.iOS/mParticle.MAUI.iOS.csproj /p:Configuration=Release /t:Rebuild

# Build Sample Apps
dotnet build Samples/mParticle.MAUI.Android.Sample/mParticle.MAUI.Android.Sample.csproj /p:Configuration=Debug /t:Rebuild
dotnet build Samples/mParticle.MAUI.iOS.Sample/mParticle.MAUI.iOS.Sample.csproj /p:Configuration=Debug /t:Rebuild


# Package for nuget
#

# dotnet pack -p:NuspecFile=mparticle.nuspec -p:NuspecBasePath=.
nuget pack mparticle.nuspec