using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class handController : MonoBehaviour
{

	private Animator animator;
	public GameObject[] cards;
	public GameObject cardHolderR;
	private int indexOfSelectedCard = 0;
	private int scrollState = 0;
	private bool aCardIsActive = true;
	private GameController gameController;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;
	public Transform awayTarget;
	private FirstPersonController fpsController;
	public float viewAngle = 180f;
	private bool taken = false;
	private bool showing = false;
	private GameObject cardInRightHand;
	private GameObject cross;
	public float throwingInstantSpeed=10;
	private AudioSource [] audio;


	void Start ()
	{
		animator = GetComponent <Animator> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController> ();
		cross = GameObject.FindGameObjectWithTag ("Cross");
		cross.GetComponent<Image> ().enabled=false;
		audio = gameObject.GetComponents<AudioSource> ();
	}
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetKey ("v")) {
			showing = true;
			animator.SetBool ("showing", true);
			var d = Input.GetAxis ("Mouse ScrollWheel");
			if (d > 0f) {
				if (scrollState < 4) {
					scrollState++;
                    audio[1].Play();
				}
			} else if (d < 0f) {
				if (scrollState > 0) {
					scrollState--;
                    audio[1].Play();
				}
			}
			foreach (GameObject g in cards) {// TODO: avoid setting of empty cardholders
				Animator tmpAnim = g.GetComponentInChildren <Animator> ();
				if (tmpAnim != null) {
					tmpAnim.SetBool ("selected", false);
				}
			}
			//Debug.Log (scrollState / 5);
			indexOfSelectedCard = scrollState / 1;
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("showing")) {

				if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.2f) {
					Animator anim = cards [indexOfSelectedCard].GetComponentInChildren <Animator> ();
					if (anim != null) {
						anim.SetBool ("selected", true);
					}
				}
			}
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("showIdle")) {
				Animator anim = cards [indexOfSelectedCard].GetComponentInChildren <Animator> ();
				if (anim != null) {
					anim.SetBool ("selected", true);
				}
			}
			aCardIsActive = true;
		} else {
			showing = false;
			animator.SetBool ("showing", false);

			if (aCardIsActive) {
				foreach (GameObject g in cards) {
					Animator tmpAnim = g.GetComponentInChildren <Animator> ();
					if (tmpAnim != null) {
						tmpAnim.SetBool ("selected", false);
					}
				}
				aCardIsActive = false;
			}
		}

		if (Input.GetMouseButtonDown (1)) {//right mouse button to "load"
			if (showing) {
				if (!taken) {
					animator.SetBool ("taken", true);
					StartCoroutine ("WaitAndTakeCard");
					taken = true;
					gameController.SetCanPickUp (false);
					cross.GetComponent<Image> ().enabled=true;
				} else {
					animator.SetBool ("taken", false);
					taken = false;
					gameController.SetCanPickUp (true);
					StartCoroutine ("WaitAndReturnCard");
					cross.GetComponent<Image> ().enabled=false;
				}
			}
			
		}
		if (Input.GetMouseButtonDown (0)) {
			if (taken) {
				animator.SetTrigger("Shoot");
				Shoot ();
				taken = false;
				gameController.SetCanPickUp (true);
				if (cardInRightHand != null) {
					Destroy (cardInRightHand, 5);
				}
				animator.SetBool ("taken", false);
				cross.GetComponent<Image> ().enabled = false;;
			}
		}
	}

	private void Shoot(){
		RaycastHit hit;
		Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);//same amazing check, credit to Deozaan #Unity3D on IRC
		//Debug.DrawLine(cardInRightHand.transform.position, hit.point);
		cardInRightHand.AddComponent <Rigidbody>();
		cardInRightHand.GetComponent<Rigidbody> ().useGravity=false;
		cardInRightHand.transform.parent=null;
		cardInRightHand.transform.LookAt(hit.point);//send it on the ray
		audio[0].Play();
		cardInRightHand.GetComponent<Rigidbody> ().velocity = cardInRightHand.transform.forward * throwingInstantSpeed;
        gameController.DescreseScore();
	}

	IEnumerator WaitAndReturnCard ()
	{
		yield return new WaitForSeconds (0.25f);
		gameController.addCardIfPossible (cardInRightHand.GetComponent <pickupItem> ().getCardValue ());
		Destroy (cardInRightHand);
	}

	IEnumerator WaitAndTakeCard ()
	{
		yield return new WaitForSeconds (0.25f);
	
		GameObject cardOrig = cards [indexOfSelectedCard].transform.Find ("cardHand").gameObject;
		cardInRightHand = Instantiate (cardOrig);
		cardInRightHand.GetComponent<pickupItem> ().SetCardValue (cardOrig.GetComponent <pickupItem> ().getCardValue ());
		gameController.UseCard (cardOrig.GetComponent <pickupItem> ().getCardValue ());
		cardInRightHand.GetComponent<Animator> ().SetBool ("selected", false);
		cardInRightHand.transform.parent = cardHolderR.transform;
		cardInRightHand.transform.localPosition = Vector3.zero;
		cardInRightHand.transform.localRotation = Quaternion.identity;
		cardInRightHand.GetComponent<Animator> ().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load ("Animation/ThrownCard.controller", typeof(RuntimeAnimatorController));
		cardInRightHand.AddComponent <KnockoutEnemy>();
		indexOfSelectedCard = 0;
		scrollState = 0;

	}
   

    public void OpenDoor(string card)
    {
        animator.SetTrigger("openDoor");
//        StartCoroutine("WaitAndTakeCardForOpeningDoor",card);
//        animator.SetTrigger("openTaken");
    }

//    IEnumerator WaitAndTakeCardForOpeningDoor(string card)
//    {
//        yield return new WaitForSeconds(0.25f);
//
//        GameObject cardOrig=null;
//
//        foreach (GameObject g in cards)
//        {
//            var x = g.GetComponentInChildren<pickupItem>();
//            if (x.getCardValue() == card)
//            {
//                cardOrig = g.transform.Find("cardHand").gameObject;
//
//            }
//        }
//
//        cardInRightHand = Instantiate(cardOrig);
//        cardInRightHand.GetComponent<pickupItem>().SetCardValue(card);
//        cardInRightHand.GetComponent<Animator>().SetBool("selected", false);
//        cardInRightHand.transform.parent = cardHolderR.transform;
//        cardInRightHand.transform.localPosition = Vector3.zero;
//        cardInRightHand.transform.localRotation = Quaternion.identity;
//        cardInRightHand.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/ThrownCard.controller", typeof(RuntimeAnimatorController));
//    }
}
