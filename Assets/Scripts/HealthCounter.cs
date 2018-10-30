using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour 
{
	[SerializeField]
	List <Vote> votes = null;

	[SerializeField]
	Sprite happyNegoSprite = null;

	[SerializeField]
	Sprite sadNegoSprite = null;

	Image image = null;

	// Use this for initialization
	void Start () 
	{
		image = GetComponent<Image>();

		SpawnManager.OnCitizenFinished += HandleCitizenFinished;

		SpawnManager.OnGameRestarted += HandleGameRestarted;
	}

	void HandleCitizenFinished()
	{
		votes[SpawnManager.HitpointCount].FadeOut();

		image.sprite = SpawnManager.HitpointCount > 5 ? happyNegoSprite : sadNegoSprite;
	}

	void HandleGameRestarted()
	{
		foreach(var vote in votes)
		{
			vote.Enable();
		}

		image.sprite = SpawnManager.HitpointCount > 5 ? happyNegoSprite : sadNegoSprite;
	}	
}
