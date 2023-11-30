using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.DTOs
{
    public class FeedbackAddModel
    {
        public int DoctorId { get; set; }
        public string Feedback { get; set; }
    }
}
