#!/bin/bash +x

# Not used yet
InstallToolWithErrorHandling ()
{
   if $2 > /dev/null ; then
      echo "✅ $1 was found"
   else
      echo "❌ $1 not found, installing it..."
      $3
   if [ $? -eq 1 ]; then
      exit 1
   else
      echo "✅ $1 was installed"
   fi 
fi
}

CheckIfInstalledCorrectly () 
{
   if [ $? -eq 1 ]; then
      exit 1
   else
      echo "✅ $1 was installed"
   fi 
}

echo "🥾 ---- Running bootstrapper ---- 🥾"

# -- x-platform stuff --

# dotnet-script
if dotnet tool list -g | grep dotnet-script > /dev/null ; then
   echo "✅ dotnet-script was found"
else
   echo "❌ dotnet-script was not found, installing..."
   dotnet tool install -g dotnet-script > /dev/null
   CheckIfInstalledCorrectly "dotnet-script"
fi

# -- macOS stuff --

# Check if you are running macos
if [[ "$(uname -s 2>/dev/null)" == "Darwin" ]]; then
   echo "✅ You are running on Apple software."
   echo "Trying to select Xcode 26.2"
   sudo xcode-select -s /Applications/Xcode_26.2.app #This is mainly used to force Azure to use a specific Xcode version, Xcode Azure paths are found here: https://github.com/actions/runner-images/blob/macos-14/20240923.101/images/macos/macos-14-Readme.md#xcode
   echo "✅ You are now running on Xcode 26.2"
else
   echo "❌ You are not running on  software. This build system requires you to run on a Mac."
   exit 0
fi


#azure-CLI
if az > /dev/null ; then
   echo "✅ Azure CLI was found"
else
   echo "❌ Azure CLI not found, installing it..."
   brew update && brew install azure-cli && az devops extension install
   CheckIfInstalledCorrectly "Azure CLI"
fi

#maui
if [[ "$*" != *"skipMAUIBootstrap"* ]]
then
   echo "Will install MAUI."
   sudo dotnet workload install maui > /dev/null 2>&1
   echo "✅ .NET MAUI was installed."
   #sudo dotnet workload list
   #Uncomment the lines below to enforce installing specific android and/or ios sdk versions from sdk-versions.json
   #echo "Will make sure we install correct sdk from sdk-version.json."
   #sudo dotnet workload install android ios --from-rollback-file sdk-versions.json
   #echo "✅ Android and iOS was installed."
   #sudo dotnet workload list
fi