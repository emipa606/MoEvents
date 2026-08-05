# Mo'Events (Continued) - GitHub Copilot Instructions

## Mod Overview and Purpose

Mo'Events (Continued) is an enhancement mod for RimWorld, originally created by ilawz and updated by Taranchuk. The purpose of the mod is to enrich the gameplay experience by introducing a variety of new incidents and events, adding complexity and unpredictability to the game. With contributions for translations in Chinese and French, the mod is accessible to a wider audience, enhancing its global appeal.

## Key Features and Systems

- **New Incidents & Events**: A variety of unique incidents such as Abomination, Amnesia, Animal Migration, Calm Weather, and more have been added. These events introduce new challenges and opportunities for players.
- **Event Trigger Settings**: Players can customize the probability of triggering various events, allowing for a tailored gameplay experience.
- **Translation Support**: The mod includes support for multiple languages, including Chinese and French, allowing for localization and broader player engagement.
- **Update of Legacy Code**: Many of the original events have been updated and expanded, ensuring compatibility with the latest versions of RimWorld and enhancing functionality.

## Coding Patterns and Conventions

- **Namespaces and Class Organization**: The C# code is organized into logical namespaces and classes. Common types include `JobGiver_Segmentation`, `MOIncidentWorker_Amnesia`, and others, all of which follow good naming conventions.
- **Method Naming**: Methods within classes are clearly named to describe their functions, such as `TryExecuteWorker` and `FindMeatyPrey`.
- **XML Integration**: XML files outline game definitions for incidents, jobs, factions, and more. Ensure proper XML structure and adherence to RimWorld's schema for seamless integration.

## XML Integration

- **Incident Definitions (`IncidentDef`)**: Located in `IncidentDefs.xml`, these define the blueprint for in-game incidents and are pivotal to adding new events.
- **Job Definitions (`JobDef`)**: In `JobDefs.xml`, job specifics are detailed for unique behaviors upon event occurrences.
- **Faction and Pawn Kind Definitions**: Stored in `Factions.xml` and `PawnKinds.xml`. These define new factions and pawn types introduced by the mod's events.

## Harmony Patching

- **Purpose**: Harmony patches allow for non-destructive changes to RimWorld's base functionality, enabling the mod to inject custom behaviors without altering core files.
- **Implementation**: Use Harmony's `Patch` attribute to target existing methods and apply `Prefix`, `Postfix`, or `Transpiler` as necessary to augment game mechanics.

## Suggestions for Copilot

1. **C# Coding Assistance**: Use Copilot to generate boilerplate code for new incident workers or job drivers by examining existing patterns in your codebase, such as the setup in `MOIncidentWorker_Amnesia`.
   
2. **Optimization Proposals**: Let Copilot propose optimizations for existing methods, possibly enhancing performance or maintainability. For instance, streamline logic in `TryExecuteWorker`.

3. **Enhanced Localization Support**: Copilot can assist in generating new string entries for additional languages, or identifying existing strings that need translation.

4. **XML File Generation**: Copilot can be utilized to draft new XML definitions for events, jobs, and entities, ensuring they adhere to RimWorld's required formats.

5. **Improvement Suggestions**: Use Copilot's suggestions to refactor repetitive code or improve naming conventions for better readability and maintenance.

By following these guidelines and utilizing tools like GitHub Copilot, you can efficiently enhance and extend the functionality of Mo'Events (Continued), bringing a richer experience to RimWorld players. For any specific issues or bugs, consider using the provided Discord channel for support and always provide logs using the Log Uploader tool.

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

