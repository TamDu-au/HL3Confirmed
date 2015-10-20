using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour 
{
	public float mouseSensitivity;
	public Camera playerCam;
	
	private GameObject pauseMenu;
	
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		checkMouseMovement();
	}
	private void checkMouseMovement()
	{
		float h = mouseSensitivity * Input.GetAxis("Mouse X");
		float v = mouseSensitivity * Input.GetAxis("Mouse Y");
		playerCam.transform.Rotate(-v, 0, 0);
		playerCam.transform.eulerAngles = new Vector3(playerCam.transform.eulerAngles.x, transform.eulerAngles.y, 0f);
		transform.Rotate(0, h, 0);
	}
}
