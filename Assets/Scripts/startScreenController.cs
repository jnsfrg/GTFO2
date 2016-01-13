using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
* Controller for the startscreen specific tasks.
*/
public class startScreenController : MonoBehaviour
{
	public InputField inputField;

	void Start ()
	{	// Sets the last saved Palyer name as PlayerName
		string name = PlayerPrefs.GetString ("PlayerName");
		if (name.Length > 0) {
			inputField.text = name;
		}
	}

}
