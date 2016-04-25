using UnityEngine;
using System.Collections;

public class SpawnOnClick : MonoBehaviour {

	public GameObject toSpawn;
	public float zPos = 0f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameObject obj = (GameObject)Instantiate(toSpawn);
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = zPos;
			obj.transform.position = pos;
		}
	}
}
