using UnityEngine;
using System.Collections;

public class Jumpy : MonoBehaviour {

	public float moveSpeed = 0; 
	public bool jumpy;
	public float timer = 3;
	public GameObject player;
	public string playerTag = "Player";

	[Header ("HeadCrab")]
	public float jumpSeepd = 500;
	private float originalTimer;
	public float moveSpeedChange = 10;
	public float headCrabDamage = 30;

	
	
	void Start ()
	{
		originalTimer = timer;
	}

	// Update is called once per frame
	void Update () {
		if (jumpy == true) 
		{
			StartCoroutine (jump ());
			if (player != null)
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		}
	}

	
	private IEnumerator jump ()
	{
		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			moveSpeed += moveSpeedChange;
			if (player != null)
			GetComponent <Rigidbody> ().AddForce (Vector3.up * jumpSeepd);
			
			timer = originalTimer;
			yield return new WaitForSeconds (1.5f);
			moveSpeed -= moveSpeedChange;
		}
		
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == playerTag) 
		{
			//use headcrab damage
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == playerTag) {
			player = other.gameObject;
		}
	}

}
