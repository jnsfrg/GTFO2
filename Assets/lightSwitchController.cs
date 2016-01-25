using UnityEngine;
using System.Collections;

public class lightSwitchController : MonoBehaviour {


	private bool canSwitch;
	private bool lightOn;
	private GameObject lamp;
	private GameObject plane;
	// Use this for initialization
	void Start () {
	
		lightOn = true;
		lamp = GameObject.FindGameObjectWithTag ("light1");
		plane = GameObject.FindGameObjectWithTag ("light1Plane");
		plane.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (canSwitch) {

			if (Input.GetKeyDown ("f")) {
				if (lightOn) {
					Debug.Log ("light off");
					lamp.SetActive (false);
					lightOn = false;
					plane.SetActive (true);

				} else {
					Debug.Log ("light on");
					lamp.SetActive (true);
					lightOn = true;
					plane.SetActive (false);

				}
			}


		}
	
	}

	void OnTriggerEnter(Collider collider){

		if (collider.gameObject.tag == "Player") {
			canSwitch = true;
			
		}

	}
}
