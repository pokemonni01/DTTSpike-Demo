using UnityEngine;
using System.Collections;

public class LabelControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Show() {
		Renderer[] renderers =gameObject.GetComponents<Renderer>();
		foreach ( Renderer lRenderer in renderers)
		{
			lRenderer.enabled=true;
		}
	}
	void Hide() {
		Renderer[] renderers =gameObject.GetComponents<Renderer>();
		foreach ( Renderer lRenderer in renderers)
		{
			lRenderer.enabled=false;
		}
	}
}
