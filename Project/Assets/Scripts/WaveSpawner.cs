using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public List<Transform> EnemyPrefabs;

    public Transform Spawnpoint;
    public float timeinwaves = 5f;
    private float countdown = 10f;
    public Text currentWaveText;
    public Text waveCountdowntext;
    private int waveNumber = 0;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (countdown <= 0f){
            StartCoroutine(SpawnWave());
            countdown = timeinwaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        currentWaveText.text = "Wave:" + waveNumber;
        waveCountdowntext.text = "Next Wave in \n" + string.Format("{0:00}",countdown) ;
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        PlayerStats.Rounds++;
        if (waveNumber % 10 == 0)
        {
            for (int i = 0; i < waveNumber/10; i++)
            {
                SpawnEnemy("Boss");
                yield return new WaitForSeconds(0.5f);
            }

        }
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy("Normal");
            yield return new WaitForSeconds(0.5f);
        }

    }

    void SpawnEnemy(string isBoss)
    {
        if (isBoss == "Normal")
        { 
        Instantiate(EnemyPrefabs[Random.Range(0,2)], Spawnpoint.position, Spawnpoint.rotation);
        }
        if (isBoss == "Boss")
        {
            Instantiate(EnemyPrefabs[2], Spawnpoint.position, Spawnpoint.rotation);
        }

    }
}
