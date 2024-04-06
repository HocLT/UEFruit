using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitManagerUI : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] Image nextFruitImage;

    FruitManager fruitManager;

    private void Awake()
    {
        FruitManager.onNextFruitIndexSet += NextFruitIndexCallback;
        fruitManager = GetComponent<FruitManager>();
    }

    private void OnDestroy()
    {
        FruitManager.onNextFruitIndexSet -= NextFruitIndexCallback;
    }

    void NextFruitIndexCallback()
    {
        nextFruitImage.sprite = fruitManager.GetNextFruitSprite();
    }


}
