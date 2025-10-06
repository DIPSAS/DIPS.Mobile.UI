#!/bin/bash +x


./build/bootstrapper/bootstrapper.sh $*

if [ $? -eq 1 ]; then
    echo "❌ Bootstrapper failed. Exiting..."
    exit 1
else
     cd build
     
     dotnet-script AwesomeBuildsystem.csx -- "$@"
     
     cd ..
fi