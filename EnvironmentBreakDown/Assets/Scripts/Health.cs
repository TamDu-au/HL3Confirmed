using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float health = 100f;
	public bool isZombie;
	public bool isBarnacle;
	private void Start()
	{
		
	}
	
	public void healthModifier(float amount)
	{
		health += amount;
		checkHealth();
	}
	
	private void checkHealth()
	{
		if (health <= 0)
		{
			if (isZombie)
			{
				Zombie zomb = GetComponentInParent<Zombie>();
				if (zomb)
				{
					zomb.headCrabMode = true;
				}
			}
			else if (isBarnacle)
			{
				BarnaclePull barn = GetComponentInChildren<BarnaclePull>();
				if (barn)
				{
					barn.turnOff();
					Destroy(gameObject);
				}
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
