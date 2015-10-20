using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour 
{
	[Header ("Zombie")]
	public float moveSpeed = 20; 
	public string playerTag = "Player";
	private GameObject player;
	public bool move;
	public float zombieDamage = 10;
	public GameObject blood;

	[Header ("Kill")]
	public GameObject zombieHalf;
	public GameObject headCrabHalf;
	public bool headCrabMode;
	public float pushBack = 20;
	private bool isHeadcrab;

	// Update is called once per frame
	void Update () 
	{
		if (move == true) 
		{
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, moveSpeed * Time.deltaTime);
		}

		if (headCrabMode == true) 
		{
			isHeadcrab = true;
			headCrabMode = false;
			headCrabTime ();
			headCrabHalf.GetComponent <Jumpy>().jumpy = true;
			moveSpeed = 0;
			blood.SetActive (true);
			
		}

	}


	void headCrabTime ()
	{
		zombieHalf.AddComponent <Rigidbody> ();
		zombieHalf.GetComponent <Rigidbody> ().AddRelativeForce (Vector3.left * pushBack, ForceMode.Impulse);
		Vector3 newV = new Vector3 (Random.Range (0,2), Random.Range (0,2), Random.Range (0,2));
		zombieHalf.GetComponent <Rigidbody> ().AddRelativeForce (newV * pushBack / 2, ForceMode.Impulse);
		headCrabHalf.AddComponent <Rigidbody> ();
		Destroy (gameObject.GetComponent <Rigidbody> ());
		zombieHalf.transform.parent = null;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			move = true;
			player = other.gameObject;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == playerTag)
		{
			move = false;
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (other.gameObject.tag == playerTag)
		{
			if (isHeadcrab == false)
			{
				// use zombieDamage
				Health healthComponent = other.gameObject.GetComponent<Health>();
				if (healthComponent && other.gameObject.tag == "Player")
				{
					float zombDam = zombieDamage * 10 * Time.deltaTime;
					healthComponent.healthModifier(-zombDam);
				}
			}
		}
	}
}
