#! /bin/bash

docfx
cd docs-site
sed -i 's/https:\/\/github.com\/mavfi-foundation/https:\/\/github.com\/mavfi-foundation\/source-generators/g' projects/source-generators/public/main.js
docfx
