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
    public class AdminRepository : IAdminService
    {
        private readonly MedicalAppointmentDbContext _dbContext;

        public AdminRepository(MedicalAppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetNumberOfDoctors()
        {
            try
            {
                //get the number of doctors from the database
                var numberOfDoctors = await _dbContext.Doctors.CountAsync();

                return numberOfDoctors;
            }
            catch (Exception ex)
            {
                //handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0; // Return appropriate value for error scenario
            }
        }
        public async Task<int> GetNumberOfPatients()
        {
            try
            {
                // get the number of patients from the database
                var numberOfPatients = await _dbContext.Patients.CountAsync();

                return numberOfPatients;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0; // Return appropriate value for error scenario
            }
        }
        public async Task<dynamic> GetNumberOfAppointmentRequests()
        {
            try
            {
                // get the counts from the database
                var numberOfAppointments = await _dbContext.Appointments.CountAsync();
                var numberOfPendingAppointments = await _dbContext.Appointments.CountAsync(a => a.Status == "Pending");
                var numberOfCompletedAppointments = await _dbContext.Appointments.CountAsync(a => a.Status == "Completed");
                var numberOfCancelledAppointments = await _dbContext.Appointments.CountAsync(a => a.Status == "Cancelled");

                var result = new
                {
                    NumberOfAppointments = numberOfAppointments,
                    NumberOfPendingAppointments = numberOfPendingAppointments,
                    NumberOfCompletedAppointments = numberOfCompletedAppointments,
                    NumberOfCancelledAppointments = numberOfCancelledAppointments
                };

                return result;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Return appropriate value for error scenario
            }
        }
        public async Task<dynamic> GetTopFiveSpecializations()
        {
            try
            {
                // get the top five specializations from the database
                var topFiveSpecializations = await _dbContext.Specializations
                    .OrderByDescending(s => s.Doctors.SelectMany(d => d.Appointments).Count())
                    .Take(5)
                    .Select(s => new
                    {
                        DoctorName = s.Doctors.FirstOrDefault().FirstName + " " + s.Doctors.FirstOrDefault().LastName, 
                        NumberOfAppointments = s.Doctors.SelectMany(d => d.Appointments).Count()
                    })
                    .ToListAsync();

                return topFiveSpecializations;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Return appropriate value for error scenario
            }
        }
        public async Task<dynamic> GetTopTenDoctors()
        {
            try
            {
                // get the top ten doctors from the database
                var topTenDoctors = await _dbContext.Doctors
                    .OrderByDescending(d => d.Appointments.Count())
                    .Take(10)
                    .Select(d => new
                    {
                        ImageLink = d.ImageLink,
                        FullName = d.FirstName + " " + d.LastName,
                        Specialization = d.Specialization.Name,
                        NumberOfAppointments = d.Appointments.Count()
                    })
                    .ToListAsync();

                return topTenDoctors;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<List<DoctorModel>> GetAllDoctors(int page, int pageSize, string search)
        {
            try
            {
                var query = _dbContext.Doctors
                    .Include(d => d.Specialization)
                    .Where(d => string.IsNullOrEmpty(search) ||
                                d.FirstName.Contains(search) ||
                                d.LastName.Contains(search) ||
                                d.Specialization.Name.Contains(search))
                    .OrderBy(d => d.FirstName) 
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                var doctors = await query.Select(d => new DoctorModel
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
                        ImageLink = f.ImageLink,
                        FullName = f.Patient.FullName,
                        Feedback = f.FeedbackText
                    }).ToList()
                }).ToListAsync();

                return doctors;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<DoctorModel> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _dbContext.Doctors
                    .Include(d => d.Specialization)
                    .Include(d => d.Feedbacks)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (doctor == null)
                {
                    // Return appropriate value or handle the case where the doctor is not found
                    return null;
                }

                var doctorModel = new DoctorModel
                {
                    ImageLink = doctor.ImageLink,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email,
                    Phone = doctor.Phone,
                    Specialization = doctor.Specialization,
                    Gender = doctor.Gender,
                    DateOfBirth = doctor.DateOfBirth,
                    Feedbacks = doctor.Feedbacks.Select(f => new FeedbackModel
                    {
                        ImageLink = f.ImageLink,
                        FullName = f.Patient.FullName,
                        Feedback = f.FeedbackText
                    }).ToList()
                };

                return doctorModel;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Return appropriate value for error scenario
            }
        }
        public async Task<bool> AddNewDoctor(DoctorModel doctorModel)
        {
            try
            {
                // Check if the doctor with the given email already exists
                if (await _dbContext.Doctors.AnyAsync(d => d.Email == doctorModel.Email))
                {
                    // Return false or handle the case where the doctor already exists
                    return false;
                }

                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(doctorModel.Password);

                // Map DoctorModel to Doctor entity
                var newDoctor = new Doctor
                {
                    ImageLink = doctorModel.ImageLink,
                    FirstName = doctorModel.FirstName,
                    LastName = doctorModel.LastName,
                    Email = doctorModel.Email,
                    PasswordHash = hashedPassword,
                    Phone = doctorModel.Phone,
                    Gender = doctorModel.Gender,
                    DateOfBirth = doctorModel.DateOfBirth,
                    AdminId = 1, 
                    SpecializationId = doctorModel.SpecializationId
                };

                // Add the new doctor to the database
                _dbContext.Doctors.Add(newDoctor);
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
        public async Task<bool> EditDoctor(DoctorModel doctorModel)
        {
            try
            {
                // Retrieve the existing doctor from the database
                var existingDoctor = await _dbContext.Doctors.FindAsync(doctorModel.Id);

                // Check if the doctor exists
                if (existingDoctor == null)
                {
                    // Return false or handle the case where the doctor does not exist
                    return false;
                }

                // Update the properties of the existing doctor with the values from DoctorModel
                existingDoctor.ImageLink = doctorModel.ImageLink;
                existingDoctor.FirstName = doctorModel.FirstName; 
                existingDoctor.LastName = doctorModel.LastName; 
                existingDoctor.Email = doctorModel.Email;
                existingDoctor.Phone = doctorModel.Phone;
                existingDoctor.Gender = doctorModel.Gender;
                existingDoctor.DateOfBirth = doctorModel.DateOfBirth;
                existingDoctor.Specialization = doctorModel.Specialization;


                // Update the doctor in the database
                _dbContext.Doctors.Update(existingDoctor);
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
        public async Task<bool> DeleteDoctor(int id)
        {
            try
            {
                // Find the doctor with the specified id
                var doctorToDelete = await _dbContext.Doctors.FindAsync(id);

                // Check if the doctor exists
                if (doctorToDelete == null)
                {
                    // Return false or handle the case where the doctor does not exist
                    return false;
                }

                // Check if the doctor has any appointments
                if (_dbContext.Appointments.Any(appointment => appointment.DoctorId == id))
                {
                    // Return false or handle the case where the doctor has appointments
                    return false;
                }

                // Remove the doctor from the database
                _dbContext.Doctors.Remove(doctorToDelete);
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
        public async Task<List<PatientModel>> GetAllPatients(int page, int pageSize, string search)
        {
            try
            {
                // Query patients based on the search criteria
                var query = _dbContext.Patients
                    .Where(patient =>
                        string.IsNullOrEmpty(search) ||
                        patient.FullName.Contains(search) ||
                        patient.Email.Contains(search) ||
                        patient.Phone.Contains(search));

                // Calculate the number of items to skip based on pagination parameters
                int itemsToSkip = (page - 1) * pageSize;

                // Retrieve patients for the specified page and page size
                var patients = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();

                // Map the Patient entities to PatientModel DTOs
                var patientModels = patients.Select(patient => new PatientModel
                {
                    ImageLink = patient.ImageLink,
                    FullName = patient.FullName,
                    Email = patient.Email,
                    Phone = patient.Phone,
                    Gender = patient.Gender,
                    DateOfBirth = patient.DateOfBirth
                }).ToList();

                return patientModels;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<PatientModel> GetPatientById(int id)
        {
            try
            {
                // Retrieve the patient by ID
                var patient = await _dbContext.Patients.FindAsync(id);

                // Check if the patient was not found
                if (patient == null)
                {
                    // Handle the case where the patient with the specified ID was not found
                    return null;
                }

                // Map the Patient entity to PatientModel DTO
                var patientModel = new PatientModel
                {
                    ImageLink = patient.ImageLink,
                    FullName = patient.FullName,
                    Email = patient.Email,
                    Phone = patient.Phone,
                    Gender = patient.Gender,
                    DateOfBirth = patient.DateOfBirth
                };

                return patientModel;
            }
            catch (Exception ex)
            {
                // handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> AddDiscountCode(DiscountCodeModel discountCodeModel)
        {
            try
            {
                // Check if the discount code with the given code already exists
                if (await _dbContext.DiscountCodes.AnyAsync(dc => dc.Code == discountCodeModel.DiscountCode))
                {
                    // Return false or handle the case where the discount code already exists
                    return false;
                }

                // Map DiscountCodeModel to DiscountCode entity
                var newDiscountCode = new DiscountCode
                {
                    Code = discountCodeModel.DiscountCode,
                    NumberOfAppointmentsCompleted = discountCodeModel.NumberOfAppointments,
                    DiscountType = discountCodeModel.DiscountType,
                    Value = discountCodeModel.Value,
                };

                // Add the new discount code to the database
                _dbContext.DiscountCodes.Add(newDiscountCode);
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
        public async Task<bool> UpdateDiscountCode(DiscountCodeUpdateModel discountCodeUpdateModel)
        {
            try
            {
                // Retrieve the existing discount code by ID
                var existingDiscountCode = await _dbContext.DiscountCodes.FindAsync(discountCodeUpdateModel.Id);

                // Check if the discount code with the given ID exists
                if (existingDiscountCode == null)
                {
                    // Return false or handle the case where the discount code does not exist
                    return false;
                }

                // Check if the discount code is already applied to any appointments
                if (_dbContext.Appointments.Any(appointment => appointment.DiscountCodeId == discountCodeUpdateModel.Id))
                {
                    // Return false or handle the case where the discount code is already applied
                    return false;
                }

                // Update the properties of the existing discount code
                existingDiscountCode.Code = discountCodeUpdateModel.DiscountCode;
                existingDiscountCode.NumberOfAppointmentsCompleted = discountCodeUpdateModel.NumberOfAppointments;
                existingDiscountCode.DiscountType = discountCodeUpdateModel.DiscountType;
                existingDiscountCode.Value = discountCodeUpdateModel.Value;

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
        public async Task<bool> DeleteDiscountCode(int id)
        {
            try
            {
                // Retrieve the discount code by ID
                var discountCode = await _dbContext.DiscountCodes.FindAsync(id);

                // Check if the discount code with the given ID exists
                if (discountCode == null)
                {
                    // Return false or handle the case where the discount code does not exist
                    return false;
                }

                // Check if the discount code is already applied to any appointments
                if (_dbContext.Appointments.Any(appointment => appointment.DiscountCodeId == id))
                {
                    // Return false or handle the case where the discount code is already applied
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
                // Retrieve the discount code by ID
                var discountCode = await _dbContext.DiscountCodes.FindAsync(id);

                // Check if the discount code with the given ID exists
                if (discountCode == null)
                {
                    // Return false or handle the case where the discount code does not exist
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
