using UnityEngine;
using System;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public SimpleCharacter CreateCharacter(
        SimpleCharacter prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed,
        int maxHealth)
    { 
        SimpleCharacter instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        DirectionalMover mover;
        DirectionalRotator rotator;
        Health health;

        if (instance.TryGetComponent(out CharacterController characterController))
        {
            mover = new DirectionalMover(characterController, moveSpeed);
            rotator = new DirectionalRotator(instance.transform, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException("Not found mover component");
        }

        health = new Health(maxHealth);

        instance.Init(mover, rotator, health);

        return instance;
    }
}
