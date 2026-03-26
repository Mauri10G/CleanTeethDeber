
using CleanTeeth.Domain.Exceptions;

namespace Clean_Teeth.Domain.Exceptions;

// Actividad Pagina: 35
public sealed class TimeInterval
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public double DuraticionMinutos => (End - Start).TotalMinutes;
    public TimeInterval(DateTime start , DateTime end)
    {
        if(start > end)
        {
            throw new BusinessRuleException("La fecha de inicio debe ser antes que la fecha final");
        }
        Start = start;
        End = end;
    }

}
