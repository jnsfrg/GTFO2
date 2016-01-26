using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class open : MonoBehaviour
{
	Animator animator;
	// Use this for initialization

	private List<string> cards;
	private GameController gameController;
    private AudioSource audio;
    private GameObject doorText;
    private GameObject textDoor;
    private GameObject doorTextNo;
    private GameObject textDoorNo;
	void Start ()
	{
		animator = GetComponent<Animator> ();
        audio = GetComponent<AudioSource>();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameController> ();
        Canvas canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        doorText = Resources.Load ("Prefabs/garageText") as GameObject;
        textDoor = Instantiate (doorText, new Vector2(Screen.width/2,20), Quaternion.identity) as GameObject;
        textDoor.transform.parent = canvas.transform;
        textDoor.SetActive (false);
        doorTextNo = Resources.Load ("Prefabs/garageTextNo") as GameObject;
        textDoorNo = Instantiate (doorTextNo, new Vector2(Screen.width/2,20), Quaternion.identity) as GameObject;
        textDoorNo.transform.parent = canvas.transform;
        textDoorNo.SetActive (false);
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
                audio.Play();
                animator.SetTrigger("open");
            }
            else
            {
                textDoor.SetActive(false);
                textDoorNo.SetActive(true);
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
    void OnTriggerExit(Collider col){
        if (col.tag == "Player")
        {
            textDoor.SetActive (false);  
            textDoorNo.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col){
        if (col.tag == "Player")
        {
            textDoor.SetActive (true);  
        }
    }
}
