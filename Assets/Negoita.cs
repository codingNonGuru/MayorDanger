using System.Collections;
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

	const float speedModifier = 0.1f;

	const float shootCooldown = 0.3f;

	float shootTimer = 0.0f;

	int currentTextureIndex = 0;

	Vector3 lastInsidePosition;

	bool wasRemoved = false;

	Vector3 startPosition;

	float positionFactor = 0.0f;

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
			transform.position += transform.forward * speedModifier;
			/*positionFactor += speedModifier;
			if(positionFactor > 1.0f)
			{
				positionFactor = 1.0f;
			}*/

			hasMoved = true;
		}
		else if(Input.GetKey("s"))
		{
			transform.position -= transform.forward * speedModifier;
			/*positionFactor -= speedModifier;
			if(positionFactor < -1.0f)
			{
				positionFactor = -1.0f;
			}*/

			hasMoved = true;
		}

		if(Input.GetKey("a"))
		{
			transform.position -= transform.right * speedModifier;
			/*positionFactor += speedModifier;
			if(positionFactor > 1.0f)
			{
				positionFactor = 1.0f;
			}*/

			hasMoved = true;
		}
		else if(Input.GetKey("d"))
		{
			transform.position += transform.right * speedModifier;
			/*positionFactor -= speedModifier;
			if(positionFactor < -1.0f)
			{
				positionFactor = -1.0f;
			}*/

			hasMoved = true;
		}

		//transform.position = startPosition + positionFactor * new Vector3(0.0f, 0.0f, 5.5f);

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

		transform.position = lastInsidePosition;

		wasRemoved = true;
	}
}
