using System;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Fruit[] fruitPrefabs;
    [SerializeField] Fruit[] spawnableFruits;
    [SerializeField] LineRenderer fruitSpawnLine;

    Fruit currentFruit;

    [Header("Settings")]
    [SerializeField] float fruitYSpawnPosition;
    [SerializeField] Transform fruitsParent;

    bool canManage;     // default: false
    bool isControlling;

    [Header("Actions")]
    public static Action onNextFruitIndexSet;

    [Header("Next Fruit Settings")]
    [SerializeField] int nextFruitIndex;

    private void Awake()
    {
        MergeManager.onMergeProcess += MergeProcessCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcess -= MergeProcessCallback;
    }

    void Start()
    {
        SetNextFruitIndex();
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
        currentFruit = Instantiate(spawnableFruits[nextFruitIndex], pos, Quaternion.identity, fruitsParent);

        SetNextFruitIndex();    // sinh ra fruit mới
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

    void MergeProcessCallback(FruitType fruitType, Vector2 spawnPosition)
    {
        // tìm Fruit tương ứng với FruitType
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType() == fruitType)
            {
                // sinh fruit mới
                SpawnMergeFruit(fruitPrefabs[i], spawnPosition);
                break;
            }
        }
    }

    void SpawnMergeFruit(Fruit fruit, Vector2 spawnPosition)
    {
        Fruit fruitInstance = Instantiate(fruit, spawnPosition, Quaternion.identity, fruitsParent);
        fruitInstance.EnablePhysics();
    }

    void SetNextFruitIndex()
    {
        nextFruitIndex = UnityEngine.Random.Range(0, spawnableFruits.Length);
        onNextFruitIndexSet?.Invoke();
    }

    public Sprite GetNextFruitSprite()
    {
        return spawnableFruits[nextFruitIndex].GetSprite();
    }
}
