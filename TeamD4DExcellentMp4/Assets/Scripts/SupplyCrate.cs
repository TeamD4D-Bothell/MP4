using UnityEngine;
using System.Collections;

public class SupplyCrate : MonoBehaviour {

	public float startTime = 5;
	public float minRatio = 0.2f;
	public float startX = 0.5f;
	public float startY = 0.5f;

	private float current;
	private float timeRatio;

	// Use this for initialization
	void OnEnable () {
		current = startTime;
		transform.localScale = new Vector3(startX, startY);
	}
	
	// Update is called once per frame
	void Update () {
		current -= Time.deltaTime;
		timeRatio = current / startTime;
		if (timeRatio > minRatio)
			transform.localScale = new Vector3(startX * timeRatio, startY * timeRatio);
		if (timeRatio <= 0) {
			gameObject.SetActive(false);
		}
	}

	void OnDisable() {
		transform.localScale = new Vector3(startX, startY);
		timeRatio = 1;
		current = startTime;
	}
}
