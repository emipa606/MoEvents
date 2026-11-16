# GitHub Copilot Instructions for Mo'Events (Continued)

## Mod Overview and Purpose

Mo'Events (Continued) is an update to the original "ilawz" mod for the game RimWorld. It adds various new events and incidents to enhance the gameplay experience. The mod includes translations for Chinese and French, and allows players to adjust the trigger chances of the events through settings. 

## Key Features and Systems

### Events and Incidents
The mod introduces several new events and incidents, including:
- Abomination: Rescued pawns turn into abominations.
- Amnesia: Random pawns forget their previous tasks.
- Animal Migration: Packs of animals migrate through the area.
- Calm Weather: Weather becomes calm.
- Hungry Insects: Manhunting insects search for food.
- Luxury Cargo Pods: Fallen luxury items from the spaceship.
- Nausea: Random pawns feel sick.
- Stroke: Random pawns lose control of their body.
- Survival Pods: Survival pods with essential items.
- Tribal Aid: Tribes request medical help for their injured members.
- Thanksgiving: Friendly tribes offer spare food.

### Settings
Players can adjust the trigger chances of these events through the mod settings.

### Translation
The mod includes translations for Chinese and French, thanks to the contributions of gast and qux.

## Coding Patterns and Conventions

### C# Code Structure
- **Namespaces**: Use `Mlie.MoEvents` as the primary namespace.
- **Class Names**: Use PascalCase for class names, e.g., `MOIncidentWorker_Amnesia`.
- **Method Names**: Use camelCase for method names, e.g., `TryExecuteWorker`.
- **File Organization**: Organize code into relevant files, e.g., `JobGiver_Segmentation.cs` for job-related classes.

### XML Integration
- **XML Files**: All XML definitions are located in the `Defs` directory under the mod folder.
- **Def Types**: Use appropriate Def types like `IncidentDef`, `JobDef`, `FactionDef`, etc.
- **Attributes and Tags**: Follow the RimWorld XML schema conventions for attributes and tags.

## Harmony Patching

The mod uses Harmony patches to modify vanilla RimWorld behavior. Key Harmony patches include:
- **Amnesia**: Patches the `TryExecute` method to handle amnesia events.
- **Abomination**: Patches methods related to corpse and pawn transformations.
- **Animal Migration**: Patches methods to handle animal migration incidents.

### Patching Conventions
- **Patch Method Naming**: Use descriptive names for patch methods, e.g., `Patch_Amnesia_TryExecute`.
- **Prefix/Suffix**: Use `Prefix` or `Suffix` to modify method behavior.
- **Harmony Instance**: Use a single `Harmony` instance for all patches.

## Suggestions for Copilot

### XML Generation
When working with XML, Copilot can assist in generating new `Def` entries. For example, creating a new `IncidentDef` or `JobDef`.

### C# Code Generation
Copilot can help generate new classes and methods for handling custom events and incidents. For example, creating a new `IncidentWorker` for a new event.

### Harmony Patching
Copilot can assist in generating Harmony patches by suggesting the appropriate method to patch and the type of patch (`Prefix`, `Suffix`, `Transpiler`).

### Settings UI
Copilot can help generate the settings UI for adjusting event trigger chances. This involves creating a `ModSettings` class and a settings UI using RimWorld's `ModOption` system.

## Example Usage

### XML Example
xml
<IncidentDef>
    <defName>AnimalMigration</defName>
    <label>Animal Migration</label>
    <description>A pack of animals is migrating through your area!</description>
    <category>Animal</category>
    <targetCategories>Colony</targetCategories>
    <randomSelectionWeight>0.1</randomSelectionWeight>
    <baseMagnitude>0.5</baseMagnitude>
    <workerClass>MOIncidentWorker_AnimalMigration</workerClass>
</IncidentDef>


### C# Example
csharp
using RimWorld;
using Verse;

namespace Mlie.MoEvents
{
    public class MOIncidentWorker_AnimalMigration : IncidentWorker
    {
        public override bool TryExecute(IncidentParms parms)
        {
            // Code to handle the Animal Migration incident
            return true;
        }
    }
}


### Harmony Patch Example
csharp
using HarmonyLib;
using RimWorld;
using Verse;

namespace Mlie.MoEvents
{
    [HarmonyPatch(typeof(Pawn), "TakeDamage")]
    public static class Patch_Pawn_TakeDamage
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn __instance, DamageInfo dinfo, ref bool absorbedOrReflected)
        {
            // Custom logic for modifying damage behavior
            return true;
        }
    }
}


## Conclusion

Mo'Events (Continued) aims to enhance RimWorld by introducing new and varied events and incidents. By following the coding patterns and conventions outlined above, developers can contribute to and extend the mod effectively. GitHub Copilot can significantly assist in generating and refining the code for XML definitions, C# classes, and Harmony patches.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

