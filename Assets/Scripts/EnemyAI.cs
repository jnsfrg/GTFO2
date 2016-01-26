using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class EnemyAI : MonoBehaviour
{
	public Transform[] points; 
	private int destPoint = 0;
	private NavMeshAgent agent;
	public float hearingDistance;
	private FirstPersonController fpsController;
	public float fieldOfViewAngle = 180;
	private SphereCollider col;
	private GameObject player;
	private GameController gameController;
	public AudioClip[] footstepSounds;
	private AudioSource[] audioSource;
	public float stepInterval = 0.5f;
	private float nextStep = 0f;

	private Animator animator;
	public float speedDampTime = 0.1f;              // Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;       // Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;          // Response time for turning an angle into angularSpee
	public float angle = 0;

	public bool knockout = false;
	public float knockOutTime = 10.0f;

    public bool playedSound = false;
    public float enemyDistance;
    public float enemyMaxDistance;
    private int soundFlag,seeFlag = 0;


	void Start ()
	{
        
		agent = GetComponent<NavMeshAgent> ();
		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;
		GotoNextPoint ();

		player = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		fpsController = player.GetComponent<FirstPersonController> ();
		col = GetComponent<SphereCollider> ();
		audioSource = GetComponents<AudioSource> ();
		animator = GetComponent<Animator>();
	}

	void Update ()
	{
		if (!knockout) {
			if (Vector3.Distance (transform.position, player.transform.position) > hearingDistance || !fpsController.IsMakingNoise ()) { 
				/*when not in seight make patrol*/
				if (agent.remainingDistance < 0.5f) {
					GotoNextPoint ();
				}
			} else {
				/*Follow the player*/
	
				agent.destination = player.transform.position + Vector3.up; /***Vector3.Up ist ein Vektor (0,1,0), Vector3.one ist (1,1,1)***/	
			}
		} else {
			if (agent.remainingDistance < 0.5f) {
				GotoNextPoint ();
			}
		}
		float angularSpeed = angle / angleResponseTime;
		
		// Set the mecanim parameters and apply the appropriate damping to them.
		animator.SetFloat("Speed", agent.speed, speedDampTime, Time.deltaTime);
		animator.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
		
	}

	void GotoNextPoint ()
	{
		// Returns if no points have been set up
		if (points.Length == 0) {
			return;
		}
		Debug.Log (gameObject.name + " going to: " + destPoint);

		// Set the agent to go to the currently selected destination.
		agent.destination = points [destPoint].position;
		
		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}

	void playSounds (float distance, float maxDistance)
	{
		if (Time.time > nextStep) {
            playedSound = true;
            enemyDistance = distance;
            enemyMaxDistance = maxDistance;
			audioSource[0].volume = 1 - distance / maxDistance;
			nextStep = Time.time + stepInterval; 
			int n = Random.Range (1, footstepSounds.Length);
			audioSource[0].clip = footstepSounds [n];
			audioSource[0].PlayOneShot (audioSource[0].clip);
			// move picked sound to index 0 so it's not picked next time
			footstepSounds [n] = footstepSounds [0];
			footstepSounds [0] = audioSource[0].clip;
		}
	}
	void OnTriggerStay (Collider other)
	{
		//Debug.Log ("on trigger stay: " + other.tag);
		// If the player has entered the trigger sphere...
		if (other.tag == "Player"&&!knockout) {
			float distance = Vector3.Distance (player.transform.position, transform.position);
			playSounds (distance, col.radius);
          

			Vector3 direction = other.transform.position - (transform.position + Vector3.up); /***Vektor von dem Gegner + Vector3 nach oben, hin zur Position 
			des Spielers. Das mit dem Vektor3.Up hab ich gemacht, weil der Pivot Punkt des Gegners an seinen Füßen ist und der des SPielers in der Mitte***/

//			Debug.DrawRay(transform.position+transform.up, direction.normalized*col.radius, Color.green); /***gut zum debuggen, 
//			hatte ich vergessen, dass es die Methode noch gibt ;) ***/

			float angle = Vector3.Angle (direction, transform.forward);
			// If the angle between forward and where the player is, is less than half the angle of view...
			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position + Vector3.up, direction.normalized, out hit/*, col.radius infinity?*/)) { 

					// ... and if the raycast hits the player...
					//Debug.Log (hit.collider.gameObject);

					if (hit.collider.gameObject == player.gameObject) {
                        if (soundFlag == 0)
                        {
                            audioSource[2].Play();
                            soundFlag++;
                            seeFlag = 1;
                        }
						// ... the player is in sight.
						if (fpsController.IsVisibleForEnemy ()) {
							agent.destination = player.transform.position;
						}
						if (Vector3.Distance (player.transform.position, transform.position) < 2.0f) {
							gameController.endGameWithLoose (); //Besser in der Start() schon suchen
						}
					}
				} 
			}
		}

	}
	public void Knockout(){
        audioSource[1].Play();
		StartCoroutine ("knockedOut");
	}

	IEnumerator knockedOut(){
		knockout = true;
		//GetComponentInChildren<SkinnedMeshRenderer> ().material.SetColor ("_Color", Color.green);
		agent.speed = 0;
		animator.SetBool ("knockedOut",true);
		yield return new WaitForSeconds (knockOutTime);
		animator.SetBool ("knockedOut",false);
		//GetComponentInChildren<SkinnedMeshRenderer> ().material.SetColor ("_Color", Color.white);
		knockout = false;
		agent.speed = 2;

	}
    void OnTriggerExit (Collider other) {

        if (other.tag == "Player") {
            if (seeFlag == 1)
            {
                audioSource[3].Play();
                seeFlag = 0 ;
            }
                
            soundFlag = 0;
            
            playedSound = false;
        }
    }
}
