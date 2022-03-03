using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiornoNotte : MonoBehaviour
{
	[Range(0.0f, 10.0f)]
	public float CycleSpeed;
	private Light sunLight;

	void Start()
	{
		sunLight = GetComponent<Light>();

	}
	void FixedUpdate()
	{
		transform.Rotate(new Vector3(0.1f * CycleSpeed, 0.0f, 0.0f));

		if (transform.rotation.eulerAngles.x > 0.0f && transform.rotation.eulerAngles.x < 160.0f)
		{
			sunLight.intensity = Mathf.Lerp(sunLight.intensity, 1.0f, Time.deltaTime * CycleSpeed * 2);
			//starSystem.Stop();
		}
		if (transform.rotation.eulerAngles.x > 160.0f)
		{
			sunLight.intensity = Mathf.Lerp(sunLight.intensity, 0.0f, Time.deltaTime * CycleSpeed * 2);
			//starSystem.Play();
		}
	}
}
