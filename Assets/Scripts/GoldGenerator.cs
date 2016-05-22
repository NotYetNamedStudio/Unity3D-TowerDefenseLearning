using UnityEngine;
using System.Collections;

public class GoldGenerator : MonoBehaviour {

  public float cooldown;
  float cd;
  public int GoldPerSec;
  public PlayerProfile pp;
	// Use this for initialization
	void Start () {
    cd = cooldown;
    pp = GameObject.Find("GameMaster").GetComponent<PlayerProfile>();
    if (pp == null)
    {
      Debug.Log("Player cannot be founded");
    }
	}
	
	// Update is called once per frame
	void Update () 
  {
    if (cd > 0)
    {
      cd -= Time.deltaTime;
    }
    else
    {
      cd = cooldown;
      pp.Gold += GoldPerSec;
    }
	}
}
