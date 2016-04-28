using UnityEngine;
using System.Collections;

public class MusicBox : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
