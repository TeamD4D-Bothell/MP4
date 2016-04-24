using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Turn : MonoBehaviour {

	private Animator animator;
	private string turnAxis = "Horizontal";
	private int turnKey = Animator.StringToHash("Turn");
	private int turnVal;

	public float turnRate = 1.0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		turnVal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float axisVal = Input.GetAxis(turnAxis);
		if (axisVal != 0) {
			transform.Rotate(-transform.forward, axisVal * turnRate * Time.deltaTime);
			turnVal = 1 * (int)Mathf.Sign(axisVal);
			animator.SetInteger(turnKey, turnVal);
		}
		else animator.SetInteger(turnKey, 0);
	}
}
