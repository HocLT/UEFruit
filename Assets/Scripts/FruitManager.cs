using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] LineRenderer fruitSpawnLine;

    GameObject currentFruit;

    [Header("Settings")]
    [SerializeField] float fruitYSpawnPosition;

    void Start()
    {
        HideLine();
    }

    void Update()
    {
        ManagePlayerInput();
    }

    void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseDragCallback();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUpCallback();
        }


    }

    void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();
    }

    void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();
        currentFruit.transform.position = GetSpawnPosition();
    }

    void MouseUpCallback()
    {
        HideLine();
        currentFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    // lấy tọa độ chuột trong game world
    Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 pos = GetClickedWorldPosition();
        pos.y = fruitYSpawnPosition;
        return pos;
    }

    void SpawnFruit()
    {
        Vector2 pos = GetSpawnPosition();
        currentFruit = Instantiate(fruitPrefab, pos, Quaternion.identity);
    }

    void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }
}
