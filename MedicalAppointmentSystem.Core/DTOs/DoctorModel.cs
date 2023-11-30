using MedicalAppointmentSystem.Core.Entities;
using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.DTOs
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<FeedbackModel> Feedbacks { get; set; }
    }
}
