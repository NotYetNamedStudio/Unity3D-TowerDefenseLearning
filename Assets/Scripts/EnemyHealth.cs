using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

	public int CurHealth;
	public int MaxHealth;
  public int GiveExp;
  public int GiveGold;
  public PlayerProfile pp;
	// Use this for initialization
	void Start ()
	{
    pp = GameObject.Find("GameMaster").GetComponent<PlayerProfile>();
    if (pp == null)
    {
      Debug.Log("Player cannot be founded");
    }
	}
	public void AdJustCurrentHealth (int adj)
	{

		CurHealth += adj;
		//SendMessage ("TakenHit");
		if (CurHealth <= 0) {
			Dead ();
		}
		if (CurHealth < 0)
			CurHealth = 0;
		
		if (CurHealth > MaxHealth)
			CurHealth = MaxHealth;		
	}
	public void Dead ()
	{
   // pp.Gold += GiveGold;
    pp.Exp += GiveExp;
		//SendMessage ("PlayDead");
		Destroy (this.gameObject);
	}
	public void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Bullet")) {
			//AdJustCurrentHealth(-25);
		}
	}
}
