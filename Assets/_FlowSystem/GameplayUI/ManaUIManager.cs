using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ManaUIManager : MonoBehaviour
{
    [SerializeField] private float defaultAnimationProgressDuration = 0.5f;

    [SerializeField] private List<Slider> manaProgressBars;


    [Button]
    public void UpdateManaBar(int index, float value, float duration, Action onComplete = null)
    {
        DOTween.Kill(manaProgressBars[index]);
        manaProgressBars[index].DOValue(value, duration > 0 ? duration : defaultAnimationProgressDuration).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }


}
