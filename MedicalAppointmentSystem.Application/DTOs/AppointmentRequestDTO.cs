using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class AppointmentRequestDTO
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int TimeSlotId { get; set; }
    }
}
