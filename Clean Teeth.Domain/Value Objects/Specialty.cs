
namespace Clean_Teeth.Domain.Value_Objects;

public class Specialty
{
    public String Name { get; private set; }
    public String Descripcion { get; private set; }

    public Specialty(String name, String descripcion)
    {
        Name = name;
        Descripcion = descripcion;
    }
}
