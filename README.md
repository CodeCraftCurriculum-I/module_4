Welcome to "Adventure," your opportunity to bring your imagination to life in a text-based role-playing game (RPG)! Reminiscent of the classic game Zork, Adventure offers an open platform where you can create your own narratives, environments, characters, and challenges. However, despite the seemingly limitless possibilities, the foundational concepts of programming still form the backbone of this project.

## Core Programming Concepts in "Adventure"

### Variables

In "Adventure," variables play a vital role in defining various game elements such as player attributes, game states, item descriptions, locations, and more. The manipulation and interaction of these variables drive the narrative and gameplay.

### Conditionals

Conditionals breathe life into the game, allowing for different outcomes and paths based on player actions. From simple decisions like choosing a path to complex actions like solving puzzles, conditionals make these narrative twists possible.

### Arrays

Arrays can hold collections of game elements such as inventory items or locations. This can provide a more dynamic, immersive experience as players navigate through the game world.

### Loops

Loops keep the game running, enabling the player to continuously interact with the game world until they reach a specified end condition.

### Functions

Functions streamline the game's complexity by breaking down tasks into manageable, reusable pieces. Functions can handle player actions, update game states, display narrative text, and much more.

## Scope and Complexity

"Adventure" is a more complex project, not in terms of new concepts, but in terms of scope. It's easy to get lost in the endless possibilities and end up with scope creep—a situation where the game's features continue to expand beyond the original plan. It's a common challenge in game development, but it's also an opportunity for you to learn how to manage your ideas, prioritize features, and define a clear, achievable scope for your project.

> **Note**: While the possibilities for your game are only limited by your imagination, the importance of good, clean code remains. As your projects get more complex, maintainable and understandable code becomes even more crucial.

## Project Requirements

You must work from the code in [GitHub Repository](https://github.com/CodeCraftCurriculum-I/module_4_adventure).

Before you write any code, you should "sketch" the pseudo code and make a flowchart for how you plan to do the following alterations to the game.

### Module Requirements

- **TODOS**: Do all the things that are tagged with `///TODO:` in the code.
- **Inventory**: Implement an inventory for the player, and the functionality for adding and removing items.
- **More Locations**: Implement a minimum of two more locations for the player to travel to.
- **Items**: Create at least one artifact that needs to be combined with another artifact to make a change (for instance, a key and lock).

### Challenge Requirements (Higher Grades)

- **Cheat Codes**: Implement a minimum of two cheat codes (teleport is a good idea).
- **Duration Effects**: Implement a system that enables recurring effects (example: poison debuff, scared, prone, blurry vision, etc.).
- **Update Location Description**: When an item (e.g., the key in the start room) is added to the player's inventory, it should not be part of the room description anymore.
- **Keep State**: When a player comes back to a location (if you allow the player to revisit), the location should have the changes that the player created previously still be there.

### HARD Requirements

- Implement save game and restore functionality.

---

## Learning Objectives

Throughout this module, you should aim to understand how different parts of a program interact, how to troubleshoot effectively, and how to write clean, effective code.

### Assessment

Assessment for this project will be based not just on feature completion, but also on your attention to detail, the cleanliness and readability of your code, and the thoughtfulness of your README file reflections.

### File Structure

All your code and related files should be neatly organized in a Zip file, with the following internal structure:

```
module_4_Adventure
├── Adventure.cs
├── Game.cs
├── Location.cs
├── Item.cs
├── *.*
└── readme.md
```

This is your opportunity to demonstrate not just what you've learned, but how you can apply it creatively and effectively in a real programming project.
