using UnityEngine;
using System.Collections;

public class KnockoutEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("Enemy")) {
			Debug.Log ("ignore");
			Physics.IgnoreCollision(g.GetComponent<SphereCollider>(), GetComponent<BoxCollider>());
		}


	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("on");
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "EnemyBody") {
			Debug.Log ("Enemy Knockout ");

			EnemyAI enemyAI = other.GetComponentInParent <EnemyAI>();

			enemyAI.Knockout ();
			Destroy (gameObject);
		}
	}
}
