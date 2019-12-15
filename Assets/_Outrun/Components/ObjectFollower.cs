using Assets._Outrun.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    public GameObject Followed;
    public float MinimumDistance;

    private Transform followed;
    private float minimumDistance;
    private bool firstTime;

    void Start()
    {
        this.followed = Followed.transform;
        this.minimumDistance = MinimumDistance;
        this.firstTime = true;
    }

    void Update()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.z = followed.transform.position.z - minimumDistance;
        transform.position = actualPosition;
    }
    public void Toggle()
    {
        enabled = !enabled;
    }
}
