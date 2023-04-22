#!/bin/bash +x

echo "🥾 ---- Running bootstrapper ---- 🥾"
# Check if you are running macos
if sw_vers -productname | grep -q 'macOS' ; then
   echo "✅ You are running on  software."
else
   echo "❌ You are not running on  software. This build system requires you to run on a Mac."
fi

#dotnet-script
if dotnet tool list -g | grep dotnet-script > /dev/null ; then
   echo "✅ dotnet-script was found."
else
   echo "❌ dotnet-script was not found, installing..."
   dotnet tool install -g dotnet-script > /dev/null
fi

#maui ios
if dotnet workload list| grep maui-ios > /dev/null ; then
   echo "✅ .NET MAUI iOS was found."
else
   echo "❌ .NET MAUI iOS was not found, installing..."
   sudo dotnet workload install maui-ios
fi

#maui android
if dotnet workload list | grep maui-android > /dev/null ; then
   echo "✅ .NET MAUI Android was found."
else
   echo "❌ .NET MAUI Android was not found, installing..."
   sudo dotnet workload install maui-android
fi