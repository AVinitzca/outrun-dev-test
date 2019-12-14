using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Outrun.Components.CarEngineState
{
    interface ICarEngineState
    {
        void Initialize(CarEngine engine);
        void Update();
    }
}
