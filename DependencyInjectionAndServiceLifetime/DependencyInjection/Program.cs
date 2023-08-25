using System;

// Define interfaces for dependencies
public interface IWheels
{
    void Rotate();
}

// Define different wheel implementations
public class MRFWheels : IWheels
{
    public void Rotate()
    {
        Console.WriteLine("MRF Wheels rotating...");
    }
}

public class YokohamaWheels : IWheels
{
    public void Rotate()
    {
        Console.WriteLine("Yokohama Wheels rotating...");
    }
}

// Car class with dependency injection
public class Car
{
    private readonly IWheels _wheels;

    public Car(IWheels wheels)
    {
        _wheels = wheels;
    }

    public void Drive()
    {
        Console.WriteLine("Car is driving...");
        _wheels.Rotate();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a car with MRFWheels
        IWheels mrfWheels = new MRFWheels();
        Car carWithMRF = new Car(mrfWheels);
        carWithMRF.Drive();

        // Create a car with YokohamaWheels
        IWheels yokohamaWheels = new YokohamaWheels();
        Car carWithYokohama = new Car(yokohamaWheels);
        carWithYokohama.Drive();
    }
}