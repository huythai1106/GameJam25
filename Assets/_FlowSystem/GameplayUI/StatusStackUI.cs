using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StatusStackUI : MonoBehaviour
{
	[SerializeField] private float scaleUpDuration;
	[SerializeField] private float scaleDownDuration;
	[SerializeField] private List<StatusUnit> statusUnits;
	[SerializeField] private StatusUnit statusUnitPrefab;


	[Button]
	public void Change(int amount)
	{
		for (int i = 0; i < statusUnits.Count; i++)
		{
			if (i < amount)
			{
				if (statusUnits[i].isActiveHPIcon() == false)
				{

					statusUnits[i].ScaleUpStatusUnit(scaleUpDuration);
				}
			}
			else if (i >= amount && statusUnits[i].isActiveHPIcon() == true)
			{
				statusUnits[i].ScaleDownStatusUnit(scaleDownDuration);

			}
		}

	}



	public void GoMax()
	{
		Change(statusUnits.Count);
	}

	[Button]
	public void SetUp(int unitCount)
	{
		for (int i = 0; i < unitCount; i++)
		{
			var unit = Instantiate(statusUnitPrefab, transform);
			statusUnits.Add(unit);
		}
	}



	public int GetMaxUnitCount()
	{
		return statusUnits.Count;
	}
}
