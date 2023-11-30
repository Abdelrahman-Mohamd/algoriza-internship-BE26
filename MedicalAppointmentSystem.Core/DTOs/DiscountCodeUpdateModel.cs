using MedicalAppointmentSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.DTOs
{
    public class DiscountCodeUpdateModel
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public int NumberOfAppointments { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
    }
}
