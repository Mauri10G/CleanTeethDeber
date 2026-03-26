
using Clean_Teeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;

namespace Clean_Teeth.Domain.Entities;

public class DentalOffice
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    //Lista para manejar las citas 
    private readonly List<Appointment> _appointments = new();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public DentalOffice(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"El {nameof(name)} es requerido");
        }
        Name = name;
        Id = Guid.CreateVersion7();
    }

    public void ScheduleAppointment(Appointment newAppointment)
    {
        // Validar que no choquen horarios
        bool overlaps = _appointments.Any(a =>
            a.DentistId == newAppointment.DentistId &&
            a.Status == AppointmentStatus.Scheduled &&
            newAppointment.TimeInterval.Start < a.TimeInterval.End &&
            a.TimeInterval.Start < newAppointment.TimeInterval.End);

        if (overlaps)
            throw new BusinessRuleException("El dentista ya tiene una cita programada en ese horario");

        _appointments.Add(newAppointment);
    }
}
