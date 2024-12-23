#!/bin/bash

# Liste des modèles
models=("Supplier")

# DbContext
dbContext="MyInventoryContext"

# Dossier de sortie
outputDir="Controllers/API"

# Générer un contrôleur pour chaque modèle
for model in "${models[@]}"; do
    echo "Génération du contrôleur pour le modèle : $model"
    dotnet aspnet-codegenerator controller -name "${model}Controller" -async -api -m "$model" -dc "$dbContext" -outDir "$outputDir"
done

echo "Tous les contrôleurs ont été générés avec succès !"

