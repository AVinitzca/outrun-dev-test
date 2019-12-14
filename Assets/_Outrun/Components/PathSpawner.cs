using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    private static readonly float PathWidth = 10.0f;

    public GameObject PathPrefab;
    public int MaxChildCount;
    public float PathDistance;
    public CarEngine CharacterCarEngine;

    private GameObject pathPrefab;
    private int maxChildCount;
    private float pathDistance;
    private int childCount;
    private int totalChildsSpawned;
    private CarEngine characterCarEngine;

    void Start()
    {
        this.pathPrefab = PathPrefab;
        this.maxChildCount = MaxChildCount;
        this.pathDistance = PathDistance;
        // Using this to prevent calling GetChildCount
        // We lose safety, because calling GetChild without actually
        // controlling the childCount is unsafe. But we win a bit of performance
        this.childCount = 0;
        this.totalChildsSpawned = 0;
        this.characterCarEngine = CharacterCarEngine;
    }

    void Update()
    {
        SpawnPaths();
        KillPaths();
    }

    private void SpawnPaths()
    {
        while (childCount < maxChildCount)
            InstantiatePath();
    }

    private void InstantiatePath()
    {
        // Weird hack to use world position, after Instantiate setting the parent won't work
        GameObject path = Instantiate(pathPrefab, transform.position + new Vector3(0.0f, 0.0f, pathDistance * totalChildsSpawned), Quaternion.identity);
        path.transform.parent = transform;
        childCount++;
        totalChildsSpawned++;
    }

    private void KillPaths()
    {
        if(transform.childCount > 0)
        {
            Transform firstChild = transform.GetChild(0);
            if((firstChild.position.z + PathWidth * 2.0f) < characterCarEngine.transform.position.z)
            {
                Destroy(transform.GetChild(0).gameObject);
                childCount--;
            }
        }
    }
}
