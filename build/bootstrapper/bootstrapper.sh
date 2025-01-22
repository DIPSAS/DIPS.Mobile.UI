#!/bin/bash +x

echo "🥾 ---- Running bootstrapper ---- 🥾"

# Check if you are running macos
if ! type sw_vers &> /dev/null; then
  echo "🪟 You are probably running on Windows"
elif sw_vers -productname | grep -q 'macOS'; then
  echo "✅ You are running on  software."
  if sudo xcode-select -p | grep -q '16.1'; then
     echo "✅ You are running on Xcode 16.1"
  else
     echo "Trying to select Xcode 16.1"
     sudo xcode-select -s /Applications/Xcode_16.1.app
     echo "✅ You are now running on Xcode 16.1"
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
   if dotnet workload list  | grep maui > /dev/null ; then
      echo "✅ .NET MAUI was found."
   else
      echo "Will install MAUI."
      sudo dotnet workload install maui-android maui-ios
      echo "✅ .NET MAUI was installed."
      echo "Will make sure we install correct sdk from sdk-version.json."
      sudo dotnet workload install ios --from-rollback-file sdk-versions.json
      echo "✅ iOS SDK was pinned."
      sudo dotnet workload list
   fi
fi

# Save it for later if we need to update
# PIN TO SPECIFIC GH CLIENT DUE TO BUG WHEN CREATING PRs
# Construct the download URL
# GH_URL="https://github.com/cli/cli/releases/download/v2.57.0/gh_2.57.0_macOS_arm64.zip"

# # Download and extract the GitHub CLI
# echo "Downloading GitHub CLI version 2.57.0 from $GH_URL..."
# curl -L -o gh.tar.gz "$GH_URL"
# echo "Extracting the GitHub CLI..."
# tar -xzf gh.tar.gz

# cd gh_2.57.0_macOS_arm64 || exit 1
# chmod 777 bin/gh
# mv bin/gh ../.
# cd ..
# rm -rf gh_2.57.0_macOS_arm64
# rm -rf gh.tar.gz