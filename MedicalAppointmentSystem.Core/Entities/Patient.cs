using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<DiscountCode> DiscountCodes { get; set; }
    }
}
