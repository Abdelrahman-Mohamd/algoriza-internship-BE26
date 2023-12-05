using MedicalAppointmentSystem.Core.Interfaces;
using MedicalAppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using MedicalAppointmentSystem.Core.DTOs;
using MedicalAppointmentSystem.Core.Entities;

namespace MedicalAppointmentSystem.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorService
    {
        private readonly MedicalAppointmentDbContext _dbContext;

        public DoctorRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                // Retrieve the doctor from the database using the email
                var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.Email == email);

                // Check if the doctor with the given email exists
                if (doctor == null)
                {
                    // Doctor not found, return false or handle the case accordingly
                    return false;
                }

                // Check if the doctor's account is active
                if (!doctor.IsActive)
                {
                    // Doctor account is not active, return false or handle the case accordingly
                    return false;
                }

                // Validate the password using BCrypt
                if (BCrypt.Net.BCrypt.Verify(password, doctor.PasswordHash))
                {
                    // Password is valid, return true
                    return true;
                }

                // Password is invalid, return false
                return false;
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
                var doctor = await _dbContext.Doctors
                    .Include(d => d.Specialization)
                    .FirstOrDefaultAsync(d => d.Id == doctorId);

                if (doctor == null)
                {
                    return null;
                }

                var appointments = await _dbContext.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.DiscountCode)
                    .Where(a => a.DoctorId == doctorId &&
                                (string.IsNullOrEmpty(searchBy) ||
                                 EF.Functions.Like(a.Patient.FullName, $"%{searchBy}%")))
                    .OrderByDescending(a => a.AppointmentDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var appointmentModels = appointments.Select(a => new AppointmentModel
                {
                    ImageLink = doctor.ImageLink,
                    DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                    Specialization = doctor.Specialization.Name,
                    Day = a.AppointmentDay,
                    Time = a.AppointmentDate,
                    Price = a.Price,
                    DiscountCode = a.DiscountCode.Code,
                    FinalPrice = a.FinalPrice,
                    Status = a.Status,
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
        public async Task<bool> ConfirmCheckUp(int bookingId)
        {
            try
            {
                var appointment = await _dbContext.Appointments
                    .FirstOrDefaultAsync(a => a.Id == bookingId);

                if (appointment == null)
                {
                    return false;
                }

                appointment.Status = "Confirmed";
                await _dbContext.SaveChangesAsync();

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
                
                var newAppointment = new Appointment
                {
                    DoctorId = appointmentRequest.DoctorId,
                    PatientId = appointmentRequest.PatientId,
                    TimeSlotId = appointmentRequest.TimeSlotId,
                   
                };

                _dbContext.Appointments.Add(newAppointment);
                await _dbContext.SaveChangesAsync();

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
                // Find the appointment based on doctorId and timeSlotId
                var appointmentToUpdate = await _dbContext.Appointments
                    .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.TimeSlotId == timeSlotId);

                if (appointmentToUpdate == null)
                {
                    // Handle the case where the appointment is not found
                    return false;
                }

                
                appointmentToUpdate.Status = "Updated"; 

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();

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
                // Find the appointment based on doctorId and timeSlotId
                var appointmentToDelete = await _dbContext.Appointments
                    .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.TimeSlotId == timeSlotId);

                if (appointmentToDelete == null)
                {
                    // Handle the case where the appointment is not found
                    return false;
                }

                // Remove the appointment from the database
                _dbContext.Appointments.Remove(appointmentToDelete);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                //handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; 
            }
        }



    }
}
