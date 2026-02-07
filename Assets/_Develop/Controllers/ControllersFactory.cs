using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersFactory 
{
    public PlayerDirectionalController CreatePlayerDirectionalController(IDirectionalMovable movable, IKeyboardInput input) 
    {
        return new PlayerDirectionalController(movable, input);
    }

    public PlayerRotatableController CreatePlayerRotatableController(IDirectionalRotatable rotatable, IDirectionalMovable movable) 
    {
        return new PlayerRotatableController(rotatable, movable);
    }

    public RandomAIDIrectionalMovableController CreateRandomAIDIrectionalMovableController(
            Vector3 spawnPoint,
            float timeToChangeDirection,
            float leashRadius,
            float returnLockDuration,
            IDirectionalMovable movable)
    {
        return new RandomAIDIrectionalMovableController(
            spawnPoint,
            timeToChangeDirection, 
            leashRadius, 
            returnLockDuration, 
            movable
            );
    }

    public CompositeController CreateMainHeroPlayerController(SimpleCharacter character, IKeyboardInput input)
    {
        return new CompositeController(
            CreatePlayerDirectionalController(character, input),
            CreatePlayerRotatableController(character, character)
            );
    }


    public CompositeController CreateEnemyController(
            Vector3 spawnPoint,
            float timeToChangeDirection,
            float leashRadius,
            float returnLockDuration,
            SimpleCharacter character
            )
    {
        return new CompositeController(
            CreateRandomAIDIrectionalMovableController(
                spawnPoint,
                timeToChangeDirection,
                leashRadius,
                returnLockDuration,
                character),
            CreatePlayerRotatableController(character, character)
            );
    }

}
