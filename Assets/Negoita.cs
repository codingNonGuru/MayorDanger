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

	const float speedModifier = 0.05f;

	const float shootCooldown = 0.3f;

	float shootTimer = 0.0f;

	int currentTextureIndex = 0;

	Vector3 lastInsidePosition;

	bool wasRemoved = false;

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

		if(Input.GetKey("s"))
		{
			transform.position -= transform.forward * speedModifier;
            if (!Footsteps.isPlaying)
                Footsteps.Play();
            
			hasMoved = true;
        }
		else if(Input.GetKey("w"))
		{
			transform.position += transform.forward * speedModifier;
            if (!Footsteps.isPlaying)
                Footsteps.Play();

			hasMoved = true;
        }

		if(Input.GetKey("a"))
		{
			transform.position -= transform.right * speedModifier;
            if (!Footsteps.isPlaying)
                Footsteps.Play();

			hasMoved = true;
        }
		else if(Input.GetKey("d"))
		{
			transform.position += transform.right * speedModifier;
            if (!Footsteps.isPlaying)
                Footsteps.Play();

			hasMoved = true;
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
		var mousePosition = Input.mousePosition;
		mousePosition.z = (Camera.main.transform.position - transform.position).magnitude;

		var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		var direction = worldPosition - transform.position;
		direction.Normalize();

		var parrotObject = Instantiate(parrotPrefab);

		parrotObject.transform.position = transform.position;

		var soilDirection = direction;
		soilDirection.y = 0.0f;
		soilDirection.Normalize();

		float angle = Vector3.Dot(direction, soilDirection);
		angle = 57.297f * Mathf.Acos(angle);

		direction = Quaternion.AngleAxis(angle, Camera.main.transform.right) * direction;
		direction.Normalize();
		direction.y = 0.0f;

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
