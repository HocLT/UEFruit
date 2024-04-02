using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] FruitType fruitType;
    public static Action<Fruit, Fruit> onCollisionWithFruit;
    void Start()
    {

    }

    void Update()
    {

    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveTo(Vector2 targetPosistion)
    {
        transform.position = targetPosistion;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Fruit otherFruit))
        {
            onCollisionWithFruit?.Invoke(this, otherFruit);
        }
    }
}
