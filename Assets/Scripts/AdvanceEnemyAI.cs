using UnityEngine;
using System.Collections;

public class AdvanceEnemyAI : MonoBehaviour
{

  public enum State
  {
    Idle,
    Init,
    SetUp,
    Move,
    Decide,
    Die,
    TakeHit
  }
  public enum TroopType
  {
    Ranged,
    Meele,
    Summoning
  }
  public Transform EnemyTarget;
  public Transform attackrange;
  public Transform Target;
  public Transform Destination;

  public int RotationSpeed;


  private Transform MyTransform;
  [Header("Animation")]
  public AnimationClip WalkAnim;
  public AnimationClip IdelAnim;
  public AnimationClip AttackAnim;
  public AnimationClip TakeHitAnim;
  public AnimationClip DeadAnim;

  public float MoveSpeed;
  public float MaxDistance;
  public float dist;
  public float AttackTimer;
  public float CoolDown;
  public int meeleDamage = 25;
  RaycastHit hit = new RaycastHit();
  public bool IsTakingHit = false;
  public bool IsAlive = true;

  public TroopType _troopType = TroopType.Meele;
  private State _state;
  void Awake()
  {

  }

  void Start()
  {
    _state = State.Init;
    StartCoroutine("FSM");
  }
  private IEnumerator FSM()
  {
    Debug.LogWarning("****FSM****");
    while (_state != State.Idle)
    {

      switch (_state)
      {
        case State.Init:
          Init();
          break;
        case State.SetUp:
          SetUp();
          break;
        case State.Move:
          Move();
          break;
        case State.Decide:
          Decide();
          break;
        case State.Die:
          Die();
          break;
        case State.TakeHit:
          TakeHit();
          break;
      }
      yield return null;
    }
  }
  private void Init()
  {
    if (Destination != null)
    {
      _state = State.SetUp;
    }
  }
  private void SetUp()
  {
   // GameObject go = GameObject.FindGameObjectWithTag("Wall");
    //Destination = go.transform;
    Target = Destination;
    MyTransform = transform;
    _state = State.Move;
  }
  private void Move()
  {
    if (Target != null)
    {
      if (Physics.Linecast(EnemyTarget.transform.position, attackrange.transform.position, out hit))
      {
        Debug.DrawLine(EnemyTarget.transform.position, attackrange.transform.position, Color.blue);
        if (hit.transform.CompareTag("Target")&&(hit.distance<MaxDistance))
        {
          Target = hit.transform;
         // Debug.Log("Current target:" + Target.name);
        }
      }

      //Debug.DrawLine(Target.position, MyTransform.position, Color.yellow);
      MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation, Quaternion.LookRotation(Target.position - MyTransform.position), RotationSpeed * Time.deltaTime);
      if (Vector3.Distance(Target.position, MyTransform.position) > MaxDistance)
      {
        MyTransform.position += MyTransform.forward * MoveSpeed * Time.deltaTime;
        GetComponent<Animation>().CrossFade(WalkAnim.name);
      }
      else
      {
        GetComponent<Animation>().Stop();
        _state = State.Decide;
      }

    }
  }
  private void Decide()
  {

    
    if (Physics.Linecast(EnemyTarget.transform.position, attackrange.transform.position, out hit))
    {
      try
      {
        Debug.DrawLine(EnemyTarget.transform.position, attackrange.transform.position, Color.blue);
        if (hit.transform.CompareTag("Target"))
        {
          Target = hit.transform;

          Debug.DrawLine(EnemyTarget.transform.position, attackrange.transform.position, Color.green);
          if (AttackTimer > 0)
            AttackTimer -= Time.deltaTime;
          if (AttackTimer < 0)
            AttackTimer = 0;
          if (AttackTimer == 0)
          {
            GetComponent<Animation>().CrossFade(AttackAnim.name, 1f);
            StartCoroutine("Attack");
            AttackTimer = CoolDown;
          }

        }
        else
        {
          //Debug.Log("Decide to WALL,Remove this block in future");
          Target = Destination;
          AttackTimer = 0;
          _state = State.Move;
        }
      }
      catch (UnityException e)
      {
        Debug.Log(e.Message);
      }

    }
  }
  private void Die()
  { }
  private void TakeHit()
  { }

  private IEnumerator Attack()
  {

    DefenceHealth eh = (DefenceHealth)hit.transform.GetComponent("DefenceHealth");
    if (eh != null)
    {

      if (eh)
      {

        yield return new WaitForSeconds(GetComponent<Animation>().GetClip(AttackAnim.name).length * 0.65f);
        Debug.Log("Attack");
        if (eh != null)
          eh.AdJustCurrentHealth(-meeleDamage);
      }

      if (eh != null)
      {
        if (eh.CurHealth <= 0)
        {
         
          Target = Destination;
          _state = State.Move;
        }
      }

    }

  }
}
