using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Outrun.Components.CarEngineState
{
    class CarEngineStateRunning : ICarEngineState
    {
        private CarEngine carEngine;

        public CarEngineStateRunning() { }

        public void Initialize(CarEngine engine)
        {
            carEngine = engine;
            carEngine.SetDirection(new Vector3(0.0f, 0.0f, 1.0f));
            carEngine.SetVelocity(carEngine.Velocity);
        }

        public void Update() { }
    }
}
