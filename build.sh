nuget restore
msbuild Bindings/mParticle.Xamarin.AndroidBinding/mParticle.Xamarin.AndroidBinding.csproj /p:Configuration=Release /t:Rebuild
msbuild Bindings/mParticle.Xamarin.iOSBinding/mParticle.Xamarin.iOSBinding.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin/mParticle.Xamarin.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin.Abstractions/mParticle.Xamarin.Abstractions.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin.Android/mParticle.Xamarin.Android.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.Xamarin.iOS/mParticle.Xamarin.iOS.csproj /p:Configuration=Release /t:Rebuild

msbuild Bindings/mParticle.MAUI.iOSBinding/mParticle.MAUI.iOSBinding.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.MAUI.Abstractions/mParticle.MAUI.Abstractions.csproj /p:Configuration=Release /t:Rebuild
msbuild Library/mParticle.MAUI.iOS/mParticle.MAUI.iOS.csproj /p:Configuration=Release /t:Rebuild

msbuild Samples/mParticle.Xamarin.Android.Sample/mParticle.Xamarin.Android.Sample.csproj /p:Configuration=Debug /t:Rebuild
msbuild Samples/mParticle.Xamarin.Forms.Sample.Droid/mParticle.Xamarin.Forms.Sample.Droid.csproj /p:Configuration=Debug /t:Rebuild
msbuild Samples/mParticle.Xamarin.Forms.Sample.iOS/mParticle.Xamarin.Forms.Sample.iOS.csproj /p:Configuration=Debug /t:Rebuild
msbuild Samples/mParticle.Xamarin.iOS.Sample/mParticle.Xamarin.iOS.Sample.csproj /p:Configuration=Debug /t:Rebuild

nuget pack Library/mparticle.nuspec
