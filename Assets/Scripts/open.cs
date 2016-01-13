using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class open : MonoBehaviour
{
	Animator animator;
	// Use this for initialization

	private List<string> cards;
	private GameController gameController;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameController> ();
	}

	void OnTriggerStay (Collider other)
	{
		if (Input.GetKeyDown ("f")) {
			bool possible = true;
			foreach (string c in cards) {
				possible = gameController.getInventoryList ().Contains (c);
			}

			if (possible) {
				animator.SetBool ("open", true);
			}
		}
	}


	public void AddCard (string card)
	{
		if (cards == null) {
			cards = new List<string> ();
		}
		cards.Add (card);
	}
}
