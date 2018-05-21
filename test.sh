#!/bin/bash
project="src/TestLinks"
echo "Running dotnet restore..."
eval "dotnet restore ${project}"
echo ""
eval "dotnet run --project ${project} $@"
