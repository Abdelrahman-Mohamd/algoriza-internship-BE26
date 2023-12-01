using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public SpecializationDTO Specialization { get; set; }
        public int SpecializationId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<FeedbackDTO> Feedbacks { get; set; }
    }
}
