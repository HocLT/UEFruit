using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject fruitPrefab;

    [Header("Settings")]
    [SerializeField] float fruitYSpawnPosition;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManagePlayerInput();
        }
    }

    void ManagePlayerInput()
    {
        Vector2 pos = GetClickedWorldPosition();
        pos.y = fruitYSpawnPosition;
        Instantiate(fruitPrefab, pos, Quaternion.identity);
    }

    Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
