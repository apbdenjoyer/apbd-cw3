namespace cw3.Containers
{
    public abstract class Container : IContainer
    {
        public double CurrentLoad { get; set; }
        protected string Serial { get; set; }
        protected char ContType { get; } // Remove Nullable as ContType cannot be null
        protected double Height { get; set; }
        protected double ContainerWeight { get; set; }
        protected double Depth { get; set; }
        public double MaxLoad { get; set; }

        protected Container(double currentLoad, char contType, double height, double containerWeight, double depth,
            double maxLoad)
        {
            CurrentLoad = currentLoad;
            ContType = contType;
            Height = height;
            ContainerWeight = containerWeight;
            Depth = depth;
            Serial = CreateSerial();
            MaxLoad = maxLoad;
        }

        public virtual void Empty()
        {
            CurrentLoad = 0;
        }

        public virtual void Load(double newLoad)
        {
            if (newLoad > MaxLoad)
            {
                throw new OverfillException("Load is too large: " + newLoad);
            }

            CurrentLoad = newLoad;
        }

        string CreateSerial()
        {
            return "KON-" + ContType + "-" + IContainer._id++;
        }

        public override string ToString()
        {
            return $"Serial: {Serial}, Type: {ContType}, Current Load: {CurrentLoad}, Max Load: {MaxLoad}, Height: {Height}, Container Weight: {ContainerWeight}, Depth: {Depth}";
        }
    }

    class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(double currentLoad, double height, double containerWeight, double depth,
            double maxLoad) : base(currentLoad, 'L', height, containerWeight, depth, maxLoad)
        {
        }

        public void Load(double newLoad, bool isHazardous)
        {
            // Load implementation for LiquidContainer
        }

        public void HazardNotify(string message, string serial)
        {
            // Implement notification logic here
            Console.WriteLine(message, $"Container serial: {Serial}");
        }
    }

    class GasContainer : Container, IHazardNotifier
    {
        protected double Pressure { get; set; }

        public GasContainer(double currentLoad, double height, double containerWeight, double depth,
            double maxLoad, double pressure) : base(currentLoad, 'G', height, containerWeight, depth, maxLoad)
        {
            Pressure = pressure;
        }

        public override void Empty()
        {
            // Empty implementation for GasContainer
        }

        public void HazardNotify(string message, string serial)
        {
            // Implement notification logic here
            Console.WriteLine(message, $"Container serial: {Serial}");
        }

        public override string ToString()
        {
            return
                $"Serial: {Serial}, Type: {ContType}, Current Load: {CurrentLoad}, Max Load: {MaxLoad}, Height: {Height}, Container Weight: {ContainerWeight}, Depth: {Depth}, Pressure: {Pressure}";
        }
    }

    class FreezerContainer : Container
    {
        protected string ProdType;
        protected double Temperature;
        protected double RequiredTemperature;

        public FreezerContainer(double currentLoad, double height, double containerWeight, double depth,
            double maxLoad, string prodType, double temperature, double requiredTemperature) : base(currentLoad, 'C',
            height, containerWeight, depth, maxLoad)
        {
            ProdType = prodType;
            if (temperature > requiredTemperature)
            {
                throw new Exception(
                    $"Temperature too low. Current: {temperature}, required: {requiredTemperature}");
            }

            Temperature = temperature;
            RequiredTemperature = requiredTemperature;
        }

        public override string ToString()
        {
            return
                $"Serial: {Serial}, Type: {ContType}, Current Load: {CurrentLoad}, Max Load: {MaxLoad}, Height: {Height}, Container Weight: {ContainerWeight}, Depth: {Depth}, Prod Type: {ProdType}, Suggested Temperature: {RequiredTemperature}, Temperature: {Temperature}";
        }
    }

    internal interface IHazardNotifier
    {
        void HazardNotify(string message, string serial);
    }

    internal class OverfillException(string message) : Exception(message);
}
