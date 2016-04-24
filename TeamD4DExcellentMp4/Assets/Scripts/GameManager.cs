using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject targetSuper;
	private List<GameObject> targets;
	public int listSize = 10;

	private float top, bottom, right, left;
	public float margin = 0.25f;
	private Vector3 newPos;

	public Text healthy;
	public Text abandoned;
	private int currentDead;
	int counter = 0;

	// Use this for initialization
	void Start() {
		Vector3 topRight = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width, Screen.height, 0));
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(
			new Vector3(0, 0, 0));

		top = topRight.y - margin;
		right = topRight.x - margin;
		bottom = bottomLeft.y + margin;
		left = bottomLeft.x + margin;

		targets = new List<GameObject>();

		try {
			for (int i = 0; i < listSize; i++) {
				GameObject obj = Instantiate(targetSuper) as GameObject;
				obj.SetActive(false);
				targets.Add(obj);
			}
		}
		catch {
			Debug.Log("GameManager does not have a reference for spawning houses.");
		}

		currentDead = 0;

		try {
			healthy.text = "Healthy: " + targets.Count;
		}
		catch {
			Debug.Log("Healthy text was not assigned to game manager");
		}
		try {
			abandoned.text = "Abandoned: " + currentDead;
		}
		catch {
			Debug.Log("Abandoned text was not assigned to game manager");
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Cancel"))
			Application.Quit();

		if (Input.GetButtonDown("Jump")) {
			Reset ();
		}

		Count ();
	}

	private void Reset() {
		foreach (GameObject obj in targets) {
			obj.SetActive(false);
			obj.transform.position = new Vector3(Random.Range(left, right), 
				Random.Range(bottom, top));
			obj.SetActive(true);
		}

		currentDead = 0;
	}

	private void Count() {
		counter = 0;
		foreach (GameObject target in targets) {
			House house = target.GetComponent<House>();
			if (house.CurrentState == House.HOUSE_STATE.dead)
				counter++;
		}
		currentDead = counter;
		healthy.text = "Healthy: " + (targets.Count - currentDead);
		abandoned.text = "Abandoned: " + currentDead;
	}
}
