using MedicalAppointmentSystem.Core.DTOs;
using MedicalAppointmentSystem.Core.Interfaces;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    internal class SpecializationRepository : ISpecializationService
    {
        private readonly MedicalAppointmentDbContext _dbContext;
        public SpecializationRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SpecializationModel>> GetAllSpecializations()
        {
           
            try
            {
                //get all specializations
                var specializations = await _dbContext.Specializations
                    .Select(s => new SpecializationModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
                    .ToListAsync();

                return specializations;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
