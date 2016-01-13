using UnityEngine;
using System.Collections;
/**
* Use this Behavior on an area with collider. if the player enters it its won.
**/
public class TriggerWin : MonoBehaviour
{
	private GameController controller;
	
	void Start ()
	{
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			controller.endGameWithSuccess ();
		}
	}
}
