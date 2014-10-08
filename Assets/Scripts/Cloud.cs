using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float minY = 1;
	public float maxY = 4;
	public float minSpeed = 0.05f;
	public float maxSpeed = 0.4f;
	public float startX = 6;
	public float endX = -6;

	float speed;

	void Start()
	{
		speed = Random.Range(minSpeed, maxSpeed);
	}

	void Update () 
	{
		if(transform.position.x < endX)
		{
			//Reset cloud
			transform.position = new Vector3(startX, Random.Range(minY, maxY), transform.position.z);
			speed = Random.Range(minSpeed, maxSpeed);
			if(Random.Range(0.0f, 1.0f) < 0.5f)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
		}

		//Move cloud
		transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
	}
}
