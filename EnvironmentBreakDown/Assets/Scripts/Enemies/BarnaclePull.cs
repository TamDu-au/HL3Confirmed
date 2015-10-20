


using UnityEngine;
using System.Collections;

public class BarnaclePull : MonoBehaviour 
{

	/*
	 * TODO: 
	 * Disable the player's movement script on trigger, re-enable on exit.
	 * Damage player when it has reached the top.
	 * 
	 */ 

	public float pullTimer = 2f; // How long an object is pulled before there is a pause.
	private float resetPullTimer = 2f;
	public float pullSpeed = 5f;
	private bool atTop = false;
	private Rigidbody objRgb;
	public float damage;
	private void Start()
	{
		resetPullTimer = pullTimer;
	}
	
	private void Update()
	{
//		if (atTop)
//		{
//			Health health = objRgb.GetComponent<Health>();
//			if (health)
//			{
//				health.healthModifier(-damage);
//			}
//		}
	}

	private IEnumerator pullObject(Rigidbody collidedRigidbody)
	{
		while(Vector3.Distance(transform.parent.position, collidedRigidbody.transform.position) > 1.5f)
		{
			pullTimer -= Time.deltaTime;

			if(pullTimer < 0)
			{
				yield return new WaitForSeconds(0.333f);
				pullTimer = resetPullTimer;
			}
			else
			{
				collidedRigidbody.MovePosition(collidedRigidbody.transform.position + Vector3.up * pullSpeed * Time.deltaTime);
				yield return null;
			}
		}

		//reach top now make fall
		if (collidedRigidbody.gameObject.tag != "Player")
		{
			collidedRigidbody.AddForce(Vector3.right * 10, ForceMode.Impulse);
		}
//		else
//		{
//			//atTop = true;
//		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Rigidbody collidedRigidbody = other.gameObject.GetComponent<Rigidbody>();
		if(collidedRigidbody != null)
		{
			objRgb = collidedRigidbody;
			collidedRigidbody.useGravity = false;
			StartCoroutine (pullObject(collidedRigidbody));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Rigidbody collidedRigidbody = other.gameObject.GetComponent<Rigidbody>();

		if(collidedRigidbody != null)
		{
			StopAllCoroutines();
			pullTimer = resetPullTimer;
			collidedRigidbody.useGravity = true;
			collidedRigidbody = null;
			atTop = false;
		}
	}

	public void turnOff()
	{
		StopAllCoroutines();
		if (objRgb) {
			objRgb.useGravity = true;
			objRgb = null;
		}
	}
}