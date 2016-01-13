using UnityEngine;
using System.Collections;

public class CubePlane : MonoBehaviour
{

	public GameObject player;
	private GameObject[,] boxesArray;
	private Texture2D texture;
	private Color fogOfWarColor;
	private Color transparentColor;

	
	void Start ()
	{

		boxesArray = new GameObject[100, 100];
		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				boxesArray [i, j] = cube;
				cube.transform.position = new Vector3 (i, 30, j);
				cube.AddComponent<BoxCollider> ();
				cube.name = i + " " + j;
				cube.layer = 8;
				cube.GetComponent<Renderer> ().material.color = Color.gray;
			}
		}

//		fogOfWarColor = new Color (170, 170, 170, 1);
//		transparentColor = new Color (200, 200, 200, 0.5f);
//		// renders the objects
//		var renderer = GetComponent<Renderer> ();
//		// get the material of object
//		Material fogOfWarMat = null;
//		if (renderer != null) {
//			fogOfWarMat = renderer.material;
//		}
//		texture = new Texture2D (100, 100, TextureFormat.RGBA32, false);
//		texture.wrapMode = TextureWrapMode.Clamp;
//		fogOfWarMat.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var ray = new Ray (player.transform.position, Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			var strs = hit.collider.gameObject.name.Split ();
			if (strs.Length > 1) {
				drawCirlceAroundCenter (strs [0], strs [1]);
			}
		}
	}

	void drawCirlceAroundCenter (string str, string str2)
	{
		int num1, num2;
		Debug.Log ("Hit stuff at: " + int.TryParse (str, out num1) + ";" + int.TryParse (str2, out num2));
		Debug.Log ("Int1 :" + num1 + "Int2 :" + num2);

		for (int p = 0; p <= 4; p++) {

			if (p == 0) {
				for (int i = num1 - 1; i < num1 + 2; i++) {
					if (i < 100 && (num2 - 2) < 100)
						boxesArray [i, num2 - 2].gameObject.SetActive (false);
					//texture.SetPixel(i, num1 -2, transparentColor);
				}
			} else if (p == 4) {
				for (int i = num1 - 1; i < num1 + 2; i++) {
					if (i < 100 && (num2 + 2) < 100)
						boxesArray [i, num2 + 2].gameObject.SetActive (false);
					//texture.SetPixel(i, num2 -2, transparentColor);
				}
			} else {
				for (int i = num1 - 2; i < num1 + 3; i++) {
					for (int j = num2 - 1; j < num2 + 2; j++) {
						if (i < 100 && j < 100)
							boxesArray [i, j].gameObject.SetActive (false);
					}
				}
			}
		}
	}
}