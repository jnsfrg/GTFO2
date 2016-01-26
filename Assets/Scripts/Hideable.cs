using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/**
 * makes a (dark) area where the player can hide and will not be seen (but heard)
**/
public class Hideable : MonoBehaviour {
	private FirstPersonController fps;
    private GameObject textHide;

	void Start () {
		fps = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ();
        Canvas canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        GameObject hideText = Resources.Load ("Prefabs/hideText") as GameObject;
        textHide = Instantiate (hideText, new Vector2(Screen.width/2,80), Quaternion.identity) as GameObject;
        textHide.transform.parent = canvas.transform;
        textHide.SetActive (false);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			fps.setHideing (true);
            textHide.SetActive (true);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			fps.setHideing (false);
            textHide.SetActive (false);
		}
	}

}
