using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
	//Inventory
	private List<string> inventoryList = null;

	/* Int value = Card value
     * Card deck in Strings
     * Herz = Herz2 -> HerzA 
     * Kreuz = Kreuz2 -> KreuzA (2 3 4 5 6 7 8 9 10 B D K A)
     * Pik = Pik2 -> PikA
     * Karo = Karo2 -> KaroA
     */

	public GameObject[] cardHolders;
	public static List<string> allPossibleCards = new List<string> {
		"Herz2",
		"Herz3",
		"Herz4",
		"Herz5",
		"Herz6",
		"Herz7",
		"Herz8",
		"Herz9",
		"Herz10",
		"HerzB",
		"HerzD",
		"HerzK",
		"HerzA",
		"Kreuz2",
		"Kreuz3",
		"Kreuz4",
		"Kreuz5",
		"Kreuz6",
		"Kreuz7",
		"Kreuz8",
		"Kreuz9",
		"Kreuz10",
		"KreuzB",
		"KreuzD",
		"KreuzK",
		"KreuzA",
		"Pik2",
		"Pik3",
		"Pik4",
		"Pik5",
		"Pik6",
		"Pik7",
		"Pik8",
		"Pik9",
		"Pik10",
		"PikB",
		"PikD",
		"PikK",
		"PikA",
		"Karo2",
		"Karo3",
		"Karo4",
		"Karo5",
		"Karo6",
		"Karo7",
		"Karo8",
		"Karo9",
		"Karo10",
		"KaroB",
		"KaroD",
		"KaroK",
		"KaroA"
	};

	private List<string> cards;

	// Use this for initialization
	void Start ()
	{
		initInventoryList ();
		//Debug.Log ("Cards: " + inventoryList.Count);
		updateInventoryDisplayOnHand ();
	}

	// Let Start() or getInventoryList() call it !! 
	private void initInventoryList ()
	{
		if (inventoryList == null) {
			inventoryList = new List<string> ();
			//Start with 5 Random Cards
			cards = new List<string> (allPossibleCards);
			for (int i = 0; i < 5; i++) {
				int num = Random.Range (0, cards.Count);
				inventoryList.Add (cards [num]);
				cards.RemoveAt (num);
			}
		}
	}

	public List<string> getInventoryList ()
	{
		if (inventoryList == null) {
			initInventoryList ();// to make sure this class is already initialized when called in another Start(). 
		}
		return inventoryList;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	//Use Card for something
	public void useItem (string card)
	{
		if (inventoryList.Count > 0) {
			Debug.Log ("Remove Card nr: " + inventoryList.Count);
			inventoryList.Remove (card);
			Debug.Log ("Cards: " + inventoryList.Count);
		}
		this.updateInventoryDisplayOnHand ();
	}

	// updates the display of Cards on Canvas. TODO: Move to Gamecontroller, does it belong to Inventory ??
//	public void updateInventoryDisplayOnCanvas ()
//	{
//		for (int i = 0; i < image.Length; i++) {
//			image [i].gameObject.SetActive (false);
//		}
//
//		for (int i = 0; i < inventoryList.Count; i++) {
//			var name = inventoryList [i];
//			var sprite = (Sprite)Resources.Load (name, typeof(Sprite));
//			image [i].sprite = sprite;
//			image [i].gameObject.SetActive (true);
//		}
//	}
	public void updateInventoryDisplayOnHand ()
	{
		for (int i = 0; i < cardHolders.Length; i++) {
			cardHolders [i].gameObject.SetActive (false);
		}

		for (int i = 0; i < inventoryList.Count; i++) {
			var name = inventoryList [i];
			var texture = (Texture)Resources.Load (name, typeof(Texture));
			cardHolders [i].GetComponent<MeshRenderer> ().material.mainTexture = texture;
			cardHolders [i].gameObject.SetActive (true);
			cardHolders [i].GetComponent<pickupItem> ().SetCardValue (name);
		}
	}
}