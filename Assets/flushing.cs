using UnityEngine;
using System.Collections;



public class flushing : MonoBehaviour {
    private Canvas canvas;
    private GameObject toiletText;
    private GameObject textToilet;
    private bool flush = false;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();

        toiletText = Resources.Load ("Prefabs/toiletText") as GameObject;
        textToilet = Instantiate (toiletText, new Vector2(Screen.width/2,20), Quaternion.identity) as GameObject;
        textToilet.transform.parent = canvas.transform;
        textToilet.SetActive (false);
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (flush)
        {
            if (Input.GetKeyDown("f"))
            {
                audio.Play();
            }
        }
	    
	}

    void OnTriggerEnter(Collider col){
        if (col.tag == "Player")
        {
            flush = true;
            textToilet.SetActive (true);
        }
    }
    void OnTriggerExit(Collider col){
        if (col.tag == "Player")
        {
            flush = false;
            textToilet.SetActive (false);
            audio.Stop();
        }
    }
}
