
using Clean_Teeth.Domain.Enums;
using Clean_Teeth.Domain.Value_Objects;
using CleanTeeth.Domain.Exceptions;

namespace Clean_Teeth.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid DentalOfficeId { get; private set; }
    public AppointmentStatus Status { get; private set;}
    public TimeInterval TimeInterval { get; private set;}
    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public DentalOffice? DentalOffice { get; private set; }
    public AppointmentType Type { get; private set; } // Tipo de cita
    public Appointment(Guid patientId, Guid dentistId, Guid dentalOfficeId , TimeInterval timeInterval, AppointmentType type)
    {
        if(timeInterval.Start < DateTime.UtcNow)
        {
            throw new BusinessRuleException($"La fecha de inicio no puede ser anterior a la fecha actual");
        }
        
        // Validacion de los 30 minutos
        if (type == AppointmentType.Treatment)
        {
            // Calculamos la diferencia entre fin e inicio
            TimeSpan duration = timeInterval.End - timeInterval.Start;

            if (duration.TotalMinutes < 30)
            {
                throw new BusinessRuleException("Una cita de tipo Tratamiento debe durar al menos 30 minutos.");
            }
        }

        PatientId = patientId;
        DentistId = dentistId;
        DentalOfficeId = dentalOfficeId;
        TimeInterval = timeInterval;
        Type = type;
        Status = AppointmentStatus.Scheduled;
        Id = Guid.CreateVersion7();
    }

    public void Cancel()
    {
        // No se puede cancelar si faltan menos de 24 horas
        if (DateTime.UtcNow.AddHours(24) > TimeInterval.Start)
        {
            throw new BusinessRuleException("No se puede cancelar con menos de 24 horas de antelación.");
        }

        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException($"Solo se puede cancelar una cita programada");
        }

        Status = AppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Scheduled) 
        {
            throw new BusinessRuleException($"Solo se puede completar una cita programada");
        }
        Status = AppointmentStatus.Completed;
    }

    //Reprogramacion 
    public void Reschedule(TimeInterval newInterval)
    {
        // No se puede reprogramar si faltan menos de 24 horas
        if (DateTime.UtcNow.AddHours(24) > TimeInterval.Start)
        {
            throw new BusinessRuleException("No se puede reprogramar con menos de 24 horas de antelacion");
        }

        TimeInterval = newInterval;
        Status = AppointmentStatus.Rescheduled;
    }

}
