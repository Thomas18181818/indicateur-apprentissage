# Thomas_TrainingIndicator – Guide de workflow

Ce document résume l'architecture de l'indicateur d'entraînement, la logique du code, les
étapes pour travailler avec Codex et GitHub, ainsi que la procédure d'installation dans
NinjaTrader 8.

## Architecture rapide

1. **Fichier unique** : `src/Thomas_TrainingIndicator.cs` contient l'intégralité du code.
2. **Fonctions clés** :
   - `OnStateChange` configure l'indicateur (paramètres, instanciation de la SMA).
   - `OnBarUpdate` met à jour la SMA et dessine la ligne horizontale du dernier prix.
3. **Paramètres utilisateurs** :
   - `Period` – longueur de la SMA.
   - `ShowLastPriceLine` – active/désactive la ligne horizontale.

## Workflow Codex

1. **Créer/éditer le fichier** :
   - Ouvrir l'espace de travail souhaité.
   - Cliquer sur `Add file` → `Create new file` ou ouvrir `src/Thomas_TrainingIndicator.cs`.
   - Coller le code complet fourni ci-dessous puis enregistrer (`Ctrl+S`).
2. **Validation** :
   - Utiliser `Run` → `dotnet-format` ou l'action de lint configurée si disponible.
   - Vérifier qu'il n'y a ni erreur ni avertissement.
3. **Extraction** :
   - Utiliser `Download` → `Export workspace` ou `Pull` si Codex est lié à GitHub.
   - Confirmer que le fichier `.cs` est présent et à jour.

## Workflow GitHub

1. `git status` pour vérifier les modifications (`src/Thomas_TrainingIndicator.cs` + docs).
2. `git add src/Thomas_TrainingIndicator.cs docs/Thomas_TrainingIndicator_Workflow.md`.
3. `git commit -m "Ajout de l'indicateur Thomas_TrainingIndicator"`.
4. `git push origin <branche>` pour publier sur GitHub.
5. Ouvrir une Pull Request décrivant brièvement la fonctionnalité.

## Installation dans NinjaTrader 8

1. Copier `Thomas_TrainingIndicator.cs` dans `Documents\NinjaTrader 8\bin\Custom\Indicators`.
2. Ouvrir NinjaTrader → `New` → `NinjaScript Editor`.
3. Dans l'arborescence, faire clic droit → `Compile` (ou appuyer sur `F5`).
4. Une fois la compilation réussie, l'indicateur apparaît dans `Indicators`.

## Modification rapide

- **Changer la période** : ajuster la propriété `Period` dans la fenêtre `Indicators` ou modifier la valeur par défaut dans `State.SetDefaults`.
- **Changer la couleur de la SMA** : adapter la couleur passée à `AddPlot(Brushes.DodgerBlue, ...)`.
- **Désactiver la ligne de prix** : décocher `ShowLastPriceLine` dans les paramètres.
- **Modifier le calcul** : remplacer `trainingSma = SMA(Close, Period);` par un autre calcul ou ajouter des séries supplémentaires.

## Tests simples

1. Charger un graphique (ex. ES 5 min) et appliquer l'indicateur.
2. Vérifier que la SMA suit le prix et change de forme en modifiant `Period`.
3. Basculer `ShowLastPriceLine` pour confirmer l'apparition/disparition de la ligne grise.
4. Lancer le mode `Playback` ou attendre de nouvelles barres pour confirmer la mise à jour en temps réel.
