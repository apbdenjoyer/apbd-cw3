namespace cw3.Containers;

abstract class Container : IContainer {
    public double LoadMass { get; set; }
    protected string Serial { get; set; }
    protected char? ContType { get; set; }
    protected double Height { get; set; }
    protected double ContainerWeight { get; set; }
    protected double Depth { get; set; }
    public double MaxLoad { get; set; }

    public Container(double loadMass, double height, double containerWeight, double depth,
        double maxLoad) {
        LoadMass = loadMass;
        Height = height;
        ContainerWeight = containerWeight;
        Depth = depth;
        Serial = CreateSerial();
        MaxLoad = maxLoad;
    }

    public void Empty() {
        LoadMass = 0;
    }

    public virtual void Load(double loadMass) {
        { if (loadMass > MaxLoad)
          { throw new OverfillException("Load is too large: " + loadMass.ToString()); }

          LoadMass = loadMass; }
    }

    string CreateSerial() {
        return "KON-" + ContType + "-" + IContainer._id++;
    }
}

class LiquidContainer : Container, IHazardNotifier {
    public LiquidContainer(double loadMass, double height, double containerWeight, double depth, double maxLoad) : base(
        loadMass, height, containerWeight, depth, maxLoad) {
        base.ContType = 'L';
    }

    public void Load(double loadMass, bool isHazardous) {
        if (isHazardous)
        { if (loadMass > MaxLoad / 2)
          { base.Load(loadMass / 2); } }

        base.Load(loadMass);
    }
}

internal interface IHazardNotifier {
    void Notify(string message, string serial) {
        Console.WriteLine(message + ": " + serial);
    }
}

internal class OverfillException : Exception {
    public OverfillException(string message) : base(message) { }
}