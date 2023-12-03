using MedicalAppointmentSystem.Core.Entities;
using MedicalAppointmentSystem.Core.Enums;
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

            // Seed Admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    UserName = "Admin_Ahmed",
                    PasswordHash = "sfx4567rr",
                    FirstName = "Ahmed",
                    LastName = "Mohamed",
                    Email = "Ahmed45@yahoo.com",
                    Phone = "1234567890",
                    Gender = Gender.Male, 
                    DateOfBirth = new DateTime(1990, 1, 1),
                }
            );

            // Seed Doctor
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    PasswordHash = "sfr45456rtwq",
                    ImageLink = "doctor.jpg",
                    FirstName = "Adham",
                    LastName = "Montaser",
                    IsActive = true,
                    Email = "Adham7878@yahoo.com",
                    Phone = "9876543210",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1985, 5, 10),
                    AdminId = 1,
                    SpecializationId = 1,
                }
            );

            // Seed Patient
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    ImageLink = "patient.jpg",
                    PasswordHash = "ioug45679gh",
                    FullName = "Aya Mohamed",
                    Email = "AyaMohamed@example.com",
                    Phone = "1234567890",
                    IsActive = true,
                    Gender = Gender.Female,
                    DateOfBirth = new DateTime(1990, 8, 15),
                }
            );

            // Seed Specializations
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization
                {
                    Id = 1,
                    Name = "Cardiology",
                },
                new Specialization
                {
                    Id = 2,
                    Name = "Dermatology",
                },
                new Specialization
                {
                    Id = 3,
                    Name = "Orthopedics",
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
