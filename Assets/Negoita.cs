﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negoita : MonoBehaviour 
{
    public AudioSource ParrotTrigger;
    public AudioSource Footsteps;

	[SerializeField]
	GameObject parrotPrefab = null;

	[SerializeField]
	Material material = null;

	[SerializeField]
	List <Texture> textures = null;

	const float speedModifier = 0.02f;

	const float shootCooldown = 0.3f;

	float shootTimer = 0.0f;

	int currentTextureIndex = 0;

	Vector3 lastInsidePosition;

	bool wasRemoved = false;

	Vector3 startPosition;

	float verticalFactor = 0.0f;

	float horizontalFactor = 0.0f;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0) && shootTimer > shootCooldown && SpawnManager.IsPlaying)
		{
			Fire();
            ParrotTrigger.Play();
		}

		shootTimer += Time.deltaTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Move();

		wasRemoved = false;
	}

	void Move()
	{
		if(wasRemoved)
			return;

		bool hasMoved = false;

		if(Input.GetKey("w"))
		{
			//transform.position += transform.forward * speedModifier;
			verticalFactor += speedModifier;
			if(verticalFactor > 1.0f)
			{
				verticalFactor = 1.0f;
			}

			hasMoved = true;
		}
		else if(Input.GetKey("s"))
		{
			//transform.position -= transform.forward * speedModifier;
			verticalFactor -= speedModifier;
			if(verticalFactor < -1.2f)
			{
				verticalFactor = -1.2f;
			}

			hasMoved = true;
		}

		const float horizontalModifier = 0.4f;
		if(Input.GetKey("d"))
		{
			//transform.position -= transform.right * speedModifier;
			horizontalFactor += speedModifier;
			if(horizontalFactor > horizontalModifier)
			{
				horizontalFactor = horizontalModifier;
			}

			hasMoved = true;
		}
		else if(Input.GetKey("a"))
		{
			//transform.position += transform.right * speedModifier;
			horizontalFactor -= speedModifier;
			if(horizontalFactor < -horizontalModifier)
			{
				horizontalFactor = -horizontalModifier;
			}

			hasMoved = true;
		}

		transform.position = startPosition + verticalFactor * transform.forward * 5.5f + horizontalFactor * transform.right * 5.5f;

		if(hasMoved)
		{
			if (!Footsteps.isPlaying)
                Footsteps.Play();
		}

        if (!Input.anyKey)
            Footsteps.Stop();

		if(hasMoved)
		{
			ChangeTexture();
		}
	}

	void Fire()
	{
		var direction = new Vector3(1.0f, 0.0f, 0.0f);

		var parrotObject = Instantiate(parrotPrefab);

		parrotObject.transform.position = transform.position;

		var parrot = parrotObject.GetComponent<Parrot>();
		parrot.Direction = direction;

		shootTimer = 0.0f;
	}

	void ChangeTexture()
	{
		material.mainTexture = textures[currentTextureIndex];

		currentTextureIndex++;

		if(currentTextureIndex == textures.Count)
			currentTextureIndex = 0;
	}

	void OnTriggerStay(Collider collider)
	{
		var terrain = collider.gameObject.GetComponent<Terrain>();
		if(terrain == null)
			return;

		lastInsidePosition = transform.position;
	}

	void OnTriggerExit(Collider collider)
	{
		var border = collider.gameObject.GetComponent<Terrain>();
		if(border == null)
			return;

		//transform.position = lastInsidePosition;

		wasRemoved = true;
	}
}
