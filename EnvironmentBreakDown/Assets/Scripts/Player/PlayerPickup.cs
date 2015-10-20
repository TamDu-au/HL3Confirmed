using UnityEngine;
using System.Collections;

public class PlayerPickup : MonoBehaviour 
{
	[Header("Player Controls")]
	public KeyCode interactKey = KeyCode.E;
	public KeyCode toggleThrow = KeyCode.LeftControl;
	[Header("Pickedup Variables")]
	public float throwForce= 10f;
	public bool isKinematicMode;
	[Header("Pickup detection variables")]
	public GameObject position;
	public float radius = 0.5f;
	public LayerMask pickupableLayers;
	[Header("Required Components")]
	public GameObject playerCamera;
	private GameObject equippedGO;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(interactKey))
		{
			if (equippedGO)
			{
				if (Input.GetKey(toggleThrow))
				{
					deEquip(false);
				}
				else
				{
					deEquip(true);
				}
			}
			else
			{
				getColliders();
			}
		}
	}
	
	private void getColliders()
	{
		Collider[] pickupableObjects = Physics.OverlapSphere(position.transform.position, radius, pickupableLayers);
		for (int i = 0; i < pickupableObjects.Length; i++)
		{
			Rigidbody pickRgb = pickupableObjects[i].gameObject.GetComponent<Rigidbody>();
			if (pickRgb)
			{
				equipPickup(pickRgb);
				break;
			}
		}
	}
	
	private void equipPickup(Rigidbody pickedupRgb)
	{
		equippedGO = pickedupRgb.gameObject;
		pickedupRgb.useGravity = false;
		pickedupRgb.transform.SetParent(playerCamera.transform);
		if (isKinematicMode)
			pickedupRgb.isKinematic = true;
		else
			pickedupRgb.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	private void deEquip(bool canThrow)
	{
		Rigidbody equippedRgb = equippedGO.GetComponent<Rigidbody>();
		if (isKinematicMode)
			equippedRgb.isKinematic = false;
		else
			equippedRgb.constraints = RigidbodyConstraints.None;
		equippedRgb.useGravity = true;
		equippedGO.transform.SetParent(null);
		if (canThrow)
		{
			equippedRgb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
		}
		equippedGO = null;
	}
}
