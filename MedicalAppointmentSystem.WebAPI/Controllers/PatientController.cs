using Microsoft.AspNetCore.Mvc;
using MedicalAppointmentSystem.Application.DTOs;
using MedicalAppointmentSystem.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/patient")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientAppService _patientAppService;

    public PatientController(IPatientAppService patientAppService)
    {
        _patientAppService = patientAppService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PatientRegistrationDTO patientRegistrationDTO)
    {
        try
        {
            var result = await _patientAppService.Register(patientRegistrationDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            var result = await _patientAppService.Login(loginDTO.Email, loginDTO.Password);
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
            var result = await _patientAppService.GetAllDoctors(page, pageSize, search);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("bookAppointment")]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentBookingDTO appointmentBookingDTO)
    {
        try
        {
            var result = await _patientAppService.BookAppointment(appointmentBookingDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getAllBookings")]
    public async Task<IActionResult> GetAllBookings(int patientId)
    {
        try
        {
            var result = await _patientAppService.GetAllBookings(patientId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("cancelBooking")]
    public async Task<IActionResult> CancelBooking([FromBody] int bookingId, int patientId)
    {
        try
        {
            var result = await _patientAppService.CancelBooking(bookingId, patientId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }
}
