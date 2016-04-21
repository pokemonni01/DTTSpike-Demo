using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System.Collections;

public class GamePlay : MonoBehaviour {

	private GameObject bird;
	private GameObject dttsLabel;
	private GameObject tabToJumpLabel;
	private GameObject circleCenter;
	private GameObject rightSpike;
	private GameObject leftSpike;
	private GameObject resultObject;
	private Text[] textPoint;
	private InterstitialAd interstitial;

	private bool menuState = false;
	private int point;

	//Result State
	private bool showResultState = false;

	//Game Play State
	private bool gamePlayState = false;
	private float startTimeAfterDead;
	// Use this for initialization
	void Start () {
		menuState = true;
		bird = GameObject.Find ("bird");
		dttsLabel = GameObject.Find ("label");
		tabToJumpLabel = GameObject.Find ("tabToJumpLabel");
		point = 0;
		circleCenter = GameObject.Find ("circle center");
		rightSpike = GameObject.Find ("Right Spike");
		leftSpike = GameObject.Find ("Left Spike");
		textPoint = GameObject.Find ("TextPoint").GetComponents<Text>();
		resultObject = GameObject.Find ("Result");
		resultObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (menuState)
			start ();
		if (gamePlayState)
			playGame ();
		if (showResultState) {
			onResult ();
		}
	}

	public void start(){
		gamePlayState = false;
		menuState = true;
		showResultState = false;
		onMenuState ();
		RequestBanner ();
		RequestInterstitial ();
	}

	public void onMenuState(){
		menuState = false;
		resultObject.SetActive (false);
		dttsLabel.SendMessage ("Show");
		tabToJumpLabel.SendMessage ("Show");
		bird.GetComponent<BirdMovement> ().onStart ();
		point = 0;
		textPoint[0].text = "00";

	}

	public void playGame(){
		gamePlayState = true;
		menuState = false;
		showResultState = false;
		onPlayGame ();
	}

	public void onPlayGame(){
		dttsLabel.SendMessage ("Hide");
		tabToJumpLabel.SendMessage ("Hide");
	}

	public void result(){
		menuState = false;
		gamePlayState = false;
		showResultState = true;
		startTimeAfterDead = Time.time;
	}

	void onResult(){
//		showResultState = false;
		float timeDiff = Time.time - startTimeAfterDead;
		if (timeDiff >= 3) {
			resultObject.GetComponent<ResultState> ().Show ();
			bird.SetActive (false);
			if (interstitial.IsLoaded()) {
				interstitial.Show();
			}
		}
		rightSpike.SendMessage ("Hide");
		leftSpike.SendMessage ("Hide");
	}

	public void addPoint(){
		point++;
		if( point < 10 )
			textPoint[0].text = "0"+point;
		else
			textPoint[0].text = point+"";
		if (point % 2 == 0 && point > 1) {
			rightSpike.SendMessage ("Show");
			leftSpike.SendMessage ("Hide");
			
		} else {
			leftSpike.SendMessage ("Show");
			rightSpike.SendMessage ("Hide");
		}
	}

	public int getPoint(){
		return point;
	}

	private void RequestInterstitial(){
		string adUnitId = "ca-app-pub-1854800632169810/8818059549";

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator).Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	private void RequestBanner(){
		string adUnitId = "ca-app-pub-1854800632169810/2354553546";

		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator).Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}
}//end class
