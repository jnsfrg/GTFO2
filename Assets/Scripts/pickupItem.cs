using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pickupItem : MonoBehaviour
{

	private string cardValue;
	private GameController gameController;
	private AudioSource audio;
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameController> ();
		audio = gameObject.GetComponent<AudioSource> ();
	}


	// pickup by walking in
	public void OnTriggerEnter (Collider col)
	{

		if (col.gameObject.tag == "Player") {
			//Only Pickup when inventory not full
			if (gameController.addCardIfPossible (cardValue)) {
				audio.Play ();
				Debug.Log ("PickUpAndPlay");

			

				Destroy (this.gameObject, 3/10);
			
							
			
			}
		}
	}
	// set the card value that is added if you pick it up.
	public void SetCardValue (string val)
	{
		cardValue = val;
	}

	public string getCardValue ()
	{
		return cardValue;
	}

}
