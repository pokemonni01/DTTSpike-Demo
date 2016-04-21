using UnityEngine;
using System.Collections;

public class leftSpike : MonoBehaviour {

	private float posY;
	private string spikeName;
	private int numberOfSpike;

	// Use this for initialization
	void Start () {
		spikeName = gameObject.name;
		numberOfSpike = int.Parse(spikeName.Substring (spikeName.Length - 1));
		randomPositionSpike ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, (float)posY, gameObject.transform.position.z);
	}

	public void randomPositionSpike(){
		//		if( numberOfSpike == 1 ){
		//			posY = Random.Range (5, 6);
		//		}else if( numberOfSpike == 2 ){
		//			posY = Random.Range (3, 4);
		//		}else if( numberOfSpike == 3 ){
		//			posY = Random.Range (0, 2);
		//		}else if( numberOfSpike == 4 ){
		//			posY = Random.Range (-3, -1);
		//		}else if( numberOfSpike == 5 ){
		//			posY = Random.Range (-6, -4);
		//		}
		posY = Random.Range (-6, 6);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//		posY = Random.Range (-6, 6);
		if (coll.gameObject.name.Equals ("bird")) {
			GameObject.Find ("Left Spike").SendMessage ("Hide");
		}
	}

}
