#!/bin/bash +x

echo "🥾 ---- Running bootstrapper ---- 🥾"
#dotnet-script
if dotnet tool list -g | grep dotnet-script > /dev/null ; then
   echo "✅ dotnet-script was found."
else
   echo "❌ dotnet-script was not found, installing..."
   dotnet tool install -g dotnet-script > /dev/null
   echo "✅ dotnet-script was installed."
fi

if [[ "$*" != *"skipMAUIBootstrap"* ]]
then

   #maui ios
   if dotnet workload list  | grep maui-ios > /dev/null ; then
      echo "✅ .NET MAUI iOS was found."
   else
      echo "❌ .NET MAUI iOS was not found, installing..."
      sudo dotnet workload install maui-ios
      echo "✅ .NET MAUI iOS was installed."
   fi

   #maui android
   if dotnet workload list  | grep maui-android > /dev/null ; then
      echo "✅ .NET MAUI Android was found."
   else
      echo "❌ .NET MAUI Android was not found, installing..."
      sudo dotnet workload install maui-android
      echo "✅ .NET MAUI Android was installed."
   fi

fi