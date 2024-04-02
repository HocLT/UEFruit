using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitCallback;
    }

    void Update()
    {

    }
    private void CollisionBetweenFruitCallback(Fruit sender, Fruit otherFruit)
    {
        Debug.Log("Collision detected by: " + sender.name);
    }
}
