
using CleanTeeth.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Clean_Teeth.Domain.Value_Objects;
public class Email
{
    public string Value { get; } = null!;

    public Email(string email)
    {
        //Formato valido de email
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, emailRegex))
        {
            throw new BusinessRuleException("El formato del correo electrónico no es válido");
        }
    }

}
