using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

	public float minLife = 15;
	public float maxLife = 20;

	private float lifeTime;
	private float currTime;

	private Animator animator;
	private int stateKey = Animator.StringToHash("State");

	public enum HOUSE_STATE {
		healthy,
		dying,
		dead
	}
	private HOUSE_STATE currState = HOUSE_STATE.dying;
	public HOUSE_STATE CurrentState { get { return currState; } }

	// START
	void Start () {
		animator = GetComponent<Animator>();
		currState = HOUSE_STATE.dying;
		SetLifespan();
	}

	// ON ENABLE
	void OnEnable() {
		if(animator != null) {
			SetState(HOUSE_STATE.dying);
		}
	}

	// UPDATE
	void Update () {
		if (currState == HOUSE_STATE.dying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				SetState(HOUSE_STATE.dead);
			}
		}
	}
	
	// Handles collision with crate
	// If extended, add check for tag to differentiate between
	// crate and whatever other obstacle
	void OnTriggerEnter2D(Collider2D other) {
		if (currState == HOUSE_STATE.dying) {
			SetState(HOUSE_STATE.healthy);
		}
	}
	
	
	// SET STATE
	private void SetState(HOUSE_STATE state) {
		switch (state)
		{
			case HOUSE_STATE.healthy:
				currState = HOUSE_STATE.healthy;
				animator.SetInteger(stateKey, 0);
				break;
			case HOUSE_STATE.dying:
				currState = HOUSE_STATE.dying;
				animator.SetInteger(stateKey, 1);
				SetLifespan();
				break;
			case HOUSE_STATE.dead:
				currState = HOUSE_STATE.dead;
				animator.SetInteger(stateKey, 2);
				break;
			default:
				break;
		}
	}

	private void SetLifespan() {
		lifeTime = Random.Range(minLife, maxLife);
		currTime = lifeTime;
	}

	public void BringDown() {
		if (currState == HOUSE_STATE.dying) {
			currTime = 0;
			SetState(HOUSE_STATE.dead);
		}
	}
}
