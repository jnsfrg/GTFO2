using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pickupItem : MonoBehaviour
{

    private string cardValue;
    private GameController gameController;
    private AudioSource[] audio;
    private GameObject textCard;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent <GameController>();
        audio = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();

        Canvas canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        GameObject cardText = Resources.Load ("Prefabs/cardText") as GameObject;
        textCard = Instantiate (cardText, new Vector2(Screen.width/2,20), Quaternion.identity) as GameObject;
        textCard.transform.parent = canvas.transform;
        textCard.SetActive (false);

    }


    // pickup by walking in
    public void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            //Only Pickup when inventory not full
            if (gameController.addCardIfPossible(cardValue))
            {
                audio[1].Play();
                Debug.Log("PickUpAndPlay");
                Destroy(this.gameObject);
            }
            else
            {
                textCard.SetActive(true);
            }
        }
    }
    // set the card value that is added if you pick it up.
    public void SetCardValue(string val)
    {
        cardValue = val;
    }

    public string getCardValue()
    {
        return cardValue;
    }

    public void OnTriggerExit(Collider other){
        if (other.tag == "Player")
        {
            textCard.SetActive(false);
        }
    }

}
