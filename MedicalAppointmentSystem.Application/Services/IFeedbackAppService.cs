using MedicalAppointmentSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public interface IFeedbackAppService
    {
        Task<List<FeedbackDTO>> GetFeedbacksByDoctorId(int doctorId);
        Task<bool> AddFeedback(FeedbackAddDTO feedbackModel);
    }
}
