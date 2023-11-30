using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string ImageLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
