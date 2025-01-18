using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
