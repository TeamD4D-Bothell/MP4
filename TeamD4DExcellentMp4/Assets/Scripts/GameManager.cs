using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Starting to become monolithic. Refactor if any more functionality is added.
/// </summary>
public class GameManager : MonoBehaviour {

	public GameObject targetSuper;
	private List<GameObject> houseGOs;
	private List<House> houses;
	public int listSize = 10;
	public string nextLevel = "";
    

	private float top, bottom, right, left;
	public float margin = 0.25f;
	private Vector3 newPos;

	public Text healthy;
	public Text abandoned;
	private int deadCount = 0,
				goodCount = 0;
				
	public enum GAME_STATE {
		start, playing, levelEnd
	}
	private GAME_STATE currState = GAME_STATE.start;
	public GAME_STATE CurrentState { get { return currState; } }

	public float levelTime = 15f;
	private float currTime;
	public float CurrentTime { get { return currTime; } }

	public float endTimer = 3f;
	public GameObject endPrompt;

	// Awake
	void Awake() {
        
		Vector3 topRight = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width, Screen.height, 0));
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(
			new Vector3(0, 0, 0));

		top = topRight.y - margin;
		right = topRight.x - margin;
		bottom = bottomLeft.y + margin;
		left = bottomLeft.x + margin;

		houseGOs = new List<GameObject>();
		houses = new List<House>();
		try {
			for (int i = 0; i < listSize; i++) {
				GameObject obj = Instantiate(targetSuper) as GameObject;
				obj.SetActive(false);
				houseGOs.Add(obj);
			}
			foreach (GameObject house in houseGOs) {
				var obj = house.GetComponent<House>();
				obj.minLife = levelTime;
				obj.maxLife = levelTime;
				houses.Add(obj);
			}
		}
		catch { Debug.Log("GameManager does not have a reference for spawning houses."); }

		deadCount = 0;
		goodCount = 0;

		try { healthy.text = "Healthy: " + goodCount; }
		catch { Debug.Log("Healthy text was not assigned to game manager"); }
		try { abandoned.text = "Abandoned: " + deadCount; }
		catch { Debug.Log("Abandoned text was not assigned to game manager"); }
		try { abandoned.text = "Abandoned: " + deadCount; }
		catch { Debug.Log("Abandoned text was not assigned to game manager"); }
		
		if (endPrompt != null) { endPrompt.SetActive(false); }
		else {
			Debug.LogError("No reference for end text object");
			Debug.Break();
		}

		currTime = levelTime;
	}

	// UPDATE
	void Update () {

       
        if (Input.GetButtonUp("Cancel"))
			Application.Quit();

		switch (currState) {
			case GAME_STATE.start:
				CheckStart();
				break;
			case GAME_STATE.playing:
				CheckDone();
				break;
			case GAME_STATE.levelEnd:
				EndGame();
				break;
			default:
				break;
		}
	}
	

	// CHECK START
	private void CheckStart() {
		if (Input.GetButtonDown("Submit")) {
			SetupHouses();
		}
	}
	
	// RESET HOUSES
	private void SetupHouses() {
		foreach (GameObject obj in houseGOs) {
			obj.SetActive(false);
			obj.transform.position = new Vector3(Random.Range(left, right), 
				Random.Range(bottom, top));
			obj.SetActive(true);
		}
		goodCount = 0;
		deadCount = 0;
		currState = GAME_STATE.playing;
		currTime = levelTime;
	}

	
	// Check if all buildings are destroyed or healthy
	private void CheckDone() {
		currTime -= Time.deltaTime;

		if (currTime <= 0) {
			currTime = 0;
			Count();
			if (goodCount + deadCount == listSize) {
				currState = GAME_STATE.levelEnd;
				endPrompt.SetActive(true);
			}
		}
	}

	// COUNT HOUSES
	private void Count() {
		deadCount = 0;
		goodCount = 0;

		foreach (House house in houses) {
			house.BringDown();
			if (house.CurrentState == House.HOUSE_STATE.dead) {
				deadCount++;
			}
			else if (house.CurrentState == House.HOUSE_STATE.healthy) {
				goodCount++;
			}
		}

		healthy.text = "Healthy: " + goodCount;
		abandoned.text = "Abandoned: " + deadCount;
	}

	// END GAME
	private void EndGame() {
		endTimer -= Time.deltaTime;

		if (endTimer <= 0) {
			SceneManager.LoadScene(nextLevel);
		}
	}
}
