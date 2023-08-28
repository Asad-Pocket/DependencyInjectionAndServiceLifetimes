using System;

// Dependency classes
public interface WaterProtection
{
    void Launch();
}
public class WaterResistant : WaterProtection
{
    public void Launch()
    {
        Console.WriteLine("Apple Watch launch as Water Resistant version");
    }
}
public class WaterProof : WaterProtection
{
    public void Launch()
    {
        Console.WriteLine("Apple Watch launch as Water Proof version");
    }
}

class AppleWatch
{
    private WaterProtection _waterProtection;

    public AppleWatch(WaterProtection waterProtection)
    {
        _waterProtection = waterProtection;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("AppleWatch Info:");
        _waterProtection.Launch();
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Original objects
        WaterProtection originalWaterProtection = new WaterResistant();

        // Creating an AppleWatch object with original dependencies
        AppleWatch originalAppleWatch = new AppleWatch(originalWaterProtection);
        originalAppleWatch.DisplayInfo();

        // New object
        WaterProtection _NewWaterProtection = new WaterProof();

        // Creating an AppleWatch object with updated water protection dependency
        AppleWatch updatedAppleWatch = new AppleWatch(_NewWaterProtection);
        updatedAppleWatch.DisplayInfo();
    }
}
