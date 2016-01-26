using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using UnityStandardAssets.Characters.FirstPerson;

public class SoundMonitor : MonoBehaviour
{

	

    // Use this for initialization
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public int numberOfPlayerObjects = 20;
    public int numberOfEnemyObjects = 20;

    public GameObject[] playerCubes;
    public GameObject[] enemyCubes;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
    private EnemyAI enemy1;
    private EnemyAI enemy2;
    public AudioClip[] footstepSounds;

    public float enemydistance;
    public float stepInterval = 0.5f;
    public float[,] spectest = new float[1024, 1];

    void Start()
    {

        int x = 0;
        for (int i = 0; i < numberOfEnemyObjects; i++)
        {

            Vector3 pos = new Vector3(transform.position.x-25 + x, transform.position.y-5, transform.position.z +1);
            GameObject c = (GameObject) Instantiate(enemyPrefab,pos,Quaternion.identity);
            //c.transform.parent = transform;
           // c.transform.position = pos;
            x = x + 2;
        }
        int y = 1;
        for (int j = 0; j < numberOfPlayerObjects; j++)
        {

            Vector3 pos = new Vector3(transform.position.x-25 + y, transform.position.y-5, transform.position.z+1);
            GameObject c = (GameObject) Instantiate(playerPrefab, pos, Quaternion.identity);
            //c.transform.parent = transform;
            //c.transform.position=new Vector3(0 + x, 0, 1);
            y = y + 2;
        }

        playerCubes = GameObject.FindGameObjectsWithTag("playerCubes");
        enemyCubes = GameObject.FindGameObjectsWithTag("enemyCubes");
        controller = GameObject.FindObjectOfType<FirstPersonController>();
        var test = GameObject.Find("Enemy");
        var test1 = GameObject.Find("Enemy2");
        enemy1 = test.GetComponent<EnemyAI>();
        enemy2 = test1.GetComponent<EnemyAI>();
        //enemy1 = GameObject.FindObjectOfType<EnemyAI> ();
        var sound =	enemy1.GetComponent<AudioClip>();

        //Debug.Log("SOUND:" + sound);

        for (int i = 0; i < 1023; i++)
        {


            int test12434 = Random.Range(1, 100);
            float num = (float)test12434 / 100;
           // Debug.Log(num);
            spectest[i, 0] = num; 
        }

       // Debug.Log("test" + spectest[555, 0]);

        for (int i = 0; i < enemyCubes.Length; i++)
        {
            Vector3 prev = enemyCubes[i].transform.localScale;

            prev.y = 0.0f;
            enemyCubes[i].transform.localScale = prev; 
        }
        for (int i = 0; i < playerCubes.Length; i++)
        {
            Vector3 prev = playerCubes[i].transform.localScale;

            prev.y = 0.0f;
            playerCubes[i].transform.localScale = prev; 
        }
    }
	
    // Update is called once per frame
    void Update()
    {
		
//		if (controller.playedSound != false) {
//			//Debug.Log ("Played" + controller.playedSound);
//			showPlayerSpectrum();
//			Debug.Log ("Player Played Sound");
//
//		}

        if (controller.IsMakingNoise())
        {
            showPlayerSpectrum(true);

        }
        else
        {

            showPlayerSpectrum(false);
        }


        if ((enemy1.playedSound = true) || (enemy2.playedSound = true))
        {



            if ((enemy1.playedSound = true) && (enemy2.playedSound = true))
            {
                //Debug.Log("Distance enemy1 " + enemy1.enemyDistance + " Distance enemy2  : " + enemy2.enemyDistance);

                if ((enemy1.enemyDistance != 0) && (enemy2.enemyDistance != 0))
                {

                    if (enemy1.enemyDistance <= enemy2.enemyDistance)
                    {
                        var distance = enemy1.enemyDistance;
                        var realVol = 1 - distance / enemy1.enemyMaxDistance;
                        showSpectrum(realVol);
                        return;

                    }

                    if (enemy2.enemyDistance < enemy1.enemyDistance)
                    {
                        var distance = enemy2.enemyDistance;
                        var realVol = 1 - distance / enemy2.enemyMaxDistance;
                        showSpectrum(realVol);
                        return;

                    }
                } 

                if (enemy1.enemyDistance != 0)
                {

                    var distance = enemy1.enemyDistance;
                    var realVol = 1 - distance / enemy1.enemyMaxDistance;
                    showSpectrum(realVol);
                    return;

                }

                if (enemy2.enemyDistance != 0)
                {
                    var distance = enemy2.enemyDistance;
                    var realVol = 1 - distance / enemy2.enemyMaxDistance;
                    showSpectrum(realVol);
                    return;

                }

                return;
            }

        }
    }


    void showSpectrum(float realVol)
    {
        for (int i = 0; i < enemyCubes.Length; i++)
        {
	
            Vector3 prev = enemyCubes[i].transform.localScale;
            int ran = Random.Range(0, 1023);
            prev.y = spectest[ran, 0] * realVol; 
            prev.y = prev.y * 7.0f;
            if (realVol >= 0.1)
            {
                prev.y = prev.y * 1.5f;
            }
            if (realVol >= 0.3)
            {
                prev.y = prev.y * 1.5f;
            }
            if (realVol >= 0.5)
            {
                prev.y = prev.y * 1.5f;
            }
            if (realVol >= 0.6)
            {
                prev.y = prev.y * 1.1f;
            }
		
            enemyCubes[i].transform.localScale = prev;

        }
    }



    void showPlayerSpectrum(bool noise)
    {

        if (noise)
        {


            if (controller.isWalking())
            {

                for (int i = 0; i < playerCubes.Length; i++)
                {

                    Vector3 prev = playerCubes[i].transform.localScale;

                    int ran = Random.Range(0, 1023);
                    prev.y = spectest[ran, 0];
                    prev.y = prev.y * 9.0f;
                    playerCubes[i].transform.localScale = prev; 
                }
            }
            else
            {

                for (int i = 0; i < playerCubes.Length; i++)
                {

                    Vector3 prev = playerCubes[i].transform.localScale;

                    int ran = Random.Range(0, 1023);
                    prev.y = spectest[ran, 0];
                    prev.y = prev.y * 15.0f;
                    playerCubes[i].transform.localScale = prev; 
                }
            }

        }
        else
        {

            for (int i = 0; i < playerCubes.Length; i++)
            {

                Vector3 prev = playerCubes[i].transform.localScale;
                prev.y = 0.0f;
                playerCubes[i].transform.localScale = prev; 
            }
        }

    }
}


