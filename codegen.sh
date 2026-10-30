#!/bin/bash

if [ $# -eq 0 ]; then
    echo "Usage: $0 <filename1> <filename2> ... <filenameN>"
    echo "Example: $0 MyCode AnotherFile ThirdFile"
    echo "This will create .cs, .fs, and .vb files for each filename"
    exit 1
fi

cd "${INIT_CWD:-$PWD}" || exit 1
current_dir="$(pwd)"
created_files=()

for filename in "$@"; do
    touch "${filename}.cs"
    touch "${filename}.fs"
    touch "${filename}.vb"
    
    created_files+=("  ${filename}.cs")
    created_files+=("  ${filename}.fs")
    created_files+=("  ${filename}.vb")
done

echo "Created files in $current_dir:"
printf '%s\n' "${created_files[@]}"
