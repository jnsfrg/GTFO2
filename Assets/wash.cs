using UnityEngine;
using System.Collections;

public class wash : MonoBehaviour
{
    private bool washed = false;
    private AudioSource a;
    private SkinnedMeshRenderer arms;
    private GameController gameController;
    // Use this for initialization
    void Start()
    {
        a = GetComponent < AudioSource>();
        arms = GameObject.Find("Arm_Mesh").GetComponent<SkinnedMeshRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!washed)
            {
                if (Input.GetKeyDown("f"))
                {
                    a.Play();
                    gameController.IncreaseScore();
                    gameController.IncreaseScore();
                    gameController.IncreaseScore();
                    StartCoroutine("washing");
                }
            }
        }
    }

    IEnumerator washing()
    {
        yield return new WaitForSeconds(a.clip.length);
       
        arms.material.mainTexture = (Texture)Resources.Load("ArmsDiffuseWhite", typeof(Texture));
        washed = true;
    }
}
