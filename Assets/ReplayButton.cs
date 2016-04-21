using UnityEngine;
using System.Collections;

public class ReplayButton : MonoBehaviour {
	
	private GameObject mainCamera;
	private GamePlay[] gamePlay;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
		gamePlay = mainCamera.GetComponents<GamePlay> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		gamePlay[0].start ();
	}
}
