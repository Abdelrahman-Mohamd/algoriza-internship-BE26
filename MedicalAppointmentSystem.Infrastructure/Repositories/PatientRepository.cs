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
    public class PatientRepository : IPatientService
    {
        private readonly MedicalAppointmentDbContext _dbContext;

        public PatientRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Register(PatientRegistrationModel patientModel)
        {
            try
            {
                // Check if the patient with the given email already exists
                if (await _dbContext.Patients.AnyAsync(p => p.Email == patientModel.Email))
                {
                    // Return false or handle the case where the patient already exists
                    return false;
                }

                // Map PatientRegistrationModel to Patient entity
                var newPatient = new Patient
                {
                    ImageLink = patientModel.ImageLink,
                    FullName = patientModel.FirstName + " " + patientModel.LastName,
                    Email = patientModel.Email,
                    Phone = patientModel.Phone,
                    Gender = patientModel.Gender,
                    DateOfBirth = patientModel.DateOfBirth
                };

                // Add the new patient to the database
                _dbContext.Patients.Add(newPatient);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful registration
                return true;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                // Retrieve the patient from the database using the email
                var patient = await _dbContext.Patients.SingleOrDefaultAsync(d => d.Email == email);

                // Check if the doctor with the given email exists
                if (patient == null)
                {
                    // Patient not found, return false or handle the case accordingly
                    return false;
                }

                // Check if the patients's account is active
                if (!patient.IsActive)
                {
                    // Patients account is not active, return false or handle the case accordingly
                    return false;
                }

                // Validate the password using BCrypt
                if (BCrypt.Net.BCrypt.Verify(password, patient.PasswordHash))
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
        public async Task<List<DoctorModel>> GetAllDoctors(int page, int pageSize, string search)
        {
            try
            {
                
                var doctors = await _dbContext.Doctors
                    .Include(d => d.Specialization) 
                    .Where(d => d.FirstName.Contains(search) || d.LastName.Contains(search))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(d => new DoctorModel
                    {
                        ImageLink = d.ImageLink,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Email = d.Email,
                        Phone = d.Phone,
                        Specialization = d.Specialization, 
                        Gender = d.Gender,
                        DateOfBirth = d.DateOfBirth,
                        Feedbacks = d.Feedbacks.Select(f => new FeedbackModel
                        {
                        }).ToList()
                    })
                    .ToListAsync();

                return doctors;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; 
            }
        }
        public async Task<bool> BookAppointment(AppointmentBookingModel appointmentBookingModel)
        {
            try
            {
                // Check if the appointment slot is available
                var isSlotAvailable = await IsAppointmentSlotAvailable(appointmentBookingModel.DoctorId, appointmentBookingModel.TimeSlotId);
                if (!isSlotAvailable)
                {
                    // Return false or handle the case where the slot is not available
                    return false;
                }

                // Map AppointmentBookingModel to Appointment entity
                var newAppointment = new Appointment
                {
                    DoctorId = appointmentBookingModel.DoctorId,
                    PatientId = appointmentBookingModel.PatientId,
                    AppointmentDate = appointmentBookingModel.AppointmentDate,
                    AppointmentDay = appointmentBookingModel.AppointmentDay,
                    TimeSlotId = appointmentBookingModel.TimeSlotId,
                    Price = appointmentBookingModel.Price,
                    FinalPrice = appointmentBookingModel.FinalPrice,
                    Status = appointmentBookingModel.Status,
                    // Map other properties as needed
                };

                // Add the new appointment to the database
                _dbContext.Appointments.Add(newAppointment);
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful appointment booking
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false indicating an error occurred
            }

        }

        private async Task<bool> IsAppointmentSlotAvailable(int doctorId, int timeSlotId)
        {
            // Check if the time slot is available for the given doctor
            var isSlotAvailable = await _dbContext.Appointments
                .AnyAsync(a => a.DoctorId == doctorId && a.TimeSlotId == timeSlotId);

            // If the slot is available, return false (already booked); otherwise, return true
            return !isSlotAvailable;
        }
        public async Task<List<AppointmentModel>> GetAllBookings(int patientId)
        {
            try
            {
                

                var appointments = await _dbContext.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.Doctor.Specialization) 
                    .Where(a => a.PatientId == patientId)
                    .ToListAsync();

                var bookingModels = appointments.Select(a => new AppointmentModel
                {
                    ImageLink = a.Doctor.ImageLink,
                    DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                    Specialization = a.Doctor.Specialization.Name,
                    Day = a.AppointmentDay,
                    Time = a.AppointmentDate,
                    Price = a.Price,
                    DiscountCode = a.DiscountCode.Code,
                    FinalPrice = a.FinalPrice,
                    Status = a.Status
                }).ToList();

                return bookingModels;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> CancelBooking(int bookingId, int patientId)
        {
            try
            {
                

                // Check if the booking belongs to the current patient
                var booking = await _dbContext.Appointments
                    .FirstOrDefaultAsync(a => a.Id == bookingId && a.PatientId == patientId);

                if (booking == null)
                {
                    // The booking doesn't exist or doesn't belong to the current patient
                    return false;
                }

                booking.Status = "Canceled";

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Return true indicating successful cancellation
                return true;
            }
            catch (Exception ex)
            {
                // hhandle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
