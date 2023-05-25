#!/bin/bash +x


if [[ "$*" != *"skipBootstrap"* ]]
then
    ./build/bootstrapper/bootstrapper.sh
fi

if [ $? -eq 1 ]; then
    echo "❌ Bootstrapper failed. Exiting..."
    exit 1
else
     cd build
     
     dotnet-script build.csx -- "$@"
     
     cd ..
fi