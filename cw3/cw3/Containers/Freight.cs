namespace cw3.Containers;

public class Freight
{
    private List<Container> _containers;
    private double _maxWeight;
    private double _currentWeight = 0;
    private double _maxVelocity;
    private double _maxContainerNumber;

    public Freight(double maxWeight, double currentWeight, double maxVelocity, double maxContainerNumber)
    {
        _maxWeight = maxWeight;
        _currentWeight = currentWeight;
        _maxVelocity = maxVelocity;
        _maxContainerNumber = maxContainerNumber;
        _containers = new List<Container>();
    }

    public void LoadContainerOnShip(Container container)
    {
        var size = _containers.Count;
        if (_currentWeight + container.CurrentLoad <= _maxWeight && size <= _maxContainerNumber)
        {
            _containers.Add(container);
            _currentWeight += container.CurrentLoad;
        }
        else
        {
            throw new OverfillException("Load is too large: " + container.ToString());
        }
    }

    public void LoadContainersOnShip(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainerOnShip(container);
        }
    }

    public void RemoveContainerFromShip(Container container)
    {
        _containers.Remove(container);
        _currentWeight -= container.CurrentLoad;
    }

    public void ReplaceContainerOnShip(Container oldContainer, Container newContainer)
    {
        RemoveContainerFromShip(oldContainer);
        LoadContainerOnShip(newContainer);
    }

    public void TransferContainer(Container container, Freight otherFreight)
    {
        RemoveContainerFromShip(container);
        otherFreight.LoadContainerOnShip(container);
    }

    public void PrintContainerInfo(Container container)
    {
        Console.WriteLine(container);
    }

    public void PrintShipInfo()
    {
        Console.WriteLine($"Max weight: {_maxWeight}, Current weight: {_currentWeight}, Max container number: {_maxContainerNumber}, Container number: {_containers.Count}, Max velocity: {_maxVelocity}");
    }
}