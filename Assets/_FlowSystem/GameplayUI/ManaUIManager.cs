using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
public class ManaUIManager : MonoBehaviour
{
    public static ManaUIManager Instance;
    [SerializeField] private float defaultAnimationProgressDuration = 0.5f;

    [SerializeField]
    private List<StatusStackUI> manaStackList;
    [Button]
    public void UpdateManaBar(int value, float duration, Action onComplete = null)
    {
        int currentValue = value;
        for (int i = 0; i < manaStackList.Count; i++)
        {
            if (currentValue >= manaStackList[i].GetMaxUnitCount())
            {
                manaStackList[i].GoMax();
                currentValue -= manaStackList[i].GetMaxUnitCount();
            }
            else
            {
                manaStackList[i].Change(currentValue);
            }
        }
    }

    [Button]
    public void Setup(int numberOfManaStack)
    {
    }

    private void Awake()
    {
        Instance = this;
    }
}
