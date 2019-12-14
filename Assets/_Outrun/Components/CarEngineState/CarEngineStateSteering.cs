using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Outrun.Components.CarEngineState
{
    public class CarEngineStateSteering : ICarEngineState
    {
        private static readonly float DistanceDelta = 0.1f;

        private CarEngine carEngine;
        private SteeringDirection direction;
        private float steeringVelocity;
        private float targetHorizontal;
        private CarEngineStateSteering queued;
        private bool stateQueued = false;

        public CarEngineStateSteering(SteeringDirection direction)
        {
            this.direction = direction;
        }

        public void Initialize(CarEngine engine)
        {
            carEngine = engine;
            steeringVelocity = carEngine.SteeringVelocity;
            float x = direction == SteeringDirection.RIGHT ? 1.0f : -1.0f;
            targetHorizontal = x * CarEngine.LaneWidth + carEngine.transform.position.x;
        }

        public void Update()
        {
            float targetDistance = targetHorizontal - carEngine.transform.position.x;
            // We don't check for passing over the delta. That is left for a designer and exceeds a proof of concept
            if (Mathf.Abs(targetDistance) < DistanceDelta)
                EndSteering();
            else
            {
                Vector3 newVelocity = new Vector3(targetDistance * steeringVelocity, 0.0f, carEngine.Velocity);
                carEngine.SetDirection(newVelocity.normalized);
                carEngine.SetVelocity(newVelocity.magnitude);
            }
        }

        private void EndSteering()
        {
            var modifiedPosition = carEngine.transform.position;
            modifiedPosition.x = targetHorizontal;
            carEngine.transform.position = modifiedPosition;
            if (stateQueued)
                carEngine.SwitchState(queued);
            else
                carEngine.SetRunning();
        }

        internal void Queue(CarEngineStateSteering carEngineStateSteering)
        {
            if(!stateQueued)
            {
                queued = carEngineStateSteering;
                stateQueued = true;
            }
        }

        public enum SteeringDirection { LEFT, RIGHT };
    }
}
