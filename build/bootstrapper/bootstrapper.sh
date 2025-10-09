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
# Check if you are running macos
if sw_vers -productname | grep -q 'macOS' ; then
   echo "✅ You are running on Apple software."
   echo "Trying to select Xcode 16.1"
   sudo xcode-select -s /Applications/Xcode_16.1.app #This is mainly used to force Azure to use a specific Xcode version, Xcode Azure paths are found here: https://github.com/actions/runner-images/blob/macos-14/20240923.101/images/macos/macos-14-Readme.md#xcode
   echo "✅ You are now running on Xcode 16.1"
else
   echo "❌ You are not running on  software. This build system requires you to run on a Mac."
fi

#dotnet-script

if dotnet tool list -g | grep dotnet-script > /dev/null ; then
   echo "✅ dotnet-script was found"
else
   echo "❌ dotnet-script was not found, installing..."
   dotnet tool install -g dotnet-script > /dev/null
   CheckIfInstalledCorrectly "dotnet-script"
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
   echo "Will install MAUI with latest version."
   sudo dotnet workload install maui
   echo "✅ .NET MAUI was installed."
   if [ -f "sdk-versions.json" ]; then
      echo "Will install Android and iOS from sdk-versions.json."
      sudo dotnet workload install android ios --from-rollback-file sdk-versions.json
      echo "✅ Android and iOS were installed from rollback file."
   
   sudo dotnet workload list
fi