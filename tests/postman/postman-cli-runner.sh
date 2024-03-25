#!/bin/bash
newman_version=$(npm list -g --depth=0 | grep newman)
echo "var value is: ${newman_version}"
#     sudo npm install -g newman
newman run AirTravel.postman_collection.json