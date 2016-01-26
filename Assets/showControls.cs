using UnityEngine;
using System.Collections;

public class showControls : MonoBehaviour {
    GameObject textControl;
	// Use this for initialization
	void Start () {
        Canvas canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        GameObject control = Resources.Load ("Prefabs/controlText") as GameObject;
        textControl = Instantiate (control, new Vector2(Screen.width*0.01f,Screen.height/2), Quaternion.identity) as GameObject;
        textControl.transform.parent = canvas.transform;
        textControl.SetActive (false);
	}
	
	
    public void OnTriggerExit(Collider other){
        if (other.tag == "Player")
        {
            textControl.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player")
        {
            textControl.SetActive(true);
        }
    }
}
