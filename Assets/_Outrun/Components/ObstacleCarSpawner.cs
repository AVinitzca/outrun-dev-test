using Assets._Outrun.Components;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCarSpawner : MonoBehaviour, IResetable
{
    [Range(0.0f, 1.0f)]
    public float ChanceToSpawn;
    public List<GameObject> ObstacleCars;
    public GameObject PathManager;
    public GameObject CarCharacter;
    public float SpawnTimerInterval;
    public bool Difficulty;

    private float MaxChanceToSpawn = 0.15f;
    private float spawnTimer;
    private float InitialChanceToSpawn;

    void Start()
    {
        InitialChanceToSpawn = ChanceToSpawn;
    }

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

        if (Difficulty && ChanceToSpawn < MaxChanceToSpawn)
            ChanceToSpawn += Time.deltaTime * 0.001f;
    }

    private void Spawn(Transform parent)
    {
        int index = Random.Range(0, ObstacleCars.Count);
        GameObject car = Instantiate(ObstacleCars[index], LastPath.position + Vector3.up + Vector3.right * RandomLaneHorizontalPosition(), Quaternion.identity);
        car.GetComponent<CarAI>().CarCharacter = CarCharacter;
        car.transform.parent = parent;
    }


    private float RandomLaneHorizontalPosition()
    {
        Transform lastPath = LastPath;
        Transform lanes = ChildWithTag(lastPath, "Lane");

        int index = Random.Range(0, lanes.childCount);
        return lanes.GetChild(index).position.x;
    }

    private Transform LastPath
    {
        get
        {
            int count = PathManager.transform.childCount;
            return count == 0 ? PathManager.transform : PathManager.transform.GetChild(count - 1);
        }
    }

    private Transform ChildWithTag(Transform transform, string tag)
    {
        for (int index = 0; index < transform.childCount; index++)
            if (transform.GetChild(index).CompareTag(tag))
                return transform.GetChild(index);
        return null;
    }

    public void Reset()
    {
        enabled = !enabled;
        ChanceToSpawn = InitialChanceToSpawn;
        // Clear obstacles
        for (int index = 0; index < transform.childCount; index++)
            Destroy(transform.GetChild(index).gameObject);
    }
}
