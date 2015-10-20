using UnityEngine;
using System.Collections;

public class Acid : MonoBehaviour {

	public float acidDamage;
//	private float objectHealth;
	
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject != null)
		{
			float dam = acidDamage * Time.deltaTime;
			Health healthComponent = col.gameObject.GetComponent<Health>();
			if (healthComponent)
			{
				healthComponent.healthModifier(-dam);
			}
		}	
	}
}
