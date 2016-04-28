using UnityEngine;
using System.Collections;

public class ForwardVelocity : MonoBehaviour {

	[Range(1, 10)]
	public float velocity = 1.0f;
	public float startVelocity = 1.0f, minVelocity = 1.0f,
		maxVelocity = 1.0f, inputScale = 0.5f;


	// Use this for initialization
	void Start () {
		velocity = startVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Vertical") != 0) {
			velocity += Input.GetAxis("Vertical") * inputScale;
			if (velocity > maxVelocity)
				velocity = maxVelocity;
			if (velocity < minVelocity)
				velocity = minVelocity;
		}
        /*    if (transform.position.x < 0) transform.position += -1 * ((transform.up * velocity * Time.deltaTime) / 2);
            transform.position += transform.up * velocity * Time.deltaTime; */
        transform.position += transform.up * velocity * Time.deltaTime;
    }
}
