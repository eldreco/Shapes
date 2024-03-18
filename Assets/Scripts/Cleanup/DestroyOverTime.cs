using UnityEngine;
using UnityEngine.Assertions;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start() {
        Assert.IsTrue(lifeTime > 0, "Life time must be positive. Object: " + gameObject.name);
    }
    
    private void Update() {
        lifeTime -= Time.deltaTime;

        if(lifeTime < 0){
            Destroy(gameObject);
        }
    }
}
