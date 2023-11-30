using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Entities
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int NumberOfAppointmentsCompleted { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public List<Appointment> Appointments { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
