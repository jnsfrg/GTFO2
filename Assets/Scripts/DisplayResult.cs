using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
* Displays the Result in the EndScreen of the Game
**/
public class DisplayResult : MonoBehaviour {
	public Text t;

	public Text p1;
	public Text p2;
	public Text p3;
	public Text p4;
	public Text p5;
	public Text p6;
	public Text p7;
	public Text p8;
	public Text p9;
	public Text p10;
	public Text rank;


	// show all highscore in the Text field on start of endscreen.
	void Start(){
		Score score = GameObject.FindGameObjectWithTag ("Score").GetComponent<Score>();

		if (score.getWin ()) {
			Debug.Log(score.getScore());
			t.text="Score: "+score.getScore() +" !!";
			if (score.isInHighscore ()) {
				rank.text = "Your Rank:  "+score.getPlayerRank();
			}else{
				rank.text = "Your Rank:  Not in Highscore";
			}
		}else{
			t.text="Game over !!";
			rank.text = "";
		}
		int[] val;
		string[] names;
		Score.getHighscores (out val, out names);
		// Quick and dirty !!!
		p1.text = "1.\t\t " + names [0] + "\t\t " + val [0];
		p2.text = "2.\t\t " + names [1] + "\t\t " + val [1];
		p3.text = "3.\t\t " + names [2] + "\t\t " + val [2];
		p4.text = "4.\t\t " + names [3] + "\t\t " + val [3];
		p5.text = "5.\t\t " + names [4] + "\t\t " + val [4];
		p6.text = "6.\t\t " + names [5] + "\t\t " + val [5];
		p7.text = "7.\t\t " + names [6] + "\t\t " + val [6];
		p8.text = "8.\t\t " + names [7] + "\t\t " + val [7];
		p9.text = "9.\t\t " + names [8] + "\t\t " + val [8];
		p10.text = "10.\t " + names [9] + "\t\t " + val [9];
	}
}
