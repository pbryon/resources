#!/bin/bash

files=$(git diff-index --cached --name-only --diff-filter=ACMR HEAD | grep topics)
error=0
if [ ${#files} -eq 0 ]; then
    exit 0
fi

echo Link checker - pre-commit hook
./test.sh $files 1>/dev/null
if [ $? -ne 0 ]; then
    exit 1
else
    echo "--> links OK!"
    exit 0
fi