using UnityEngine;
using System.Collections;

public class SetTower : MonoBehaviour 
{
  public int Selected;


  [System.Serializable]
  public class Tower
  {
    public string TowerName;
    public GameObject TowerGo;
    public int cost;
    public float ConstructSpeedRate;

  }

  public Tower[] tower;
  public GameObject tile;
  public PlayerProfile pp;
  public LayerMask _layerMask;
	// Use this for initialization
	void Start () {
    pp = GameObject.Find("GameMaster").GetComponent<PlayerProfile>();
    if (pp == null)
    {
      Debug.Log("Player cannot be founded");
    }
	}
  
	void Update () 
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;// = Physics.RaycastAll(ray);

    if (Physics.Raycast(ray, out hit, 20, _layerMask))
    {
      if (hit.transform.tag == "Tile")
      {
        tile = hit.transform.gameObject;
      }
      else
      {
        tile = null;
      }
    }
    
    if (Input.GetMouseButtonDown(1) && tile != null)
    {
      Tile tileTaken = tile.GetComponent<Tile>();
      if (!tileTaken.IsTaken&&pp.Gold>tower[Selected].cost)
      {
        pp.Gold -= tower[Selected].cost;
        Vector3 pos = new Vector3(tile.transform.position.x,0.3f,tile.transform.position.z);
        tileTaken.Tower = (GameObject)Instantiate(tower[Selected].TowerGo, pos, Quaternion.identity);
        tileTaken.IsTaken = true;
      }

    }
	
	}

  public void SelectTower(int _selected)
  {
    Selected = _selected;
  }
}
