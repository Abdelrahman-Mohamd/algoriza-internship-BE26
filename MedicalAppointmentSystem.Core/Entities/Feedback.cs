using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string ImageLink { get; set; }
        public string FeedbackText { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
