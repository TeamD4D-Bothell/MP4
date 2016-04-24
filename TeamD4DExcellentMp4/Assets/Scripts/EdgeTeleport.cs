using UnityEngine;
using System.Collections;

public class EdgeTeleport : MonoBehaviour {

	private float top, bottom, right, left;
	private Vector3 newPos;

	// Use this for initialization
	void Start () {
		Vector3 topRight = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width, Screen.height, 0));
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(
			new Vector3(0, 0, 0));

		top = topRight.y;
		right = topRight.x;
		bottom = bottomLeft.y;
		left = bottomLeft.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > top) {
			newPos = transform.position;
			newPos.y = bottom;
			transform.position = newPos;
		}
		else if (transform.position.y < bottom) {
			newPos = transform.position;
			newPos.y = top;
			transform.position = newPos;
		}
		if (transform.position.x > right) {
			newPos = transform.position;
			newPos.x = left;
			transform.position = newPos;
		}
		else if (transform.position.x < left) {
			newPos = transform.position;
			newPos.x = right;
			transform.position = newPos;
		}
	}
}
