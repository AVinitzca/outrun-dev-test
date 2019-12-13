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

    void Start()
    {
        this.followed = Followed.transform;
        this.minimumDistance = MinimumDistance;
    }

    void Update()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.z = followed.transform.position.z - minimumDistance;
        transform.position = actualPosition;
    }
}
