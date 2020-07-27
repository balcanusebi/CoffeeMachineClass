using System;

namespace CoffeeMachineSimulator.Data.Entities
{
    public class CoffeeDataEntity : BaseEntity
    {
        public string City { get; set; }
        public string SerialNumber { get; set; }
        public string SensorType { get; set; }
        public int SensorValue { get; set; }
        public DateTime RecordingTime { get; set; }
    }
}
