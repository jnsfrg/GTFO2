using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/**
 * makes a (dark) area where the player can hide and will not be seen (but heard)
**/
public class Hideable : MonoBehaviour {
	private FirstPersonController fps;


	void Start () {
		fps = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ();;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			fps.setHideing (true);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			fps.setHideing (false);
		}
	}

}
