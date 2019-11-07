using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterProperties : MonoBehaviour {
	//Create my variables for later use
	public float jumpForce = 1f;
    private bool canJump = false;
	public float speed = 5f;
	Animator animator;

	public float fireRate = 0;
	public float damageShot = 10;
	public LayerMask whatToHit;

	public float spawnRate = 10;
	public float timeToSpawnEffect = 0;

	Transform firePoint;
	Transform firePointC;
	Transform firePointU;
	Transform PlayerShoot;
	Transform PlayerJump;
	Transform PlayerDamaged;

	private float DamageShake = 0.1f;
	cameraShake camShake; 

	public Transform BulletPreFab;


	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>(); //Getting the animator so I can change the properties

		firePoint = transform.Find ("firePos"); //Find the firePos object
		if (firePoint == null) {
			Debug.LogError ("No Fire Point"); //Return an error if not found
		}
		firePointC = transform.Find ("firePosC"); //Find the firePos object
		if (firePoint == null) {
			Debug.LogError ("No Fire Point C"); //Return an error if not found
		}
		firePointU = transform.Find ("firePosU"); //Find the firePos object
		if (firePoint == null) {
			Debug.LogError ("No Fire Point U"); //Return an error if not found
		}
		PlayerShoot = transform.Find ("PlayerShoot"); //Find the PlayerShoot object
		if (PlayerShoot == null) {
			Debug.LogError ("No Player Shoot Sound"); //Return an error if not found
		}
		PlayerJump = transform.Find ("PlayerJump"); //Find the PlayerJump object
		if (PlayerJump == null) {
			Debug.LogError ("No Player Jump Sound"); //Return an error if not found
		}
		PlayerDamaged = transform.Find ("PlayerDamaged"); //Find the PlayerDamaged object
		if (PlayerDamaged == null) {
			Debug.LogError ("No Player Damaged Sound"); //Return an error if not found
		}
	}

	void Start () {
		camShake = GameScript.gm.GetComponent<cameraShake> ();
		if (camShake == null) {
			Debug.LogError ("No Camera Shake");
		}
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey ("s")) { //Check if SPACE is down and S is not down
            jumping(); //Call jump function
			animator.SetBool("jumping", true); //Set the jumping property of animator to true
        }
		if (Input.GetKey ("d") && !Input.GetKey ("s")) { //Check if D is pressed and S is not
			animator.SetBool ("moving", true); //Set moving to true in properties of animator
			transform.position += Vector3.right * Time.deltaTime *10; //Move the character right
			transform.eulerAngles = new Vector2(0,0); //Changes the direction character is facing based on movement

		} 

		if (Input.GetKey ("a") && !Input.GetKey ("s")) { //Check if A is pressed and S is not
			animator.SetBool ("moving", true); //Set property moving to true
			transform.position += Vector3.left * Time.deltaTime *10; //Move character left
			transform.eulerAngles = new Vector2(0,180); //Flip the animation towards direction of movement
		} 
		else if (!Input.GetKey ("a") && !Input.GetKey ("d") || Input.GetKey ("s")){ animator.SetBool ("moving", false); //If A & D is not pressed, or S is pressed, stop moving
		}

		if (Input.GetKey ("w") && !Input.GetKey ("s")) { //If W is pressed and S is not, set AimUp to true
			animator.SetBool ("aimUp", true);
		} else if (!Input.GetKey ("w")){ animator.SetBool ("aimUp", false); //If W is not pressed set it to false
		}

		if (Input.GetKey ("s")) { //Check if S is pressed
			animator.SetBool ("crouch", true); //Set crouch property to true
		} else if (!Input.GetKey ("s")){ animator.SetBool ("crouch", false); //If S is not pressed change property to false
		}

		if (Input.GetKeyDown ("right shift")) { //Check if Right shift is pressed
			animator.SetTrigger ("isShooting"); //Set off trigger property isShooting
			if (fireRate == 0) { //Check firerate to start shooting
				Shoot (); //Call function
			}
		} 

	}

    void jumping () { //Jump function adds a force upwards
        if (canJump == true) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			PlayerJump.GetComponent<AudioSource> ().Play ();
            canJump = false; //Stops multiple jumps while in mid air
        }
        
    }

	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "ground") { //If the tag ground is found, set jumping False
			animator.SetBool ("jumping", false);
			canJump = true; //Allow jumping as it is not grounded
		}

		if (coll.gameObject.tag == "Enemy") { //If tag is Enemy, set off trigger GotHit
			animator.SetTrigger ("gotHit");
			PlayerDamaged.GetComponent<AudioSource> ().Play ();
			camShake.Shake (DamageShake, 0.2f);
		}

		if (coll.gameObject.tag == "boss1") { //If tag is Enemy, set off trigger GotHit
			animator.SetTrigger ("gotHit");
			PlayerDamaged.GetComponent<AudioSource> ().Play ();
			camShake.Shake (DamageShake, 0.2f);
		}
    }

	void Shoot () {
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			PlayerShoot.GetComponent<AudioSource> ().Play ();
			timeToSpawnEffect = Time.time + 1/spawnRate;
		}
	}

	void Effect () {
		if (Input.GetKey ("s")) {
			Instantiate (BulletPreFab, firePointC.position, firePointC.rotation); //Changes the fire position based on buttons pressed
		} 

		if (Input.GetKey ("w")) {
			Instantiate (BulletPreFab, firePointU.position, firePointU.rotation);
		} 

		if (!Input.GetKey ("w") && !Input.GetKey ("s")){
			Instantiate (BulletPreFab, firePoint.position, firePoint.rotation);
		}



	}

}
    
    