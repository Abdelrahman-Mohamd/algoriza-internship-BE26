using MedicalAppointmentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Core.Interfaces
{
    public interface IAdminService
    {
        Task<int> GetNumberOfDoctors();
        Task<int> GetNumberOfPatients();
        Task<dynamic> GetNumberOfAppointmentRequests();
        Task<dynamic> GetTopFiveSpecializations();
        Task<dynamic> GetTopTenDoctors();

        Task<List<DoctorModel>> GetAllDoctors(int page, int pageSize, string search);
        Task<DoctorModel> GetDoctorById(int id);
        Task<bool> AddNewDoctor(DoctorModel doctorModel);
        Task<bool> EditDoctor(DoctorModel doctorModel);
        Task<bool> DeleteDoctor(int id);

        Task<List<PatientModel>> GetAllPatients(int page, int pageSize, string search);
        Task<PatientModel> GetPatientById(int id);

        Task<bool> AddDiscountCode(DiscountCodeModel discountCodeModel);
        Task<bool> UpdateDiscountCode(DiscountCodeUpdateModel discountCodeUpdateModel);
        Task<bool> DeleteDiscountCode(int id);
        Task<bool> DeactivateDiscountCode(int id);
    }
}
