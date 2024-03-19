using cw3.Containers;

namespace cw3; 

public interface IContainer {
    

    static int _id = 0;
    double MaxLoad { get; set; } 
    double LoadMass { get; set; }


    void Empty();
    void Load(double loadMass);
}