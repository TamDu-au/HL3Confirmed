using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour 
{
	public KeyCode forward = KeyCode.W;
	public KeyCode left = KeyCode.A;
	public KeyCode back = KeyCode.S;
	public KeyCode right = KeyCode.D;
	private Rigidbody myRgb;
	public float speed = 10f;
	private void Start()
	{
		myRgb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		checkKeys();
	}	
	
	private void checkKeys()
	{
		Vector3 direction = Vector3.zero;
		if (Input.GetKey(forward))
		{
			direction += transform.forward;
		}
		
		if (Input.GetKey(left))
		{
			direction += -transform.right;
		}
		
		if (Input.GetKey(back))
		{
			direction += -transform.forward;
		}
		
		if (Input.GetKey(right))
		{
			direction += transform.right;
		}
		
		myRgb.MovePosition(transform.position + direction  * speed * Time.deltaTime);
	}
}
