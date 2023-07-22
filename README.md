# ECS-Digger

https://github.com/ForestSquirrelDev/ECS-Digger/assets/82777171/f91fae33-9323-42a6-be0a-07c6d9c53f21

### What is this?
It's a small game prototype, based on my [custom ECS library](https://github.com/ForestSquirrelDev/PoorMansECS).
### Game flow
1. The game is based on grid of configurable size. 
2. Each cell of the grid has a certain configurable "depth" (which is drawn in game as colored numbers on the cells).
3. Player can "dig" any cell in the grid.
4. When player digs the cell, he reduces his dig attempts and decreases the "depth" of cell. If depth of cell goes to zero, it becomes unavailable for digging. If player has no available digs left, he loses.
5. When player digs the cell, with a configurable chance a gold bar is spawned (yellow rectangles in the video above).
6. When player drags the golden bar to the "bag" (brown-ish square in the bottom left), player collects the golden bar.
7. Once a certain configurable amount of golden bars is collected, player wins.
### Game architecture
1. Game architecture is separated in three parts: Model, View and Input.
2. Business-logic (Model part) is represented with ECS World.
3. View is represented with Unity UGUI.
4. Interactions between View and Model are made with Dependency Inversion principle in mind - both View and Model depend on abstractions - [UIInputEntity](https://github.com/ForestSquirrelDev/ECS-Digger/blob/master/Assets/Core/Model/Entities/SingletonEntities/UIInputEntity.cs) and [commands, attached to it](https://github.com/ForestSquirrelDev/ECS-Digger/tree/master/Assets/Core/Input).
5. Save system uses [fastJSON](https://github.com/mgholam/fastJSON) library to serialize data from ECS World in human-readable format: every entity in the world, that needs to be serialized and deserialized, implements the [corresponding interface](https://github.com/ForestSquirrelDev/ECS-Digger/blob/master/Assets/Core/Model/ISerializable.cs). Save is made automatically every once a configurable period of time. Load is performed during the launch of game.
