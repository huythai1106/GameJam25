using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
	[SerializeField] private Slider heathProgressBar;

	[SerializeField] private float defaultAnimationProgressDuration = 0.5f;

	[Button]
	public void UpdateHealthBar(float value, float duration = -1, Action onComplete = null)
	{
		DOTween.Kill(heathProgressBar);

		heathProgressBar.DOValue(value, duration > 0 ? duration : defaultAnimationProgressDuration).OnComplete(() =>
		{
			onComplete?.Invoke();
		});
	}
}