#!/bin/bash
project="src/TestLinks"
script=$(basename $0)
error="$script: dotnet CLI not installed. See https://github.com/dotnet/cli/blob/master/README.md"
command -v dotnet >/dev/null || echo $error >&2 && exit 1
which dotnet >/dev/null || echo $error >&2 && exit 1
echo Running dotnet restore...
$(dotnet restore $project)
echo
$(dotnet run --project ${project} $@)
