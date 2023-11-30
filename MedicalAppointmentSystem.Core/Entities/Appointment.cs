using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Days AppointmentDay { get; set; }
        public int TimeSlotId { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
