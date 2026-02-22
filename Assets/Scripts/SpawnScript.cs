using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private int numberToSpawn;
    
    private bool spawnedTheWave = false;
    public bool EnemySpawner;
    private float radius;
    public float height;
    public TMP_Text WaveCounter;
    void Start()
    {
        radius = transform.localScale.x * 0.5f;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.startNewWave && !spawnedTheWave)
        {
            if(!GameManager.Instance.waveIncremented && EnemySpawner)
            {
            GameManager.Instance.wave++;
            GameManager.Instance.waveIncremented=true;
            WaveCounter.text="Wave: "+GameManager.Instance.wave;
            }
            if(EnemySpawner)
            {
            numberToSpawn=Random.Range(GameManager.Instance.wave/2, GameManager.Instance.wave*2);
            if(numberToSpawn==0)
                {
                    numberToSpawn=1;
                }
            }
            else
            {
            numberToSpawn = GameManager.Instance.wave-1;  
            }
            
            SpawnEnemies();
            spawnedTheWave=true;
        }

        if(GameManager.Instance.numberOfEnemies== 0 && EnemySpawner )
        {
           spawnedTheWave=false;
           GameManager.Instance.startNewWave=true;
           GameManager.Instance.waveIncremented=false;
        }
        if(GameManager.Instance.numberOfEnemies== 0 && !EnemySpawner )
        {
           spawnedTheWave=false;
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 1; i<=numberToSpawn;i++)
        {
            Vector3 position = GetRandomPointOnTop();

            Instantiate(enemyToSpawn, position, Quaternion.identity);
            if(EnemySpawner)
            {
            GameManager.Instance.numberOfEnemies++;
            }
        }
    }

    Vector3 GetRandomPointOnTop()
    {
        float angle = Random.Range(0, Mathf.PI * 2f);
        float r = Random.Range(0f, radius);

        Vector3 localPosition = new Vector3(Mathf.Cos(angle)*r,height,Mathf.Sin(angle)*r);
        
        return transform.position + localPosition;
    }
}
