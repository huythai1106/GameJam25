using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StatusStackUI
{
	[SerializeField] private float scaleUpDuration;
	[SerializeField] private float scaleDownDuration;
	[SerializeField] private List<StatusUnit> statusUnits;


	[Button]
	public void Change(int amount)
	{
		for (int i = 0; i < statusUnits.Count; i++)
		{
			if (i < amount)
			{
				if (statusUnits[i].gameObject.activeSelf == false)
				{

					statusUnits[i].ScaleUpStatusUnit(scaleUpDuration);
				}
			}
			else if (i >= amount && statusUnits[i].gameObject.activeSelf == true)
			{
				statusUnits[i].ScaleDownStatusUnit(scaleDownDuration);

			}
		}

	}

	public void GoMax()
	{
		Change(statusUnits.Count);
	}

	public void SetUp(int unitCount)
	{

	}



	public int GetMaxUnitCount()
	{
		return statusUnits.Count;
	}
}


public class StatusUnit : MonoBehaviour
{

	[SerializeField] private Image healthIcon;

	public void ScaleUpStatusUnit(float duration)
	{
		healthIcon.gameObject.SetActive(true);
		DOTween.Kill(healthIcon);
		healthIcon.transform.DOScale(1, duration);
	}

	public void ScaleDownStatusUnit(float duration)
	{
		DOTween.Kill(healthIcon);
		healthIcon.transform.DOScale(0, duration).OnComplete(() =>
		{
			healthIcon.gameObject.SetActive(false);
		});
	}
}
