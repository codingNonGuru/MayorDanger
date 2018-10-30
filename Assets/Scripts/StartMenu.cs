using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour 
{
	[SerializeField]
	List<Sprite> textures = null;

	[SerializeField]
	Image title = null;

	[SerializeField]
	GameObject healthCounter;

	[SerializeField]
	GameObject nextButton;

	[SerializeField]
	GameObject infoScreen;

	int textureIndex = 0;
	
	// Update is called once per frame
	void Update () 
	{
		title.sprite = textures[textureIndex / 3];

		textureIndex++;
		if(textureIndex == textures.Count * 3)
			textureIndex = 0;
	}

	public void PressPlay()
	{
		gameObject.SetActive(false);

		healthCounter.SetActive(true);

		nextButton.SetActive(true);

		SpawnManager.Restart();
	}

	public void PressInfo()
	{
		gameObject.SetActive(false);

		infoScreen.SetActive(true);
	}

	public void OnEnable()
	{
		healthCounter.SetActive(false);

		nextButton.SetActive(false);
	}
}
