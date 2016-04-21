using UnityEngine;
using System.Collections;

public class ControllLeftSpike : MonoBehaviour {


	public float forwardSpeed = 0.1f;
	private int direction = 0; //1 go right , -1 go left
	private Vector3 velocity;


	// Use this for initialization
	void Start () {
		velocity = new Vector3 (0, 0, 0);
		forwardSpeed = 1f;
	}

	// Update is called once per frame
	void Update () {
		velocity.x = forwardSpeed * direction * Time.deltaTime;
		velocity = Vector3.ClampMagnitude (velocity, forwardSpeed);
		gameObject.transform.position += velocity;
		if( gameObject.transform.position.x > 0 )
			gameObject.transform.position = new Vector3(0, gameObject.transform.position.x, gameObject.transform.position.z);
		if( gameObject.transform.position.x < -0.6 )
			gameObject.transform.position = new Vector3(-0.6f, gameObject.transform.position.x, gameObject.transform.position.z);
	}

	void Show() {
		reposition();
		direction = 1;
	}
	void Hide() {
		direction = -1;
	}

	public void reposition(){
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			transform.GetChild (i).SendMessage ("randomPositionSpike");
		}
	}

}
