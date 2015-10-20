using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour 
{
	public float shootDelay = 0.1f;
	public float damageAmount = 10f;
	public float weaponRange = 50f;
	public LayerMask targetLayer;
	private bool canFire = true;
	public Camera playerCam;
	void Start () 
	{
	
	}
	
	private void Update()
	{
		//shootForward();
		if (Input.GetKey(KeyCode.Mouse0) && canFire)
		{
			StartCoroutine(fire(shootDelay));
		}
	}

	private IEnumerator fire(float timer)
	{
		canFire = false;
		shootForward();
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		canFire = true;
	}
	
	private void shootForward()
	{
		Ray camRay = playerCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		RaycastHit targetRayHit;
		if(Physics.Raycast (camRay, out targetRayHit, weaponRange, targetLayer))
		{
			Health targetHealth = targetRayHit.collider.GetComponent<Health>();
			if (targetHealth != null)
			{
				targetHealth.healthModifier(-damageAmount);
			}
		}
	}
}
