using System.Collections;
using UnityEngine;

public class SpawnTutorial : MonoBehaviour
{
    public static SpawnTutorial Instance;

    public bool IsActive {get; set;}
    private bool spawnedCheckpoint;
    private Transform tf;

    [SerializeField] private GameObject firstCheckpoint;

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void OnEnable() {
        SpawnObstacles.OnSpawnedObstacle += CheckShouldChangeStage;
    }

    private void OnDisable() {
        SpawnObstacles.OnSpawnedObstacle -= CheckShouldChangeStage;
    }

    private void Start() {
        tf = gameObject.transform; 
        IsActive = false;
        spawnedCheckpoint = false;
    }

    private void CheckShouldChangeStage(){
        if(SpawnObstacles.Instance.obstaclesSpawnedCount == 10 && !spawnedCheckpoint){
            StartCoroutine(SpawnStage());
            SpawnObstacles.Instance.OnDisable();
            spawnedCheckpoint = true;
        }
    }

    public IEnumerator SpawnStage(){
        yield return new WaitForSeconds(1);
        Instantiate(firstCheckpoint, tf.position, tf.rotation);
        IsActive = false;
    }

    public void ResetCheckpoint(){
        spawnedCheckpoint = false;
    }
}
