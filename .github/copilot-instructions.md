# GitHub Copilot Instructions for Mo'Events (Continued)

## Mod Overview and Purpose
**Mo'Events (Continued)** is an updated version of the original mod by ilawz. The update was primarily carried out by Taranchuk, with added translations from gast and qux. This mod enriches the RimWorld experience by introducing a variety of new events and incidents that enhance gameplay unpredictability and offer new narrative opportunities for players. Whether you're managing unexpected transformations in rescued pawns or dealing with random acts of generosity from friendly tribes, Mo'Events adds excitement and complexity to the world of RimWorld.

## Key Features and Systems
- **Abomination Event:** Similar to a rescue pod event, the rescued person turns into an Abomination after a while. They can transform corpses into more Abominations, making them particularly challenging to deal with.
- **Amnesia Event:** A random pawn forgets their current task, causing them to abandon their current work and start fresh.
- **Animal Migration Event:** A pack of animals migrates through your colony, which can be hunted, tamed, or ignored.
- **Calm Weather Event:** Calm weather decreases wind turbine efficiency but improves shooting accuracy.
- **Hungry Insects Event:** Starving, manhunting insects invade the area, seeking food.
- **Luxury Cargo Pods Event:** Chunks from a luxurious part of a spaceship crash nearby, containing valuables and possibly survivors.
- **Nausea and Stroke Events:** Random pawns may suddenly become sick or incapacitated, requiring immediate attention.
- **Survival Pods Event:** Survival pods land nearby with essential supplies and weapons.
- **Tribal Aid and Thanksgiving Events:** Friendly tribes offer assistance or gifts under certain conditions, impacting faction relations.

## Coding Patterns and Conventions
- **C#** is used for coding the main mod functionality. The mod structure follows RimWorld's modding framework using `Mod`, `IncidentWorker`, `Pawn`, and `JobDriver` classes.
- **Classes:** Generally use public access for core game integration (`IncidentWorker` and other high-level game interactions), with internal or private access for helper methods and classes.
- **Methods:** Employ descriptive names indicating their action and context, such as `TryExecute`, `BashIt`, and `SpawnRandomMigrationAt`.
  
## XML Integration
- XML files define data-driven aspects such as translations and writeable strings for events and incidents.
- Strings are made available for localization, allowing for translations (currently Chinese and French).

## Harmony Patching
- Use **Harmony** for runtime modifications of the game code, ensuring compatibility and reducing invasive changes to the game's codebase.
- Maintain clear separation between original game logic and modifications through modular patches.
  
## Suggestions for Copilot
- **Localization and Strings:** Generate suggestions for missing translations and identify hardcoded strings that should be externalized for easier localization.
- **Event Logic:** Assist in creating new event logic by integrating existing patterns (public `IncidentWorker` methods) and suggesting new interactions or consequences.
- **Code Optimization:** Identify opportunities within existing methods for optimization, such as reducing complexity in `TryExecute` logic.
- **Harmony Improvements:** Suggest additional Harmony patches to fix bugs or improve compatibility between mods.
- **Testing and Debugging:** Suggest methods for efficient testing strategies, such as creating debugging utilities to simulate events and incidents.

For any bugs or suggestions, feel free to reach out via the mod's GitHub repository or the designated support Discord channel.
