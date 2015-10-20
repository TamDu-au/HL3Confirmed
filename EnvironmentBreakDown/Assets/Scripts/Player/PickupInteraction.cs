using UnityEngine;
using System.Collections;

public class PickupInteraction : MonoBehaviour 
{
	public string[] pickupableTags;
	private GameObject pickEquipped;
	private bool canEquip = true;
	private bool delay = false;
	public Transform parent;
	
	private void Update()
	{
		if (canEquip == false && delay == false)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				pickEquipped.transform.SetParent(null);
				canEquip = true;
				Rigidbody pickRgb = pickEquipped.GetComponent<Rigidbody>();
				if (pickRgb != null)
				{
					pickRgb.isKinematic = false;
				}
				StartCoroutine(delayPickup());
			}
		}
	}
	
	private void pickup(GameObject pickupObj)
	{
		for (int i = 0; i < pickupableTags.Length; i++)
		{
			if (pickupObj.tag == pickupableTags[i])
			{
				pickupObj.transform.SetParent(parent);
				pickEquipped = pickupObj;
				Rigidbody pickRgb = pickEquipped.GetComponent<Rigidbody>();
				if (pickRgb != null)
				{
					pickRgb.isKinematic = true;
				}
				canEquip = false;
				StartCoroutine(delayPickup());
				break;
			}
		}
	}
	
	private void OnTriggerStay(Collider colObj)
	{
		if (canEquip && Input.GetKeyDown(KeyCode.E) && delay == false)
		{
			pickup(colObj.gameObject);
		}
	}
	
	private IEnumerator delayPickup()
	{
		float timer = 0.2f;
		delay = true;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		delay = false;
	}
}
