using UnityEngine;

public class FatObstacle : Obstacle, ISpawnable
{
    private readonly float offsetZ = 0.25f;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 posL1 = new(spawnObjPos.x , spawnObjPos.y , -offsetZ);
        Vector3 posL2 = new(spawnObjPos.x , spawnObjPos.y , offsetZ);
        int rndLPos = Random.Range(0 , 2);

        if(rndLPos == 0)
            Instantiate(this, posL1, spawnObjRot);
        else
            Instantiate(this, posL2, spawnObjRot);
    }
}
