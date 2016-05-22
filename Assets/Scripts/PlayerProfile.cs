using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour 
{
  public int Exp;
  public int Gold;
  public int Level;
  public int ExpToNextlevel;
  int LevelModifier=2;
  public Text GoldDisplay;
	// Use this for initialization
	void Start () 
  {
    //Exp = 0;
    Gold=750;
    //Level=1;
    //ExpToNextlevel = 100;
	}
	
	// Update is called once per frame
	void Update () 
  {
    GoldDisplay.text = "Gold:" + Gold;
	}
}
