using MedicalAppointmentSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Application.Services
{
    public interface IAdminAppService
    {
        Task<int> GetNumberOfDoctors();
        Task<int> GetNumberOfPatients();
        Task<dynamic> GetNumberOfAppointmentRequests();
        Task<dynamic> GetTopFiveSpecializations();
        Task<dynamic> GetTopTenDoctors();

        Task<List<DoctorDTO>> GetAllDoctors(int page, int pageSize, string search);
        Task<DoctorDTO> GetDoctorById(int id);
        Task<bool> AddNewDoctor(DoctorDTO doctorDTO);
        Task<bool> EditDoctor(DoctorDTO doctorDTO);
        Task<bool> DeleteDoctor(int id);

        Task<List<PatientDTO>> GetAllPatients(int page, int pageSize, string search);
        Task<PatientDTO> GetPatientById(int id);

        Task<bool> AddDiscountCode(DiscountCodeDTO discountCodeDTO);
        Task<bool> UpdateDiscountCode(DiscountCodeUpdateDTO discountCodeUpdateDTO);
        Task<bool> DeleteDiscountCode(int id);
        Task<bool> DeactivateDiscountCode(int id);
    }
}
