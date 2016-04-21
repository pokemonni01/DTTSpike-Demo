using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultState : MonoBehaviour {

	private Text[] textPoint;
	private GamePlay gamePlay;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		if (gamePlay.point != null) {
			
//		}
	}

	public void Show() {
		gameObject.SetActive (true);
		textPoint = GameObject.Find ("TextPointBar").GetComponents<Text> ();
		gamePlay = GameObject.Find("Main Camera").GetComponent<GamePlay> ();
		int point = gamePlay.getPoint();
		if (point < 10)
			textPoint [0].text = "0" + point;
		else
			textPoint [0].text = point + "";
	}

	void Hide() {
		gameObject.SetActive (false);
	}
}
