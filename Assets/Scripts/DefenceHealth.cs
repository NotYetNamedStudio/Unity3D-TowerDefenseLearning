using UnityEngine;
using System.Collections;

public class DefenceHealth : MonoBehaviour {
	

	public int CurHealth;
	public int MaxHealth;

	void Start () {

	}
	public void AdJustCurrentHealth(int adj)
	{
		CurHealth += adj;
		if(CurHealth<=0)
		{
			Dead();
		 }
		if (CurHealth < 0)
			CurHealth = 0;
		
		if (CurHealth > MaxHealth)
			CurHealth = MaxHealth;
		/*if (MaxHealth < 1)
			MaxHealth = 1;*/
		
		 
	}
	public void Dead()
	{
		Destroy(this.gameObject);
	}
}
