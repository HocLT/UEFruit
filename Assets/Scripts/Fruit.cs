using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] FruitType fruitType;
    public static Action<Fruit, Fruit> onCollisionWithFruit;

    [Header("Elements")]
    [SerializeField] SpriteRenderer spriteRenderer;

    bool hascollided;
    bool canMerged;
    void Start()
    {
        Invoke("AllowMerge", 0.5f);
    }

    void Update()
    {

    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

    public void MoveTo(Vector2 targetPosistion)
    {
        transform.position = targetPosistion;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ManageCollision(other);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        ManageCollision(other);
    }

    public FruitType GetFruitType() 
    {
        return fruitType;
    }

    void ManageCollision(Collision2D other) 
    {
        hascollided = true;

        if (!canMerged)
        {
            return;
        }

        if (other.collider.TryGetComponent(out Fruit otherFruit))
        {
            if ( otherFruit.GetFruitType() != fruitType) {
                return;
            }

            if (!otherFruit.canMerged) 
            {
                return;
            }

            onCollisionWithFruit?.Invoke(this, otherFruit);
        }
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public bool Hascollided()
    {
        return hascollided;
    }

    public bool CanBeMerged()
    {
        return canMerged;
    }

    void AllowMerge()
    {
        canMerged = true;
    }
}
