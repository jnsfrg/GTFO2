using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenDoor : MonoBehaviour
{

	private Animator animator;

	// Use this for initialization
    
	private Vector3 startRot;
	private Vector3 endRot;
	private bool doorOpen;
	private bool canOpen = false;
	private bool removeInventoryItem = false;
	public float angleDoor = 90;
	public float smooth = 1;
	private Inventory inventory;
	private GameController gameController;
	private GameObject cardRenderer;
	private GameObject door;

	//Card to open
	public string cardToOpen;

	void Start ()
	{

		//animator = GetComponent<Animator>();
        
		doorOpen = false;
		//startRot = transform.eulerAngles;
		//endRot = new Vector3 (startRot.x, startRot.y + angleDoor, startRot.z);
		//TODO: Move calls to inventory to gameController for overview reasons.
		inventory = GameObject.FindGameObjectWithTag ("GameController").GetComponent <Inventory> ();
		//animator = GameObject.FindGameObjectWithTag ("Door").GetComponent<Animator> ();
		door = GameObject.FindGameObjectWithTag("Door");

	}
	
	// Update is called once per frame
	void Update ()
	{
        
		//Debug.Log ("allowed: "+canOpen);
		if (canOpen) {
			if (Input.GetKeyDown ("f")) {
				Debug.Log ("key pressed");
			}
			//Open door
			if (doorOpen) {
				//transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, endRot, Time.deltaTime * smooth);
				door.GetComponent<Animator>().Play("Cube|CubeAction.0010");
				Debug.Log ("Start Animation");
				if (removeInventoryItem) {
					removeInventoryItem = false;
					inventory.useItem (cardToOpen);
				}
			}

			//Close door
			if (!doorOpen) {
				//transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, startRot, Time.deltaTime * smooth);
			}

			if (Input.GetKeyDown ("f") && inventory.getInventoryList ().Contains (cardToOpen)) {
                
				Debug.Log ("Open Door", gameObject);
				doorOpen = true;
				removeInventoryItem = true;
                
			}
        
			if (Input.GetKeyDown ("f") ) {
				Debug.Log ("doorOpen false");
				doorOpen = false;
			}
       
			//TODO: Make open door an animation 
//       if (Input.GetKeyDown("f") && !animator.GetBool("openDoor")) {
//           animator.SetBool("openDoor", true);
//           Debug.Log("If 1");
//       }
//
//       else if (Input.GetKeyDown("f") && animator.GetBool("openDoor"))
//       {
//           animator.SetBool("openDoor", false);
//           Debug.Log("If 2");
//       }
		}
	}

	void OnTriggerEnter (Collider collider)
	{
		//Debug.Log ("OnTriggerEnter");
		if (collider.gameObject.tag == "Player") {
			canOpen = true;
			doorOpen = true;
			Debug.Log ("canOpen" + canOpen);
		}
	}

	void OnTriggerExit (Collider collider)
	{
		//Debug.Log ("OnTriggerExit");
		if (collider.gameObject.tag == "Player") {
			canOpen = false;
		}
	}
}
