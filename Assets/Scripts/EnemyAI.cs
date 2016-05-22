using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	public Transform EnemyTarget;
	public Transform attackrange;
	public Transform Target;
	public Transform Destination;
	public float MoveSpeed;
	public int RotationSpeed;
	public float MaxDistance;
	private Transform MyTransform;
	public AnimationClip WalkAnim;
	public AnimationClip IdelAnim;
	public AnimationClip AttackAnim;
	public AnimationClip TakeHitAnim;
	public AnimationClip DeadAnim;
	public float dist;
	public float AttackTimer;
	public float CoolDown;
	public int meeleDamage = 25;
	RaycastHit hit = new RaycastHit ();
	public bool IsTakingHit = false;
	public bool IsAlive = true;
	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("Wall");
		Destination = go.transform;
		Target = Destination;
		GetComponent<Animation> () [AttackAnim.name].wrapMode = WrapMode.Once;	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Target != null) 
    {
			Debug.DrawLine (Target.position, MyTransform.position, Color.red);
			MyTransform.rotation = Quaternion.Slerp (MyTransform.rotation, Quaternion.LookRotation (Target.position - MyTransform.position), RotationSpeed * Time.deltaTime);
			if (Vector3.Distance (Target.position, MyTransform.position) > MaxDistance)
      {
				MyTransform.position += MyTransform.forward * MoveSpeed * Time.deltaTime;
				GetComponent<Animation> ().CrossFade (WalkAnim.name);
			} 
		}
		Debug.DrawLine (EnemyTarget.transform.position, attackrange.transform.position, Color.blue);
		if (Physics.Linecast (EnemyTarget.transform.position, attackrange.transform.position, out hit)) 
    {
			try 
      {
				if (hit.transform.CompareTag ("Target")) 
        {
					Target = hit.transform;
					if (!IsTakingHit) 
          {
						Debug.DrawLine (EnemyTarget.transform.position, attackrange.transform.position, Color.green);
						if (AttackTimer > 0)
							AttackTimer -= Time.deltaTime;
						if (AttackTimer < 0)
							AttackTimer = 0;
						if (AttackTimer == 0) {					
							GetComponent<Animation> ().CrossFade (AttackAnim.name, 1f);
							Attack ();
							AttackTimer = CoolDown;
						}
					}
				}
        else
        {
					Target = Destination;
					AttackTimer = 0;
				}
			} catch (UnityException e) {
				Debug.Log (e.Message);
			}

		}
	}
	void TakenHit ()
	{
		StartCoroutine (TakeHit ());
	}
	IEnumerator TakeHit ()
	{
		IsTakingHit = true;
		//GetComponent<Animation> ().CrossFade (TakeHitAnim.name, 1f);
		yield return new WaitForSeconds (GetComponent<Animation> ().GetClip (TakeHitAnim.name).length);
		IsTakingHit = false;
	}
	void Awake ()
	{
		MyTransform = transform;
	}
	private void  Attack ()
	{
		DefenceHealth eh = (DefenceHealth)hit.transform.GetComponent ("DefenceHealth");
		if (eh)
    {
			eh.AdJustCurrentHealth (-meeleDamage);
		}
		if (eh.CurHealth <= 0)
    {
			Target = Destination;
		}
	}

}
