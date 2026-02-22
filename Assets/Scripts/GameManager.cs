using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int numberOfEnemies=0;
    public int wave=0;
    public bool startNewWave=false;
    public bool waveIncremented = false;
    void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    
}
