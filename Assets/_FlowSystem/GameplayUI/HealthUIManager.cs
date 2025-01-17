using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
	[SerializeField] private Slider heathProgressBar;

	[SerializeField] private float defaultAnimationProgressDuration = 0.5f;

	public void UpdateHealthBar(float value, float duration = -1, Action onComplete = null)
	{
		heathProgressBar.DOValue(value, duration > 0 ? duration : defaultAnimationProgressDuration).OnComplete(() =>
		{
			onComplete?.Invoke();
		});
	}
}