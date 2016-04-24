using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	public float minLife = 10;
	public float maxLife = 30;
	private float threshold;

	private float lifeTime;
	private float currTime;

	private Animator animator;
	private int stateKey = Animator.StringToHash("State");

	public enum HOUSE_STATE {
		healthy,
		dying,
		dead
	}

	private HOUSE_STATE currState = HOUSE_STATE.healthy;

	public HOUSE_STATE CurrentState {
		get { return currState; }
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	void OnEnable() {
		SetLifespan();
		currState = HOUSE_STATE.healthy;
	}

	void Update () {
		currTime -= Time.deltaTime;

		if (currState == HOUSE_STATE.healthy
				&& currTime < threshold) {
			currState = HOUSE_STATE.dying;
			animator.SetInteger(stateKey, 1);
		}
		else if (currState == HOUSE_STATE.dying
				&& currTime <= 0) {
			currState = HOUSE_STATE.dead;
			animator.SetInteger(stateKey, 2);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Crate Delivered");
		Debug.Log(currState);
		Debug.Log(currTime);

		if (currState == HOUSE_STATE.dying) {
			animator.SetInteger(stateKey, 0);
			currState = HOUSE_STATE.healthy;
			SetLifespan();
		}
	}

	private void SetLifespan() {
		lifeTime = Random.Range(minLife, maxLife);
		currTime = lifeTime;
		threshold = lifeTime / 2;
	}
}
