using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class AppointmentDTO
    {
        public string ImageLink { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public Days Day { get; set; }
        public System.DateTime Time { get; set; }
        public decimal Price { get; set; }
        public string DiscountCode { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
    }
}
