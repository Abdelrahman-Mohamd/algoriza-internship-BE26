using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class DayTimeSlotDTO
    {
        public Days Day { get; set; }
        public List<TimeSlotDTO> TimeSlots { get; set; }
    }
}
