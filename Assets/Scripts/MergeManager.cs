using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [Header("Settings")]
    Fruit lastSender;

    [Header("Actions")]
    public static Action<FruitType, Vector2> onMergeProcess;
    private void Awake()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitCallback;
    }

    private void OnDestroy()
    {
        Fruit.onCollisionWithFruit -= CollisionBetweenFruitCallback;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    void CollisionBetweenFruitCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
        {
            return;
        }
        lastSender = sender;

        ProcessMerge(sender, otherFruit);

        Debug.Log("Collision detected by: " + sender.name);
    }

    void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType++;
        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position) / 2;

        // xóa 2 fruit cũ
        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);

        StartCoroutine(ResetLastSenderCo());

        // tiến hành sinh fruit mới
        onMergeProcess?.Invoke(mergeFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCo()
    {
        yield return new WaitForEndOfFrame();   // chờ frame đang xử lý kết thúc
        lastSender = null;
    }
}
