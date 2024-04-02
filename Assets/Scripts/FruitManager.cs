using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Fruit[] fruitPrefabs;
    [SerializeField] LineRenderer fruitSpawnLine;

    Fruit currentFruit;

    [Header("Settings")]
    [SerializeField] float fruitYSpawnPosition;

    bool canManage;     // default: false
    bool isControlling;

    void Start()
    {
        canManage = true;
        HideLine();
    }

    void Update()
    {
        if (canManage)
        {
            ManagePlayerInput();
        }
    }

    void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            if (isControlling)
            {
                MouseDragCallback();
            }
            else
            {
                MouseUpCallback();
            }

        }
        if (Input.GetMouseButtonUp(0) && isControlling)
        {
            MouseUpCallback();
        }
    }

    void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnFruit();
        isControlling = true;
    }

    void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();
        currentFruit.MoveTo(GetSpawnPosition());
    }

    void MouseUpCallback()
    {
        HideLine();
        currentFruit.EnablePhysics();

        canManage = false;
        StartControlTimer();

        isControlling = false;
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
        currentFruit = Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], pos, Quaternion.identity);
    }

    void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }

    void StartControlTimer()
    {
        Invoke("StopControlTimer", 0.5f);
    }

    void StopControlTimer()
    {
        canManage = true;
    }
}
