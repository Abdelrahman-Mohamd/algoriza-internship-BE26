using MedicalAppointmentSystem.Core.DTOs;
using MedicalAppointmentSystem.Core.Entities;
using MedicalAppointmentSystem.Core.Interfaces;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentService
    {
        private readonly MedicalAppointmentDbContext _dbContext;

        public AppointmentRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ConfirmCheckUp(int bookingId)
        {
            try
            {
                // Retrieve the appointment by ID
                var appointment = await _dbContext.Appointments.FindAsync(bookingId);

                // Check if the appointment with the given ID exists
                if (appointment == null)
                {
                    // Return false or handle the case where the appointment does not exist
                    return false;
                }

                // Update the appointment status to "Confirmed" 
                appointment.Status = "Confirmed";

                // Update the appointment in the database
                _dbContext.Appointments.Update(appointment);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful confirmation
                return true;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddAppointment(AppointmentRequestModel appointmentRequest)
        {
            try
            {
                // Retrieve the doctor by ID
                var doctor = await _dbContext.Doctors.FindAsync(appointmentRequest.DoctorId);

                // Check if the doctor with the given ID exists
                if (doctor == null)
                {
                    // Return false or handle the case where the doctor does not exist
                    return false;
                }

                // Check if the provided time slot is available
                var timeSlot = doctor.Appointments.FirstOrDefault(a => a.Id == appointmentRequest.TimeSlotId);

                if (timeSlot == null)
                {
                    // Return false or handle the case where the time slot does not exist
                    return false;
                }

                // Update time slot Id
                timeSlot.TimeSlotId = appointmentRequest.TimeSlotId;

                // Create a new appointment based on the request model
                var newAppointment = new Appointment
                {
                    DoctorId = doctor.Id,
                    PatientId = appointmentRequest.PatientId, 
                    AppointmentDate = DateTime.Now, 
                };

                // Add the new appointment to the database
                _dbContext.Appointments.Add(newAppointment);
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
        public async Task<bool> UpdateAppointment(int doctorId, int timeSlotId)
        {
            try
            {
                // Retrieve the doctor by ID
                var doctor = await _dbContext.Doctors.FindAsync(doctorId);

                // Check if the doctor with the given ID exists
                if (doctor == null)
                {
                    // Return false or handle the case where the doctor does not exist
                    return false;
                }

                // Check if the provided time slot is available
                var timeSlot = doctor.Appointments.FirstOrDefault(a => a.Id == timeSlotId);

                if (timeSlot == null)
                {
                    // Return false or handle the case where the time slot does not exist
                    return false;
                }

                // Update time slot Id
                timeSlot.TimeSlotId = timeSlotId;

                // Save changes to the database
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
        public async Task<bool> DeleteAppointment(int doctorId, int timeSlotId)
        {
            try
            {
                // Retrieve the doctor by ID
                var doctor = await _dbContext.Doctors.FindAsync(doctorId);

                // Check if the doctor with the given ID exists
                if (doctor == null)
                {
                    // Return false or handle the case where the doctor does not exist
                    return false;
                }

                // Check if the provided time slot exists
                var timeSlot = doctor.Appointments.FirstOrDefault(a => a.Id == timeSlotId);

                if (timeSlot == null)
                {
                    // Return false or handle the case where the time slot does not exist
                    return false;
                }

                // Remove the time slot from the doctor's appointments
                doctor.Appointments.Remove(timeSlot);

                // Save changes to the database
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
        public async Task<List<AppointmentModel>> GetAllAppointments(int doctorId, string searchBy, int pageSize, int pageNumber)
        {
            try
            {
                var appointments = await _dbContext.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.DiscountCode)
                    .Include(a => a.Doctor)
                    .Where(a => a.DoctorId == doctorId && a.Status != "Cancelled")
                    .OrderBy(a => a.AppointmentDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentModels = appointments.Select(a => new AppointmentModel
                {
                    ImageLink = a.Doctor.ImageLink, 
                    DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                    Specialization = a.Doctor.Specialization.Name,
                    Day = a.AppointmentDay,
                    Time = a.AppointmentDate,
                    Price = a.Price,
                    DiscountCode = a.DiscountCode?.Code, 
                    FinalPrice = a.FinalPrice,
                    Status = a.Status
                    
                }).ToList();

                return appointmentModels;
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
