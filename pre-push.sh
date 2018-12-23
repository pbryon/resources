#!/bin/bash

echo Link checker - pre-push hook
./test.sh --minimal --debug
if [ $? -ne 0 ]; then
    exit 1
else
    echo "--> links OK!"
    exit 0
fi
