using UnityEngine;
using System.Collections;

public class StartPrompt : MonoBehaviour {
	public string inputKey = "Jump";
	void Update () {
		if (Input.GetButtonDown("Jump"))
			gameObject.SetActive(false);
	}
}
