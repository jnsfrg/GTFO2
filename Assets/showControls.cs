using UnityEngine;
using System.Collections;

public class showControls : MonoBehaviour {
    GameObject textControl;
    GameObject textEnemie;
    private bool activeEnemies=true;
    private GameObject[] enemies;
	// Use this for initialization
	void Start () {
        Canvas canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        GameObject control = Resources.Load ("Prefabs/controlText") as GameObject;
        textControl = Instantiate (control, new Vector2(Screen.width*0.01f,Screen.height/2), Quaternion.identity) as GameObject;
        textControl.transform.parent = canvas.transform;
        textControl.SetActive (false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject controlEnemyText = Resources.Load("Prefabs/enemyText") as GameObject;
        textEnemie = Instantiate(controlEnemyText, new Vector2(Screen.width/2, 0), Quaternion.identity) as GameObject;
        textEnemie.transform.parent = canvas.transform;
        textEnemie.SetActive(false);
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

    public void OnTriggerStay(Collider other) {

        if (other.tag == "Player") {
            if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.O)) {
                foreach (GameObject g in enemies ) {
                        g.SetActive(!activeEnemies);
                }
                activeEnemies = !activeEnemies;
                textEnemie.SetActive(!activeEnemies);
            }

        }
    }
}
