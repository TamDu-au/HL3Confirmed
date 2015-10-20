using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour 
{
	public float jumpHeight;
//	public float landThreshold = 0.8f;
	private bool canJump = true;
	private Rigidbody myRgb;
	
	private void Start()
	{
		myRgb = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate()
	{
		playerJump();
	}
	
	private void playerJump()
	{
		if (canJump == true && Input.GetKey(KeyCode.Space))
		{
			myRgb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
		}
	}
	
	private void OnCollisionEnter(Collision colObj)
	{
		canJump = true;
//		Vector3 contactPoint = colObj.contacts[0].normal;
//		if (contactPoint.y > landThreshold)
//		{
//			canJump = true;
//		}
	}
	
	private void OnCollisionExit(Collision colObj)
	{
		canJump = false;
	}
}
