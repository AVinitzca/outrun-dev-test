using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCarSpawner : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float ChanceToSpawn;
    public List<GameObject> ObstacleCars;
    public GameObject PathManager;
    public float SpawnTimerInterval;

    private float spawnTimer;

    void Update()
    {
        if(spawnTimer > SpawnTimerInterval)
        {
            if (Random.value < ChanceToSpawn)
            {
                Spawn(transform);
            }
            spawnTimer = 0.0f;
        }
        spawnTimer += Time.deltaTime;
    }

    private void Spawn(Transform parent)
    {
        int index = Random.Range(0, ObstacleCars.Count);
        GameObject car = Instantiate(ObstacleCars[index], LastPath.position + Vector3.up * 1.5f + Vector3.right * RandomLaneHorizontalPosition(), Quaternion.identity);
        car.transform.parent = parent;
    }

    private float RandomLaneHorizontalPosition()
    {
        Transform lastPath = LastPath;
        int index = Random.Range(0, lastPath.childCount);
        return Mathf.FloorToInt(lastPath.GetChild(index).position.x);
    }

    private Transform LastPath
    {
        get
        {
            int count = PathManager.transform.childCount;
            return count == 0 ? PathManager.transform : PathManager.transform.GetChild(count - 1);
        }
    }
}
