
namespace InMemoryObjectCache
{
    public class Car
    {
        public Car()
        {
            
        }

        public Car(string colour, int doors = 2, int cylinders = 4, bool radio = true)
        {
            Colour = colour;
            Doors = doors;
            Cylinders = cylinders;
            Radio = radio;
        }

        public string Colour { get; set; }
        public int Doors { get; set; }
        public int Cylinders { get; set; }
        public bool Radio { get; set; }
    }
}
