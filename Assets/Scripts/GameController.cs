using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

/**
 * Use this class a single point to manage important Settings of the level/game
 * The Gamecontroller Manages all Obejects that are important to the difficulty of the Gamge.
 * It also manages score and inventory objects. 
 * TODO: Count cards and stuff for Score 
**/
public class GameController : MonoBehaviour
{
	
	private Score score;
	private static string playerName;
	private Inventory inventory;
	private List<string> assignedCardsDoors = new List<string> ();
	private List<string> assignedCardsFloor = new List<string> ();
	private List<string> assignedCardsRooms = new List<string> ();
	private List<string> assignedCardsGarage = new List<string> ();
	private bool canPickUp=true;


	void Start ()
	{
		//assign variable first!!
		inventory = GetComponent<Inventory> ();
		score = GameObject.FindGameObjectWithTag ("Score").GetComponent<Score> ();
		playerName = PlayerPrefs.GetString ("PlayerName");


		assignCardsToDoors ();// this also calls assignCardsToPickUpItems();
		assignCardToGarage ();
	}

	// called by the New Game Button
	public void StartNewGame (string sceneToStart)
	{
		score.StartCounting ();
		if (playerName.Length > 0) {
			PlayerPrefs.SetString ("PlayerName", playerName);
			PlayerPrefs.Save ();
		}
        //SceneManager.LoadScene (sceneToStart);
        Application.LoadLevel(sceneToStart);
	}
	public void quitGame() {
		Application.Quit ();
	}
	void Update ()
	{
	}

	public void setPlayerName (string name)
	{
		if (name.Length > 0) {
			playerName = name;
		}
	}

	//Call this to end a game with Win
	public void endGameWithSuccess ()
	{
		score.Win ();
		score.Stop ();
		//SceneManager.LoadScene ("endScreen");
        Application.LoadLevel("endScreen");
	}

	//Call this to end a game with Loosing it
	public void endGameWithLoose ()
	{
		score.Loose ();
		score.Stop ();
        Application.LoadLevel("endScreen");
    }

	// returns a card value for a door. Therefore a value is selected that is in the inventory and not yet assigned to another door
	private string getPossibleDoorCard ()
	{
		List<string> inventoryList = inventory.getInventoryList ();
		List<string> possibleCards = inventoryList.Except (assignedCardsDoors).ToList ();
		string card = possibleCards [Random.Range (0, possibleCards.Count - 1)];
		assignedCardsDoors.Add (card);
		return card;
	}

	//assigns Card Textures to all Doors that are tagged with "Door"
	private void assignCardsToDoors ()
	{
		GameObject[] gs = GameObject.FindGameObjectsWithTag ("Door");
		if (gs != null) {
			foreach (GameObject g in gs) {  // iterate over Doors
				foreach (MeshRenderer m in g.GetComponentsInChildren<MeshRenderer> ()) { // iterate over the renderer of the CardRenderer Object in a door
					if (m.gameObject.name == "card") {
						string cardToOpen = getPossibleDoorCard ();
						g.GetComponent<OpenDoor> ().cardToOpen = cardToOpen;
						m.material.mainTexture = (Texture)Resources.Load (cardToOpen, typeof(Texture));
					}
				}
			}
			assignCardsToPickupItems ();
		}
	}

	//assign card Textures to all objects tagged with "PickUpCardFloor"
	//Picks a card from the Cards assigned to doors, only uses them once.
	private void assignCardsToPickupItems ()
	{
		GameObject[] gs = GameObject.FindGameObjectsWithTag ("PickUpCardFloor");
		foreach (GameObject g in gs) {
			List<string> cards = assignedCardsDoors.Except (assignedCardsFloor).ToList ();// only cards that arent used before and are on doors
			int z = Random.Range (0, cards.Count - 1);
			string card = cards [z];
			g.GetComponent<MeshRenderer> ().material.mainTexture = (Texture)Resources.Load (card, typeof(Texture));
			g.GetComponent <pickupItem> ().SetCardValue (card);
			assignedCardsFloor.Add (card);
		}
	}

	private void assignCardToGarage ()
	{
		GameObject g = GameObject.FindGameObjectWithTag ("GarageDoor");
		if (g != null) {
			foreach (MeshRenderer m in g.GetComponentsInChildren<MeshRenderer> ()) {
				if (m.gameObject.name == "card") {
					List<string> cards = Inventory.allPossibleCards.Except (assignedCardsDoors).ToList ();
					string card = cards [Random.Range (0, cards.Count - 1)];
					g.GetComponentInParent <open> ().AddCard (card);//
					m.material.mainTexture = (Texture)Resources.Load (card, typeof(Texture));
					assignedCardsGarage.Add (card);
				}
			}
			assignCardToRooms ();
		}
	}

	private void assignCardToRooms ()
	{
		GameObject[] gs = GameObject.FindGameObjectsWithTag ("PickUpCardGoal");
		foreach (GameObject g in gs) {
			List<string> cards = assignedCardsGarage.Except (assignedCardsRooms).ToList ();
			string card = cards [Random.Range (0, cards.Count - 1)];
			g.GetComponent<MeshRenderer> ().material.mainTexture = (Texture)Resources.Load (card, typeof(Texture));
			g.GetComponent <pickupItem> ().SetCardValue (card);
			assignedCardsRooms.Add (card);
		}
	}

	public List<string> getInventoryList ()
	{
		return inventory.getInventoryList ();
	}

	public void UseCard (string card)
	{
		inventory.useItem (card);
	}

	public bool addCardIfPossible (string card)
	{
		
		if (inventory.getInventoryList ().Count < 5 && canPickUp) {
			inventory.getInventoryList ().Add (card);
			Debug.Log ("Cards: " + inventory.getInventoryList ().Count);
			Debug.Log ("destory", gameObject);

			inventory.updateInventoryDisplayOnHand ();
			return true;
		}
		return false;
	}
	public void SetCanPickUp(bool c){
		canPickUp = c;
	}
}
