using MedicalAppointmentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<bool> Login(string email, string password);
        Task<List<AppointmentModel>> GetAllAppointments(int doctorId, string searchBy, int pageSize, int pageNumber);
        Task<bool> ConfirmCheckUp(int bookingId);
        Task<bool> AddAppointment(AppointmentRequestModel appointmentRequest);
        Task<bool> UpdateAppointment(int doctorId, int timeSlotId);
        Task<bool> DeleteAppointment(int doctorId, int timeSlotId);
    }
}
