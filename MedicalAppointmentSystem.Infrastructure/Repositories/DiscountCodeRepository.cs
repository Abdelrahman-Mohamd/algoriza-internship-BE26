using MedicalAppointmentSystem.Core.DTOs;
using MedicalAppointmentSystem.Core.Entities;
using MedicalAppointmentSystem.Core.Enums;
using MedicalAppointmentSystem.Core.Interfaces;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class DiscountCodeRepository :IDiscountCodeService
    {
        private readonly MedicalAppointmentDbContext _dbContext;

        public DiscountCodeRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddDiscountCode(DiscountCodeModel discountCodeModel)
        {
            try
            {
                // Map DiscountCodeModel to DiscountCode entity
                var newDiscountCode = new DiscountCode
                {
                    Code = discountCodeModel.DiscountCode,
                    NumberOfAppointmentsCompleted = discountCodeModel.NumberOfAppointments,
                    DiscountType = discountCodeModel.DiscountType,
                    Value = discountCodeModel.Value,
                    AdminId = discountCodeModel.AdminId, 
                    PatientId = discountCodeModel.PatientId
                };

                // Add the new discount code to the database
                _dbContext.DiscountCodes.Add(newDiscountCode);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful addition
                return true;
            }
            catch (Exception ex)
            {
                //  handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateDiscountCode(DiscountCodeUpdateModel discountCodeUpdateModel)
        {
            try
            {
                // Retrieve the existing discount code from the database
                var existingDiscountCode = await _dbContext.DiscountCodes.FindAsync(discountCodeUpdateModel.Id);

                if (existingDiscountCode == null)
                {
                    // Discount code not found, return false or handle the case accordingly
                    return false;
                }

                // Check if the discount code is already applied to any appointments
                if (existingDiscountCode.Appointments.Any())
                {
                    // Discount code is already applied, return false or handle the case accordingly
                    return false;
                }

                // Update the properties of the existing discount code
                existingDiscountCode.Code = discountCodeUpdateModel.DiscountCode;
                existingDiscountCode.NumberOfAppointmentsCompleted = discountCodeUpdateModel.NumberOfAppointments;
                existingDiscountCode.DiscountType = discountCodeUpdateModel.DiscountType;
                existingDiscountCode.Value = discountCodeUpdateModel.Value;

                // Update the discount code in the database
                _dbContext.DiscountCodes.Update(existingDiscountCode);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful update
                return true;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteDiscountCode(int id)
        {
            try
            {
                // Retrieve the discount code from the database
                var discountCode = await _dbContext.DiscountCodes.FindAsync(id);

                if (discountCode == null)
                {
                    // Discount code not found, return false or handle the case accordingly
                    return false;
                }

                // Check if the discount code is already applied to any appointments
                if (discountCode.Appointments.Any())
                {
                    // Discount code is already applied, return false or handle the case accordingly
                    return false;
                }

                // Remove the discount code from the database
                _dbContext.DiscountCodes.Remove(discountCode);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful deletion
                return true;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeactivateDiscountCode(int id)
        {
            try
            {
                // Retrieve the discount code from the database
                var discountCode = await _dbContext.DiscountCodes.FindAsync(id);

                if (discountCode == null)
                {
                    // Discount code not found, return false or handle the case accordingly
                    return false;
                }

                // Deactivate the discount code
                discountCode.IsActive = false;

                // Update the discount code in the database
                _dbContext.DiscountCodes.Update(discountCode);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful deactivation
                return true;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

    }
}
