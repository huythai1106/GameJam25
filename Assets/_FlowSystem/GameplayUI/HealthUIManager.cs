using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
	[SerializeField] private StatusStackUI heathProgressBar;

	[SerializeField] private float defaultAnimationProgressDuration = 0.5f;

	[Button]
	public void UpdateHealthBar(int value)
	{
		DOTween.Kill(heathProgressBar);

		heathProgressBar.Change(value);

	}
}