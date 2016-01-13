using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class open : MonoBehaviour
{
	Animator animator;
	// Use this for initialization

	private List<string> cards;
	private GameController gameController;
    private AudioSource failOpenDoor, openGarage;
    

	void Start ()
	{
        AudioSource[] audios = GetComponents<AudioSource>();
        failOpenDoor = audios[0];
        openGarage = audios[1];
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

            if (possible)
            {
                openGarage.Play();
                animator.SetBool("open", true);
            }
            else
                failOpenDoor.Play();
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
