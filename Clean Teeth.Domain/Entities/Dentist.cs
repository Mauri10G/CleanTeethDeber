
using Clean_Teeth.Domain.Value_Objects;
using CleanTeeth.Domain.Exceptions;

namespace Clean_Teeth.Domain.Entities;

public class Dentist
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Dentist(string name, Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"El {nameof(name)} es requerido");
        }

        if (email is null)
        {
            throw new BusinessRuleException($"El {nameof(email)} es requerido");
        }
        Name = name;
        Email = email;
        Id = Guid.CreateVersion7();
    }

}
