using MedicalAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public interface IDoctorAppService
    {
        Task<bool> Login(string email, string password);
        Task<List<AppointmentDTO>> GetAllAppointments(int doctorId, string searchBy, int pageSize, int pageNumber);
        Task<bool> ConfirmCheckUp(int bookingId);
        Task<bool> AddAppointment(AppointmentRequestDTO appointmentRequest);
        Task<bool> UpdateAppointment(int doctorId, int timeSlotId);
        Task<bool> DeleteAppointment(int doctorId, int timeSlotId);
    }
}
