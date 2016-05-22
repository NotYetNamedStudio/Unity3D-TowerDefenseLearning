using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  public bool IsTaken;
  public GameObject Tower;
  public MeshRenderer rend;
  public Color color;
  PlayerProfile pp;
  void Start()
  {
    rend = GetComponent<MeshRenderer>();
    pp = GameObject.FindObjectOfType<PlayerProfile>();
    color = rend.material.GetColor("_Color");
  }
 
}
