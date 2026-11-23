namespace Odev1
{
    class Engine
    {
        public string Model { get; set; }
        public int Horsepower { get; set; }
        public bool IsRunning { get; private set; }

        public Engine(string model, int horsepower)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Engine model can't be empty.", nameof(model));
            if (horsepower <= 0)
                throw new ArgumentOutOfRangeException(nameof(horsepower), "Horsepower must be greater than zero.");

            Model = model;
            Horsepower = horsepower;
        }

        public void Start() { IsRunning = true; Console.WriteLine($"The {Model} engine ({Horsepower} HP) has started."); }
        public void Stop() { IsRunning = false; Console.WriteLine($"The {Model} engine has stopped."); }
    }

    class Car
    {
        public string Brand { get; set; }
        public Engine Engine { get; private set; }

        public Car(string brand, Engine engine)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand can't be empty.", nameof(brand));

            Brand = brand;
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }

        public void Start() { Console.WriteLine($"Starting the {Brand}..."); Engine.Start(); }
        public void Stop() { Console.WriteLine($"Stopping the {Brand}..."); Engine.Stop(); }

        public void ReplaceEngine(Engine newEngine)
        {
            if (Engine.IsRunning) { Console.WriteLine("Stop the car before replacing the engine!"); return; }
            Engine = newEngine ?? throw new ArgumentNullException(nameof(newEngine));
            Console.WriteLine($"{Brand}: engine replaced with {newEngine.Model}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car("Toyota", new Engine("1.8 Hybrid", 140));
            car1.Start();
            car1.ReplaceEngine(new Engine("2.0 VVT-i", 170));
            car1.Stop();
            car1.ReplaceEngine(new Engine("2.0 VVT-i", 170));
            car1.Start();

            try { new Engine("Broken", -5); }
            catch (ArgumentOutOfRangeException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
