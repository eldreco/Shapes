using UnityEngine;

public class DoubleObstacle : Obstacle, ISpawnable
{
    [SerializeField] private float verticalOffset;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {        
        Vector3 posT1 = new(spawnObjPos.x , spawnObjPos.y + verticalOffset, -0.75f);
        Vector3 posT2 = new(spawnObjPos.x , spawnObjPos.y + verticalOffset, 0.75f);
        int randIndex = Random.Range(0 , 3);

        if(randIndex <= 1){
            Instantiate(this, posT1,spawnObjRot);
            Instantiate(this, posT2,spawnObjRot);
        } else{
            int rndObsPos = Random.Range(0 , 2);
            if(rndObsPos == 0)
                Instantiate(this, posT1,spawnObjRot);
            else
                Instantiate(this, posT2,spawnObjRot);
        }
    }
}
