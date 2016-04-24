using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DropSupplies : MonoBehaviour {

	public int maxActive = 3;
	public GameObject payload;

	private List<GameObject> crates;

	public Text crateText;
	private int activeCount;
	private int tempCount;

	// Use this for initialization
	void Start() {
		crates = new List<GameObject>();

		try {
			for (int i = 0; i < maxActive; i++) {
				GameObject obj = Instantiate(payload) as GameObject;
				obj.SetActive(false);
				crates.Add(obj);
			}
		}
		catch {
			Debug.Log("Payload was not assigned");
		}

		try {
			crateText.text = "Crates: 0 Max: " + maxActive; 
		}
		catch {
			Debug.Log("Crate Text reference not set in DropSupplies");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			for (int i = 0; i < crates.Count; i++) {
				if(!crates[i].activeInHierarchy) {
					crates[i].transform.position = transform.position;
					crates[i].SetActive(true);
					break;
				}
			}
		}

		tempCount = 0;
		foreach (GameObject obj in crates) {
			if (obj.activeInHierarchy)
				tempCount++;
		}
		activeCount = tempCount;
		crateText.text = "Crates: " + activeCount 
			+ " Max: " + crates.Count;
	}
}
