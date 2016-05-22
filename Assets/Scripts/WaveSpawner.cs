using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

  public enum SpawnState { SPAWNING, WAITING, COUNTING };



  [System.Serializable]
  public class Wave
  {
    public string WaveName;
    public Transform Enemy;
    public int count;
    public float SpawnRate;

  }

  public Wave[] waves;
  public Transform[] SpawnPoints;
  private int nextWave = 0;
  public float TimeToWaitTillNextWave = 5f;
  public float TimeBetweenWaves = 5f;
  public float WaveCountDown;
  public SpawnState state = SpawnState.COUNTING;
  private float OriginalTimeToWait = 0;
  void Start()
  {
    WaveCountDown = TimeBetweenWaves;
    OriginalTimeToWait = TimeToWaitTillNextWave;
  }

  void Update()
  {
    TimeToWaitTillNextWave -= Time.deltaTime;
    if (state == SpawnState.WAITING)
    {
      if (TimeToWaitTillNextWave <= 0)
      {
        TimeToWaitTillNextWave = OriginalTimeToWait;
        WaveCompleted();
      }
      else
      {
        return;
      }
    }

    if (WaveCountDown <= 0)
    {
      if (state != SpawnState.SPAWNING)
      {
        StartCoroutine(SpawnWave(waves[nextWave]));
      }
    }
    else
    {
      WaveCountDown -= Time.deltaTime;
    }
  }
  void WaveCompleted()
  {
    Debug.Log("Wave Completed");
    state = SpawnState.COUNTING;
    WaveCountDown = TimeBetweenWaves;
    if (nextWave + 1 > waves.Length - 1)
    {
      nextWave = 0;
    }
    else
    {
      nextWave++;
    }
  }
  IEnumerator SpawnWave(Wave _wave)
  {
    Debug.Log("Spawning Enemy" + _wave.Enemy.name);
    state = SpawnState.SPAWNING;
    for (int i = 0; i <= _wave.count; i++)
    {
      SpawnEnemy(_wave.Enemy);
      yield return new WaitForSeconds(_wave.SpawnRate);
    }
    state = SpawnState.WAITING;
    yield break;
  }
  void SpawnEnemy(Transform Enemy)
  {
    int RanValue=Random.Range(0, SpawnPoints.Length);
    Transform sp = SpawnPoints[RanValue];
    Transform go=(Transform) Instantiate(Enemy, sp.position, sp.rotation);
    Debug.Log("Spwaned At :" + RanValue);
    go.GetComponent<AdvanceEnemyAI>().Destination = GameObject.FindGameObjectWithTag(("Wall" + (RanValue+1)).ToString()).transform;
    

  }
}
