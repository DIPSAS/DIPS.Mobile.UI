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
   #maui
   if dotnet workload list  | grep maui > /dev/null ; then
      echo "✅ .NET MAUI was found."
   else
      echo "❌ .NET MAUI was not found, installing..."
      sudo dotnet workload install maui
      echo "✅ .NET MAUI was installed."
   fi
fi
