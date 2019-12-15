using Assets._Outrun.Components;
using Assets._Outrun.Components.CarEngineState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour, IResetable
{
    public static readonly int LaneCount = 5;
    public static readonly float LaneWidth = 4.0f;

    public float Velocity;
    public float SteeringVelocity;
    public float startingPoint;

    private float velocity;
    private Vector3 direction;
    private ICarEngineState state;
    private bool firstTime;

    void Start()
    {
        this.direction = new Vector3(0.0f, 0.0f, 1.0f);
        this.velocity = Velocity;
        startingPoint = 0.0f;
        firstTime = true;
        SetRunning();
    }

    void Update()
    {
        state.Update();
        transform.position += direction * Velocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0.0f);
    }

    private bool DirectionValid(CarEngineStateSteering.SteeringDirection direction)
    {
        int lane = GetLaneNumber();
        return (direction == CarEngineStateSteering.SteeringDirection.LEFT && lane != 0) ||
            (direction == CarEngineStateSteering.SteeringDirection.RIGHT && lane != 4);
    }
        
    internal void SwitchState(ICarEngineState state)
    {
        this.state = state;
        this.state.Initialize(this);
    }

    public void Configure(float runningVelocity, float steeringVelocity)
    {
        this.Velocity = runningVelocity;
        this.SteeringVelocity = steeringVelocity;
        SetRunning();
    }
    public int GetLaneNumber()
    {
        return Convert.ToInt32(transform.position.x / LaneWidth) + (LaneCount / 2);
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public float ForwardVelocity
    {
        get
        {
            return velocity * direction.z;
        }
    }

    public int DistanceTraveled { get => Convert.ToInt32(transform.position.z - startingPoint); }

    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }
    
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetRunning()
    {
        SwitchState(new CarEngineStateRunning());
    }

    public void SetSteering(CarEngineStateSteering.SteeringDirection direction)
    {
        if(DirectionValid(direction))
            if (!(state is CarEngineStateSteering))
                SwitchState(new CarEngineStateSteering(direction));
            else
                ((CarEngineStateSteering)state).Queue(new CarEngineStateSteering(direction));
    }

    public void Reset()
    {
        if (!firstTime)
        {
            enabled = !enabled;
        }
        else
            firstTime = false;
        startingPoint = transform.position.z;
    }

}
