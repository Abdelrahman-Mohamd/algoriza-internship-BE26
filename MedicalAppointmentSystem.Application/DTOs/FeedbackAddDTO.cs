using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.DTOs
{
    public class FeedbackAddDTO
    {
        public int DoctorId { get; set; }
        public string Feedback { get; set; }
    }
}
