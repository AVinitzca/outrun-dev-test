using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSpawner : MonoBehaviour
{
    private static int TotalSpawned = 0;

    public GameObject LeftLamp;
    public GameObject RightLamp;

    void Start()
    {
        int mod = TotalSpawned % 8;
        if (mod == 0)
            SpawnLamp(RightLamp);
        else if (mod == 7)
            SpawnLamp(LeftLamp);     
        TotalSpawned++;
    }

    private void SpawnLamp(GameObject lamp)
    {
        GameObject path = Instantiate(lamp, transform.position, Quaternion.identity);
        path.transform.parent = transform;
    }



}
