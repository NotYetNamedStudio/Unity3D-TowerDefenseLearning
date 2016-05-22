using UnityEngine;
using System.Collections;

public class ArrowBehaviour : MonoBehaviour {

	public int meeleDamage=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			EnemyHealth eh=(EnemyHealth)other.transform.GetComponent("EnemyHealth");
			if(eh)
			{
				eh.AdJustCurrentHealth(-meeleDamage);
				//Debug.Log ("Enemy taken hit health is reduced  by "+meeleDamage);
			}
			Destroy(this.gameObject);
		}
	}
}
