
using CleanTeeth.Domain.Exceptions;

namespace Clean_Teeth.Domain.Value_Objects;
public class Email
{
    public string Value { get; } = null!;

    public Email(string email)
    {
        if (String.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException($"The {nameof(email)} is required");
        }

        if (!email.Contains("@"))
        {
            throw new BusinessRuleException($"The {nameof(email)} is not valid");
        }

        Value = email;
    }

}
