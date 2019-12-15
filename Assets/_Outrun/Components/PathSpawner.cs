using Assets._Outrun.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour, IResetable
{
    public static readonly int LaneCount = 5;
    private static readonly float PathWidth = 10.0f;

    public GameObject PathPrefab;
    public int MaxChildCount;
    public float PathDistance;
    public CarEngine CharacterCarEngine;

    private int childCount;
    private int totalChildsSpawned;
    private bool goldSpawningActive;

    void Start()
    {
        // Using this to prevent calling GetChildCount
        // We lose safety, because calling GetChild without actually
        // controlling the childCount is unsafe. But we win a bit of performance
        this.childCount = 0;
        this.totalChildsSpawned = 0;
        this.goldSpawningActive = false;
    }


    void Update()
    {
        SpawnPaths();
        KillPaths();
    }

    private void SpawnPaths()
    {
        while (childCount < MaxChildCount)
            InstantiatePath();
    }

    private void InstantiatePath()
    {
        // Weird hack to use world position, after Instantiate setting the parent won't work
        GameObject path = Instantiate(PathPrefab, NextPathPosition(), Quaternion.identity);
        path.transform.parent = transform;
        path.transform.GetComponentInChildren<GoldSpawner>().enabled = goldSpawningActive;
        childCount++;
        totalChildsSpawned++;
    }

    private Vector3 NextPathPosition()
    {
        return transform.position + Vector3.forward * (totalChildsSpawned * PathDistance);
    }

    private void KillPaths()
    {
        if(transform.childCount > 0)
        {
            Transform firstChild = transform.GetChild(0);
            if((firstChild.position.z + PathWidth * 2.0f) < CharacterCarEngine.transform.position.z)
            {
                Destroy(transform.GetChild(0).gameObject);
                childCount--;
            }
        }
    }

    public void Reset()
    {
        goldSpawningActive = !goldSpawningActive;
    }
}
