using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.DTOs
{
    public class DayTimeSlotModel
    {
        public Days Day { get; set; }
        public List<TimeSlotModel> TimeSlots { get; set; }
    }
}
