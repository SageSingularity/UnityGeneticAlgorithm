# UnityGeneticAlgorithm
One implementation of genetic algorithms using the Unity3D game engine.

# Table of Contents
- [How to Play](#how-to-play)
- [How it Works](#how-it-works)
  - [PopulationManager Script](#populationmanager-script)
  - [Chromosome Script](#chromosome-script)

## How to Play
Click on the individual colored circles on the screen in order to 'Eliminate' them. The algorithm focuses on the color of the circles, and After ten seconds, or once all of the circles have been clicked, a new generation is created based on the entities that survived the longest. The top half of individuals will each breed to create two children, and the new generation will populate the screen.

## How it Works
Each spawned entity has it's own 'Chromosome' script, that contains its 'DNA'. In this case, the chosen attributes are the Red Green Blue (RGB) values of color for the Unity3D sprite renderer. As the player clicks on each circle, the time of the click is recorded and that circle/entity is 'Killed'. The top 50% of individuals that survive are then allowed to breed the next generation of circles.

The PopulationManager.cs script is the 'Manager' for the generations, and is attached to an empty game object. The Chromosome.cs script on the other hand is attached to the prefab used when instantiating a new entity, and ends up attached to each individual as an instance in order to keep track of each individuals RGB values and length of survival time. It is accessed by the PopulationManager.

### PopulationManager Script
This script controls the following:
- Spawns the initial population with random RGB values
- Displays Generation count and elapsed time on the screen
- Takes the list of individuals at the end of each generation, and creates a sorted list based on how long each entity survived
- Uses the sorted list to breed the next generation; two offspring are created per parent, in order to end up with the same population as we started

The core genetic algorithm is within the Breed() function of PopulationManager.cs and functions like so (Pseudo-code):

```
if a number in the range 0-4 has been generated:
- Offspring.Red = Parent_1.Red
else if a number in the range 5-10 has been genereated:
- Offspring.Red = Parent_2.Red

if a number in the range 0-4 has been generated:
- Offspring.Green = Parent_1.Green
else if a number in the range 5-10 has been genereated:
- Offspring.Green = Parent_2.Green

if a number in the range 0-4 has been generated:
- Offspring.Blue = Parent_1.Blue
else if a number in the range 5-10 has been genereated:
- Offspring.Blue = Parent_2.Blue
```

As you can see from the above, each offspring has a 50% chance of inheriting a color value from Parent_1 and a 50% chance of inheriting a color value from Parent_2.

### Chromosome Script
This script controls the following:
- The length of the lifespan for the entity it is attached to
- RGB values for the entity it is attached to
- Detects collision based on the player 'Clicking' on the entity on the screen, and 'Kills' it once it's been clicked on
