#!/bin/bash +x

echo "🥾 ---- Running bootstrapper ---- 🥾"

# Check if you are running macos
if ! type sw_vers &> /dev/null; then
  echo "🪟 You are probably running on Windows"
elif sw_vers -productname | grep -q 'macOS'; then
  echo "✅ You are running on  software."
  if sudo xcode-select -p | grep -q '15.0.1'; then
     echo "✅ You are running on Xcode 15.0.1"
  else
     echo "Trying to select Xcode 15.0.1"
     sudo xcode-select -s /Applications/Xcode_15.0.1.app
     echo "✅ You are now running on Xcode 15.0.1"
  fi
else
  echo "🐧 Is this a penguin I see in the distance?"
fi

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
      sudo dotnet workload install maui-android maui-ios
      echo "✅ .NET MAUI was installed."
   fi
fi
