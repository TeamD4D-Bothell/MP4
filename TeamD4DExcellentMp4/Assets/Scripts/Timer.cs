using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

	private Text timer;
	private GameManager manager;

	void Awake () {
		timer = GetComponent<Text>();
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
	}
	
	void Update () {
		timer.text = Math.Round(manager.CurrentTime, 1).ToString();
	}
}
