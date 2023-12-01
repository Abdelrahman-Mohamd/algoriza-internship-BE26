using MedicalAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public interface IDiscountCodeAppService
    {
        Task<bool> AddDiscountCode(DiscountCodeDTO discountCodeModel);
        Task<bool> UpdateDiscountCode(DiscountCodeUpdateDTO discountCodeUpdateModel);
        Task<bool> DeleteDiscountCode(int id);
        Task<bool> DeactivateDiscountCode(int id);
    }
}
