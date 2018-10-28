using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour 
{
	[SerializeField]
	List <GameObject> votes = null;

	// Use this for initialization
	void Start () 
	{
		SpawnManager.OnCitizenFinished += HandleCitizenFinished;

		SpawnManager.OnGameRestarted += HandleCitizenFinished;

		HandleCitizenFinished();
	}

	void HandleCitizenFinished()
	{
		int index = 0;
		foreach(var vote in votes)
		{
			vote.SetActive(index < SpawnManager.HitpointCount);
			index++;
		}
	}
	
}
