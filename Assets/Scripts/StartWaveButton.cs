using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour 
{
    public AudioSource ambiance;
    public AudioSource button;
    public AudioSource crowdboo;

	[SerializeField]
	List <Sprite> bubbles = null;

	Image image = null;

	// Use this for initialization
	void Start () 
	{
		SpawnManager.OnWaveSpawned += HandleWaveSpawned;

		SpawnManager.OnWaveEnded += HandleWaveEnded;

		SpawnManager.OnGameRestarted += HandleGameRestarted;

		image = GetComponent<Image>();
	}
	
	public void Press()
	{
		SpawnManager.SpawnWave();

        ambiance.Play();
        button.Play();
        crowdboo.Play();
	}

	void HandleWaveSpawned()
	{
		gameObject.SetActive(false);
	}

	void HandleWaveEnded()
	{
		if(!SpawnManager.HasWavesLeft)
			return;

		gameObject.SetActive(true);

		var sprite = bubbles[SpawnManager.CurrentWaveIndex];
		if(sprite != null)
		{
			image.sprite = sprite;
			image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		else
		{
			image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
	}

	void HandleGameRestarted()
	{
		image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		
		gameObject.SetActive(true);
	}
}
