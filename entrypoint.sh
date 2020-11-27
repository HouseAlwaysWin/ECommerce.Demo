#!/bin/bash


set -e
export PATH="$PATH:$HOME/.dotnet/tools/"

until dotnet ef database update -p API; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"

# urls domain must be 0.0.0.0, to expose ports to outside
run_cmd="dotnet watch -p API run --urls=https://0.0.0.0:5001/"
exec $run_cmd