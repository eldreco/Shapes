using System;
using TimerUtils;
using static Constants.Constants;
using UnityEngine;
using UnityEngine.Assertions;
using static Utils.PlayerUtils;

public class SpawnObstacles : MonoBehaviour
{
    public static SpawnObstacles Instance;

    protected static Transform tf;

    [SerializeField] private GameObject[] obstacles;
    private int lastSpawnedIndex = -1;

    private int obstacleIndex;

    [SerializeField] private bool onlySpawnDownObs;
    [SerializeField] private bool onlySpawnMidObs;
    [SerializeField] private bool onlySpawnUpObs;

    public int ObstaclesSpawnedCount {get; set;}

    private Timer spawnTimer;

    public static event Action OnSpawnedObstacle;


    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        tf = gameObject.transform; 
        Assert.IsFalse(obstacles.Length == 0, "The Spawn object has no obstacles assigned");
        Spawn();
    }

    public void OnEnable() {
        spawnTimer = new(2f);
        spawnTimer.OnTimerFinished += Spawn;
    }

    public void OnDisable() {
        spawnTimer.OnTimerFinished -= Spawn;
    }
    
    private void Update() { 
        spawnTimer.ExecuteTimer();
    }

    protected void Spawn(){
        if(GameManager.Instance.IsLevelEnded) return;

        obstacleIndex = UnityEngine.Random.Range(0 , obstacles.Length);
            
        if((obstacles.Length == 1 || lastSpawnedIndex != obstacleIndex)){
            ISpawnable spawnable = obstacles[obstacleIndex].GetComponent<ISpawnable>();
            spawnable.Spawn(tf.position, tf.rotation);
            ObstaclesSpawnedCount++;
            lastSpawnedIndex = obstacleIndex;
            HandleTimer();
            OnSpawnedObstacle?.Invoke();
        }else{
            Spawn(); //Repeat if its the same index
        }
    }

    private void HandleTimer(){
        if (spawnTimer.TimerMaxValue >= MIN_SPAWN_INTERVAL)
            spawnTimer.TimerMaxValue *= 1 / GameManager.Instance.Acceleration;
        spawnTimer.ResetTimer();
    }

    public void SetSpawnType(bool spawnDown, bool spawnMid, bool spawnUp){ //For other classes, i.e tutorial
        onlySpawnDownObs = spawnDown;
        onlySpawnMidObs = spawnMid;
        onlySpawnUpObs = spawnUp;
    }

    public void ResetCount(){
        ObstaclesSpawnedCount = 0;
    }
}
