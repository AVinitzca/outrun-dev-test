using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public float Velocity;

    private float velocity;
    private Vector3 direction;

    void Start()
    {
        this.velocity = Velocity;
        this.direction = new Vector3(0.0f, 0.0f, 1.0f);
    }

    void Update()
    {
        transform.position += direction * Velocity * Time.deltaTime;
    }
}
