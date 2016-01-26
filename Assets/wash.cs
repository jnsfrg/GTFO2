using UnityEngine;
using System.Collections;

public class wash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	

    public void OnTriggerStay(Collider other){
        if(other.tag=="Player"){
            if(Input.GetKeyDown("f")){
                GameObject arms =  GameObject.Find("Arm_Mesh");
                if (arms != null)
                {
                    arms.GetComponent<SkinnedMeshRenderer>().material.mainTexture= (Texture)Resources.Load ("ArmsDiffuseWhite", typeof(Texture));;
                }
            }
        }
    }
}
