# GitHub Copilot Instructions for RimWorld Mod Development

## Mod Overview and Purpose
This mod, "MoreIncidents", is designed to expand and enrich the gameplay experience of RimWorld by introducing new incidents and events. These incidents add unpredictability and depth to the narrative, offering players unique challenges and opportunities as they manage their colony in a dynamic world.

## Key Features and Systems
- **Incident Workers**: Classes like `MOIncidentWorker_Amnesia`, `MOIncidentWorker_GoodRest`, and `MOIncidentWorker_Insect` introduce new events to the game. Each worker decides the conditions under which the event can occur and what happens when it does.
- **Job Drivers**: Classes such as `JobDriver_CrushDoor`, `JobDriver_Kill`, and `JobDriver_Segmentation` implement specialized pawn behaviors, defining tasks and how they should be executed.
- **World Components**: The `WorldComp` class represents global game state changes and interactions at the world level.
- **Utility Classes**: `Mo_Utility` and `theThing_Utility` provide general-purpose helper methods that support larger systems within the mod.
- **DefOf Classes**: `MODefOf` is a static class used to define constants for various game definitions to avoid hardcoded strings, improving code maintainability and readability.

## Coding Patterns and Conventions
- **Naming Conventions**: Classes and methods are named using PascalCase. Classes convey their purpose explicitly, such as `IncidentWorker`, `JobDriver`, and `Utility`.
- **Class Structure**: Each class addresses a specific part of the game's system, using clear accesses such as `public` and `internal` appropriately.
- **Error Handling**: Consider using try-catch blocks within critical methods to handle and log exceptions that might occur during gameplay.
- **Method Structure**: Methods are well-defined with a clear single responsibility, making it easy to understand their purpose and function within the game logic.

## XML Integration
- **Patch XMLs**: Although XML parsing errors noted, it is vital to maintain well-structured XML files for data-driven configurations, such as defining incidents and jobs without recompiling code.
- **Error Handling**: Review XML files for correct formatting and ensure all necessary elements and attributes are explicitly defined to avoid parsing errors.

## Harmony Patching
- **Patch Usage**: Use the Harmony library to modify and extend game behavior safely. Consider annotations like `[HarmonyPatch]` to target specific methods or classes in the base game.
- **Non-invasive Edits**: Ensure patches respect compatibility by using Postfix and Prefix methods to alter or extend game methods without disrupting other mods.
- **Performance Considerations**: Implement conditional checks within patches to minimize the impact on game performance, especially in frequently called methods.

## Suggestions for Copilot
- **Method Stubs**: Utilize Copilot for generating method stubs, especially for new incident types or utility methods.
- **Refactoring Suggestions**: Suggest refactoring opportunities for existing methods to simplify logic or split complex methods into more modular components.
- **Validation Tasks**: Propose validation tasks for user inputs and new game settings to prevent runtime errors.
- **Documentation Comments**: Use Copilot to automatically generate XML documentation comments for public methods and classes to enhance codebase readability.
- **Harmony Patch Templates**: Leverage Copilot to create templates for standard Harmony patches, ensuring consistent structure across different patches.

By streamlining these elements and fostering a clean, efficient coding environment, you are better positioned to develop engaging and complex mods for the RimWorld community.
