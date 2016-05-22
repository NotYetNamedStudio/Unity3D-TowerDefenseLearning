using UnityEngine;
using System.Collections;

public class ShootTarget : MonoBehaviour
{

  public Rigidbody Bullet;
  public Transform ShootingPosition;
  public float AttackTimer;
  public float CoolDown;
  public bool isShoot = false;
  public AnimationClip attackAnim;
  Rigidbody Clone;
  public int Speedvelocity = 5;
  // Use this for initialization
  void Start()
  {
    Bullet.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {

  }
  Transform t = null;
  public void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Enemy"))
    {
      if (t == null)
      {
        AttackTimer = 0;
        t = other.transform.FindChild("TargetAimPos");
      }
      //GetComponent<Animation> ().CrossFade (attackAnim.name);
      if (AttackTimer > 0)
        AttackTimer -= Time.deltaTime;
      if (AttackTimer < 0)
        AttackTimer = 0;
      if (AttackTimer == 0)
      {
        StartCoroutine("Attack");
        AttackTimer = CoolDown;
      }
    }
  }
  float angle = 0;
  IEnumerator Attack()
  {
    if (t == null)
      yield return null;
    if (attackAnim != null)
    {
      GetComponent<Animation>().CrossFade(attackAnim.name);

      yield return new WaitForSeconds(GetComponent<Animation>().GetClip(attackAnim.name).length - 0.75f);
    }
    else
    {
      yield return new WaitForSeconds(0);
    }
    try
    {
      Clone = Instantiate(Bullet, ShootingPosition.position, ShootingPosition.rotation) as Rigidbody;
      Clone.gameObject.SetActive(true);
      if (t != null)
      {
        Vector3 dir = t.transform.position - ShootingPosition.transform.position;
        angle = Vector3.Angle(dir, transform.forward);
        Clone.AddForce(transform.rotation * Quaternion.Euler(angle, 0, 0) * transform.forward * Speedvelocity); 
      }
       
    }
    catch (UnityException ex)
    {
      Debug.Log("Message"+ex.Message);
    }
    Destroy(Clone.gameObject, 2);
  }

}
