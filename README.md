# SlimeRancherButWorse
Imagine stepping into a vibrant, otherworldly landscape filled with bouncy, colorful creatures just waiting to be discovered. In this report, I’ve reimagined the core elements of Slime Rancher, an adventure game that combines exploration, strategy, and a bit of whimsy. The game's objective is for the players to capture and care for various types of slimes, feed them, and collect the valuable "plorts" they produce. These plorts can either be traded for coins or offered to the boss slime.

Player Scripts: The Player script manages player movement based on user input by using a Character Controller in combination with physics-driven movement to apply velocity and move the player. 

The Telekinesis script controls the gun's telekinetic function. A ray is sent to the center of the screen; if it hits an object within range that has a Rigidbody, the object is set as the target. Based on user input, it can either pull the object closer by applying a dragging force or push it away by applying a force in the opposite direction. A Particle System enhances the visuals by generating a spiral particle effect based on the applied force, moving either towards or away from the gun. This effect adds dynamic visual feedback to interactions.

The Interaction script manages interactions with slimes, plorts, and carrots, as well as with the UI. It checks for collisions with specific tags, updating the UI accordingly. Additionally, when an object is released or shot, it instantiates a new object, applies forward force, and updates the UI to reflect the number of items collected.

Cage Structure: The cage, constructed with planes, allows the player to shoot slimes into it and freely enter or exit, while the slimes can only enter and are prevented from escaping. The cage is made using a basic Unity material with transparency for added visibility.

Slimes: The AI movement of slimes, particularly rabbit slimes, is managed by AIMovementScripts. These scripts control the wandering, detection, and target approach. Wandering behavior is achieved with a coroutine alternating between walking and rotating for random durations. Another coroutine makes the slimes jump at random intervals by applying force, giving them a lively appearance. 

The BossSlime script tracks the number of plorts consumed by the boss slime, and upon reaching a set maximum, it destroys the slime and spawns multiple spheres. These spheres are launched in various directions with random forces, creating an explosive effect. Slimes also have an animation that scales them in a loop, enhancing their lifelike appearance when combined with their movement.

Vending Machine: The vending machine operates with an Exchange script that checks for specific tags on objects. If the check is successful, the object is destroyed, and the UI updates to reflect the player’s coin count. The vending machine itself is constructed using Unity primitives and employs built-in Unity materials for a straightforward design.
