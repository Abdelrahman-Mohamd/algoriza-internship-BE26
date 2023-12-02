using Microsoft.AspNetCore.Mvc;
using MedicalAppointmentSystem.Application.DTOs;
using MedicalAppointmentSystem.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/doctor")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorAppService _doctorAppService;

    public DoctorController(IDoctorAppService doctorAppService)
    {
        _doctorAppService = doctorAppService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            var result = await _doctorAppService.Login(loginDTO.Email, loginDTO.Password);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getAllAppointments")]
    public async Task<IActionResult> GetAllAppointments(int doctorID, string searchBy, int pageSize, int pageNumber)
    {
        try
        {
            var result = await _doctorAppService.GetAllAppointments(doctorID, searchBy, pageSize, pageNumber);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("confirmCheckUp")]
    public async Task<IActionResult> ConfirmCheckUp([FromBody] int bookingId)
    {
        try
        {
            var result = await _doctorAppService.ConfirmCheckUp(bookingId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("addAppointment")]
    public async Task<IActionResult> AddAppointment([FromBody] AppointmentRequestDTO appointmentRequestDTO)
    {
        try
        {
            var result = await _doctorAppService.AddAppointment(appointmentRequestDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("updateAppointment")]
    public async Task<IActionResult> UpdateAppointment(int doctorId, int timeSlotId)
    {
        try
        {
            var result = await _doctorAppService.UpdateAppointment(doctorId, timeSlotId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("deleteAppointment")]
    public async Task<IActionResult> DeleteAppointment(int doctorId, int timeSlotId)
    {
        try
        {
            var result = await _doctorAppService.DeleteAppointment(doctorId, timeSlotId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
}
