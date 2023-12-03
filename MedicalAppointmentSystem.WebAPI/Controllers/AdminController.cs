using MedicalAppointmentSystem.Application.DTOs;
using MedicalAppointmentSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;



[Route("api/admin")]
[ApiController]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly IAdminAppService _adminAppService;

    public AdminController(IAdminAppService adminAppService)
    {
        _adminAppService = adminAppService;
    }

    [HttpGet("getNumberOfDoctors")]
    public async Task<IActionResult> GetNumberOfDoctors()
    {
        try
        {
            var result = await _adminAppService.GetNumberOfDoctors();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getNumberOfPatients")]
    public async Task<IActionResult> GetNumberOfPatients()
    {
        try
        {
            var result = await _adminAppService.GetNumberOfPatients();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getNumberOfAppointmentRequests")]
    public async Task<IActionResult> GetNumberOfAppointmentRequests()
    {
        try
        {
            var result = await _adminAppService.GetNumberOfAppointmentRequests();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getTopFiveSpecializations")]
    public async Task<IActionResult> GetTopFiveSpecializations()
    {
        try
        {
            var result = await _adminAppService.GetTopFiveSpecializations();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getTopTenDoctors")]
    public async Task<IActionResult> GetTopTenDoctors()
    {
        try
        {
            var result = await _adminAppService.GetTopTenDoctors();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getAllDoctors")]
    public async Task<IActionResult> GetAllDoctors(int page, int pageSize, string search)
    {
        try
        {
            var result = await _adminAppService.GetAllDoctors(page, pageSize, search);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getDoctorById/{id}")]
    public async Task<IActionResult> GetDoctorById(int id)
    {
        try
        {
            var result = await _adminAppService.GetDoctorById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("addNewDoctor")]
    public async Task<IActionResult> AddNewDoctor([FromBody] DoctorDTO doctorDTO)
    {
        try
        {
            var result = await _adminAppService.AddNewDoctor(doctorDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("editDoctor")]
    public async Task<IActionResult> EditDoctor([FromBody] DoctorDTO doctorDTO)
    {
        try
        {
            var result = await _adminAppService.EditDoctor(doctorDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("deleteDoctor/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        try
        {
            var result = await _adminAppService.DeleteDoctor(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getAllPatients")]
    public async Task<IActionResult> GetAllPatients(int page, int pageSize, string search)
    {
        try
        {
            var result = await _adminAppService.GetAllPatients(page, pageSize, search);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getPatientById/{id}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        try
        {
            var result = await _adminAppService.GetPatientById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("addDiscountCode")]
    public async Task<IActionResult> AddDiscountCode([FromBody] DiscountCodeDTO discountCodeDTO)
    {
        try
        {
            var result = await _adminAppService.AddDiscountCode(discountCodeDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("updateDiscountCode")]
    public async Task<IActionResult> UpdateDiscountCode([FromBody] DiscountCodeUpdateDTO discountCodeUpdateDTO)
    {
        try
        {
            var result = await _adminAppService.UpdateDiscountCode(discountCodeUpdateDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("deleteDiscountCode/{id}")]
    public async Task<IActionResult> DeleteDiscountCode(int id)
    {
        try
        {
            var result = await _adminAppService.DeleteDiscountCode(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("deactivateDiscountCode/{id}")]
    public async Task<IActionResult> DeactivateDiscountCode(int id)
    {
        try
        {
            var result = await _adminAppService.DeactivateDiscountCode(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
}
