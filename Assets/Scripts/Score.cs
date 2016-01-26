using UnityEngine;
using System.Collections;

/**
*	The Component manages the counting of the score and the Highscore. 
*	Should be used by GameController (and nothing else if your not sure).
**/
public class Score : MonoBehaviour
{

	public static bool win = false;
	private static float start;
	private static int score;
	private const int baseVal = 600;
	private const int highScoreSize = 10;
	// not used for all !!!
	private const string valKey = "highScoreVal";
	private const string nameKey = "highScoreName";
	private static int[] highScoreValues = new int[10];
	private static string[] highScoreNames = new string[10];
	private static bool InHighScore = false;
	private static int rank;
    public static int  changeValue = 60;
    private int delta;

	//set win true
	public void Win ()
	{
		win = true;
	}

	// load the saved highscore at start
	public void Start ()
	{
		for (int i = 0; i < highScoreSize; i++) {
			highScoreValues [i] = PlayerPrefs.GetInt (valKey + i, 0);
			highScoreNames [i] = PlayerPrefs.GetString (nameKey + i, "No entry");
		}
        delta = 0;
	}

	// measure time TODO: measure more.
	public void StartCounting ()
	{
        delta = 0;
		start = Time.time;
		Debug.Log (start);
	}

	public int getScore ()
	{
        return score + delta;
	}

	// Stop counting (time) TODO: you get it. (more)
	public void Stop ()
	{
		//Debug.Log (PlayerPrefs.GetString ("PlayerName", "Player"));
		score = baseVal - (int)(Time.time - start);
		rank = getRank ();
		if (win) {
			if (rank <= highScoreSize) {
				InHighScore = true;
				saveScoreToHighscore (PlayerPrefs.GetString ("PlayerName", "Player"), rank);
			} else {
				InHighScore = false;
			}
		}
	}

	public bool getWin ()
	{
		return win;
	}

	public bool isInHighscore ()
	{
		return InHighScore;
	}

	// set win false
	public void Loose ()
	{
		win = false;
	}

	/**
	Saves the score to file and inserts the player at @rank position
	should be called if you are in highscore. So that there is something to save.
	**/
	public void saveScoreToHighscore (string name, int rank)
	{
		int[] tmpVal = new int[highScoreSize];
		string[] tmpNames = new string[highScoreSize];
		for (int i = 0; i < highScoreSize; i++) {
			if (i >= rank - 1) {
				if (i == rank - 1) {
                    tmpVal [i] = score+delta;
					tmpNames [i] = name;
				} else {
					tmpVal [i] = highScoreValues [i - 1];
					tmpNames [i] = highScoreNames [i - 1];
				}
			
			} else {
				tmpVal [i] = highScoreValues [i];
				tmpNames [i] = highScoreNames [i];
			}
		}
		highScoreValues = tmpVal;
		highScoreNames = tmpNames;
		for (int i = 0; i < highScoreSize; i++) {
			PlayerPrefs.SetInt (valKey + i, tmpVal [i]);
			PlayerPrefs.SetString (nameKey + i, tmpNames [i]);
		}
		PlayerPrefs.Save ();
	}

	// returns array of all values in highscore oredered by rank
	public int[] getHighscoreValues ()
	{
		return highScoreValues;
	}

	// all names ordered by rank
	public string[] getHighscoreNames ()
	{
		return highScoreNames;
	}

	// calculates the rank after game is won. (only call it if you win)
	private int getRank ()
	{
		int i;
		for (i = 0; i < highScoreSize; i++) {
            if (highScoreValues [i] < score + delta) {
				return i + 1;
			}
		}
		return i + 1;
	}

	public int getPlayerRank ()
	{
		return rank;
	}

	public int getHighscoreSize ()
	{
		return highScoreSize;
	}

	// gives you the values and the names ordered by rank.
	public static void getHighscores (out int[]val, out string[] names)
	{
		val = new int[highScoreSize];
		names = new string [highScoreSize];
		for (int i = 0; i < highScoreSize; i++) {
			val [i] = PlayerPrefs.GetInt (valKey + i, 0);
			names [i] = PlayerPrefs.GetString (nameKey + i, "No entry");
		}
	}

    public void increaseScore(){
        delta = delta + changeValue;
    }
    public void decreaseScore(){
        delta = delta - changeValue;
    }
}