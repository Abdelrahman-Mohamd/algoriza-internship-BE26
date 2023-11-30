using MedicalAppointmentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Interfaces
{
    public interface IPatientService
    {
        Task<bool> Register(PatientRegistrationModel patientModel);
        Task<bool> Login(string email, string password);
        Task<List<DoctorModel>> GetAllDoctors(int page, int pageSize, string search);
        Task<bool> BookAppointment(AppointmentBookingModel appointmentBookingModel);
        Task<List<AppointmentModel>> GetAllBookings();
        Task<bool> CancelBooking(int bookingId);
    }
}
