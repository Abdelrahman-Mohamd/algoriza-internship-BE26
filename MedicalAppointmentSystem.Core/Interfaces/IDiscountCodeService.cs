using MedicalAppointmentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Interfaces
{
    public interface IDiscountCodeService
    {
        Task<bool> AddDiscountCode(DiscountCodeModel discountCodeModel);
        Task<bool> UpdateDiscountCode(DiscountCodeUpdateModel discountCodeUpdateModel);
        Task<bool> DeleteDiscountCode(int id);
        Task<bool> DeactivateDiscountCode(int id);
    }
}
