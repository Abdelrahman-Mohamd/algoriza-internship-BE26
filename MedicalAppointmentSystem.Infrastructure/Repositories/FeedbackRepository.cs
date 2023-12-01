using MedicalAppointmentSystem.Core.DTOs;
using MedicalAppointmentSystem.Core.Entities;
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

    public class FeedbackRepository: IFeedbackService
    {
        private readonly MedicalAppointmentDbContext _dbContext;
        public FeedbackRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<FeedbackModel>> GetFeedbacksByDoctorId(int doctorId)
        {
            try
            {
                // Query the database to get feedbacks for a specific doctor
                var feedbacks = await _dbContext.Feedbacks
                    .Where(f => f.DoctorId == doctorId)
                    .Select(f => new FeedbackModel
                    {
                        ImageLink = f.Patient.ImageLink,
                        FullName = f.Patient.FullName,
                        Feedback = f.FeedbackText,
                                              
                    })
                    .ToListAsync();

                return feedbacks;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> AddFeedback(FeedbackAddModel feedbackModel)
        {
            try
            {
                // Check if the doctor exists
                var doctorExists = await _dbContext.Doctors.AnyAsync(d => d.Id == feedbackModel.DoctorId);

                if (!doctorExists)
                {
                    // Return false or handle the case where the doctor doesn't exist
                    return false;
                }

                // Map FeedbackAddModel to Feedback entity
                var newFeedback = new Feedback
                {
                    DoctorId = feedbackModel.DoctorId,
                    FeedbackText = feedbackModel.Feedback,
                };

                // Add the new feedback to the database
                _dbContext.Feedbacks.Add(newFeedback);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful addition
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
