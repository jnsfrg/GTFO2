using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
		
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
		
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
			
		Vector3 movement = new Vector3 (2*moveVertical, 0.0f, -2*moveHorizontal);
			
		rb.AddForce (movement * speed);
	}
}