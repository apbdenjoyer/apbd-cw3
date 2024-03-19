
using cw3.Containers;

var liquidContainer = new LiquidContainer(100, 5, 10, 15, 200);
var gasContainer = new GasContainer(200, 6, 12, 18, 300, 20);
var freezerContainer = new FreezerContainer(150, 7, 14, 21, 250, "Food", -20, -15);

var freight = new Freight(1000, 0, 30, 5);

freight.LoadContainerOnShip(liquidContainer);
freight.LoadContainerOnShip(gasContainer);
freight.LoadContainerOnShip(freezerContainer);


freight.PrintShipInfo();

freight.PrintContainerInfo(liquidContainer);
freight.PrintContainerInfo(gasContainer);
freight.PrintContainerInfo(freezerContainer);

freight.RemoveContainerFromShip(gasContainer);

var newGasContainer = new GasContainer(250, 6, 12, 18, 300, 20);
freight.ReplaceContainerOnShip(liquidContainer, newGasContainer);

freight.PrintShipInfo();