using Assets._Outrun.Components.CarEngineState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    private static readonly float DestroyDistance = 20.0f;

    public float MinVelocity;
    public float MaxVelocity;
    public float MinSteeringVelocity;
    public float MaxSteeringVelocity;
    public GameObject CarCharacter;

    private CarEngine engine;

    float nextSteeringTimer = 0.0f;
    float steeringTimer = 0.0f;

    void Start()
    {
        ConfigureEngine();
        GenerateNextSteeringTimer();
    }

    void Update()
    {
        if(steeringTimer >= nextSteeringTimer)
        {
            GenerateNextSteeringTimer();
            engine.SetSteering(RandomDirection());
            steeringTimer = 0.0f;
        }
        steeringTimer += Time.deltaTime;
        if ((transform.position.z + DestroyDistance) < CarCharacter.transform.position.z)
            Destroy(gameObject);
    }

    private void ConfigureEngine()
    {
        engine = GetComponent<CarEngine>();
        engine.Configure(Velocity, SteeringVelocity);
    }

    private void GenerateNextSteeringTimer()
    {
        nextSteeringTimer = Random.Range(0.0f, 5.0f);
    }

    private CarEngineStateSteering.SteeringDirection RandomDirection()
    {
        int lane = engine.GetLaneNumber();
        if (lane == PathSpawner.LaneCount - 1)
            return CarEngineStateSteering.SteeringDirection.LEFT;
        else if (lane == 0)
            return CarEngineStateSteering.SteeringDirection.RIGHT;
        else
            return Random.value > 0.5f ? CarEngineStateSteering.SteeringDirection.LEFT : CarEngineStateSteering.SteeringDirection.RIGHT;
    }

    private float Velocity { get => Random.Range(MinVelocity, MaxVelocity); }

    private float SteeringVelocity { get => Random.Range(MinSteeringVelocity, MaxSteeringVelocity); }

}
