using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class AppointmentBookingDTO
    {
        public int TimeSlotId { get; set; }
        public int DoctorId { get; set; }
        public string DiscountCodeCoupon { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Days AppointmentDay { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
    }
}
