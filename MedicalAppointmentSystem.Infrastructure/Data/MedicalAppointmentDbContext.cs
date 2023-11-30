using MedicalAppointmentSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Infrastructure.Data
{
    public class MedicalAppointmentDbContext : DbContext
    {
        public MedicalAppointmentDbContext(DbContextOptions<MedicalAppointmentDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships

            // Admin - Doctor
            modelBuilder.Entity<Admin>()
                .HasMany(admin => admin.Doctors)
                .WithOne(doctor => doctor.Admin)
                .HasForeignKey(doctor => doctor.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor - Appointment
            modelBuilder.Entity<Doctor>()
                .HasMany(doctor => doctor.Appointments)
                .WithOne(appointment => appointment.Doctor)
                .HasForeignKey(appointment => appointment.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor - Feedback
            modelBuilder.Entity<Doctor>()
                .HasMany(doctor => doctor.Feedbacks)
                .WithOne(feedback => feedback.Doctor)
                .HasForeignKey(feedback => feedback.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient - Appointment
            modelBuilder.Entity<Patient>()
                .HasMany(patient => patient.Appointments)
                .WithOne(appointment => appointment.Patient)
                .HasForeignKey(appointment => appointment.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient - Feedback
            modelBuilder.Entity<Patient>()
                .HasMany(patient => patient.Feedbacks)
                .WithOne(feedback => feedback.Patient)
                .HasForeignKey(feedback => feedback.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Specialization - Doctor
            modelBuilder.Entity<Doctor>()
                .HasOne(doctor => doctor.Specialization)
                .WithMany(specialization => specialization.Doctors)
                .HasForeignKey(doctor => doctor.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Admin - DiscountCode
            modelBuilder.Entity<Admin>()
                .HasMany(admin => admin.DiscountCodes)
                .WithOne(discountCode => discountCode.Admin)
                .HasForeignKey(discountCode => discountCode.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient - DiscountCode
            modelBuilder.Entity<Patient>()
                .HasMany(patient => patient.DiscountCodes)
                .WithOne(discountCode => discountCode.Patient)
                .HasForeignKey(discountCode => discountCode.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // DiscountCode - Appointment
            modelBuilder.Entity<DiscountCode>()
                .HasMany(discountCode => discountCode.Appointments)
                .WithOne(appointment => appointment.DiscountCode)
                .HasForeignKey(appointment => appointment.DiscountCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
