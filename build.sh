#!/bin/bash

# Xamarin
#

# Restore packages
nuget restore

# Build bindings
msbuild Bindings/mParticle.Xamarin.AndroidBinding/mParticle.Xamarin.AndroidBinding.csproj /p:Configuration=Release /t:Rebuild
msbuild Bindings/mParticle.Xamarin.iOSBinding/mParticle.Xamarin.iOSBinding.csproj /p:Configuration=Release /t:Rebuild

# Build Libraries
msbuild Library/mParticle.Xamarin.Abstractions/mParticle.Xamarin.Abstractions.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin.Android/mParticle.Xamarin.Android.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin.iOS/mParticle.Xamarin.iOS.csproj /p:Configuration=Release /t:Rebuild

# Build Sample Apps
msbuild Samples/mParticle.Xamarin.Android.Sample/mParticle.Xamarin.Android.Sample.csproj /p:Configuration=Debug /t:Rebuild
msbuild Samples/mParticle.Xamarin.iOS.Sample/mParticle.Xamarin.iOS.Sample.csproj /p:Configuration=Debug /t:Rebuild


# .NET MAUI
# 

# # Restore packages
# dotnet restore

# # Build bindings
# dotnet build Bindings/mParticle.MAUI.AndroidBinding/mParticle.MAUI.AndroidBinding.csproj /p:Configuration=Release /t:Rebuild
# dotnet build Bindings/mParticle.MAUI.iOSBinding/mParticle.MAUI.iOSBinding.csproj /p:Configuration=Release /t:Rebuild

# # Build Libraries
# dotnet build Library/mParticle.MAUI.Abstractions/mParticle.MAUI.Abstractions.csproj /p:Configuration=Release /t:Rebuild
# dotnet build Library/mParticle.MAUI.Android/mParticle.MAUI.Android.csproj /p:Configuration=Release /t:Rebuild
# dotnet build Library/mParticle.MAUI.iOS/mParticle.MAUI.iOS.csproj /p:Configuration=Release /t:Rebuild

# # Build Sample Apps
# dotnet build Samples/mParticle.MAUI.Android.Sample/mParticle.MAUI.Android.Sample.csproj /p:Configuration=Debug /t:Rebuild
# dotnet build Samples/mParticle.MAUI.iOS.Sample/mParticle.MAUI.iOS.Sample.csproj /p:Configuration=Debug /t:Rebuild


# # Package for nuget
# #

# nuget pack Library/mparticle.nuspec
