using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
	[SerializeField] private Slider heathProgressBar;

	[SerializeField] private float animationProgressDuration = 0.5f;

	public void UpdateHealthBar(float value, Action onComplete = null)
	{
		heathProgressBar.DOValue(value, animationProgressDuration).OnComplete(() =>
		{
			onComplete?.Invoke();
		});
	}
}