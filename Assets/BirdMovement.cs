using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity;
	public Vector3 gravity;
	public Vector3 flapVelocity;
	public float maxSpeed = 10f;
	public float forwardSpeed = 2f;
	public Animation anim;

	private bool didflap;
	private bool dead = false;
	private int direction = 1; //1 go right , -1 go left
	private GameObject mainCamera;
	public Rigidbody2D rb;

	private Sprite sr;

	private bool playGame = false;

	// Use this for initialization
	void Start () {
		
		onStart ();
	}

	// Update is called once per frame
	void Update(){
		if (!playGame) {
			if (transform.position.y >= 0) {
				GetComponent<Rigidbody2D> ().gravityScale = 6f;
				transform.position = new Vector3(0,0,0);
				rb.velocity = new Vector3(0, 0, 0);
			} else if( transform.position.y <= -1 ) {
				GetComponent<Rigidbody2D> ().gravityScale = -6f;
				transform.position  = new Vector3(0,-1,0);
				rb.velocity = new Vector3(0, 0, 0);			}
		}
		if ( Input.GetMouseButtonDown(0) && !dead && playGame) {
			didflap = true;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite = sr;

	}//end update



	void FixedUpdate () {
		if( direction == 1 ){
			GetComponent<SpriteRenderer>().flipX = false;
		}
		else{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		velocity.x = forwardSpeed * direction;
		velocity += gravity * Time.deltaTime;
		if (didflap) {
			jump ();
		}
//		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
		rb.velocity = velocity;
		transform.position += velocity * Time.deltaTime;

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.IndexOf ("Spike") >= 0) {
			if( coll.gameObject.name.Equals("topSpike") ){
				velocity.y = -6;
			}
			else if(coll.gameObject.name.Equals("bottomSpike")){
				velocity.y = 6;
			}
			else if( coll.gameObject.name.IndexOf ("rightSpike") >= 0 ){
				direction = -1;
				velocity.x = -5;
			}
			else if( coll.gameObject.name.IndexOf ("leftSpike") >= 0 ){
				direction = 1;
				velocity.x = 5;
			}
			onDeadth ();
		}
		if (coll.gameObject.name.Equals ("game space")) {
			direction = direction * -1;
			if (!dead) {
				mainCamera.SendMessage ("addPoint");
			}
		}
		if (coll.gameObject.name.Equals ("game space")) {
			mainCamera.SendMessage ("collideCandy");
		}
//		Debug.LogError("Collision The Spike");
	}
		
	void jump(){
		didflap = false;
		if( velocity.y < 0 )
			velocity.y = 0;
		if (velocity.y > maxSpeed)
			velocity.y = maxSpeed;
		velocity.y = flapVelocity.y;
	}

	public void onStart(){
		gameObject.SetActive (true);
		mainCamera = GameObject.Find ("Main Camera");
		rb = GetComponent<Rigidbody2D>();
		transform.position = new Vector3(0,0,0);
		velocity = rb.velocity;
		didflap = false;
		forwardSpeed = 0;
		gravity.y = 0;
		flapVelocity.y = 0;
		direction = 1;
		playGame = false;
		dead = false;
	}

	public void onPlayGame(){
		mainCamera.SendMessage ("playGame");
		playGame = true;
		maxSpeed = 6f;
		forwardSpeed = 2f;
		gravity.y = -5;
		flapVelocity.y = 3;
		GetComponent<Rigidbody2D> ().gravityScale = 0.4f;
	}

	public void onDeadth(){
		if (dead)
			return;
		dead = true;
//		playGame = false;
//		gameObject.GetComponent<Animator>().Stop();
		mainCamera.SendMessage ("result");
	}

	void OnMouseDown(){
		if( !playGame )
			onPlayGame ();
	}
}
