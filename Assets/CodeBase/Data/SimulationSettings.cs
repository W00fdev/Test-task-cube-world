using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class SimulationSettings
    {
        public float Distance;
        public float Speed;
        public float Time;

        public SimulationSettings(float distance, float speed, float time)
        {
            Distance = distance;
            Speed = speed;
            Time = time;
        }
    }
}
