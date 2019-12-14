using Assets._Outrun.Components.CarEngineState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
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
        /// TODO: Steering  
        if(steeringTimer >= nextSteeringTimer)
        {
            GenerateNextSteeringTimer();
            engine.SetSteering(RandomDirection());
            steeringTimer = 0.0f;
        }
        steeringTimer += Time.deltaTime;
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
        return Random.value > 0.5f ? CarEngineStateSteering.SteeringDirection.LEFT : CarEngineStateSteering.SteeringDirection.RIGHT;
    }

    private float Velocity { get => Random.Range(MinVelocity, MaxVelocity); }

    private float SteeringVelocity { get => Random.Range(MinSteeringVelocity, MaxSteeringVelocity); }

}
