#!/bin/bash

# List of models
models=("Product" "Category" "StockOut" "StockOutItem" "StockEntry" "StockEntryItem" "User" "Supplier" )

# DbContext
dbContext="MyInventoryContext"

# Output directory
outputDir="Controllers/API"

# Generate controller for each model
for model in "${models[@]}"; do
    echo "Generating controller for model: $model"
    dotnet aspnet-codegenerator controller -name "${model}Controller" -async -api -m "$model" -dc "$dbContext" -outDir "$outputDir"
done

echo "All controllers have been generated successfully!"
