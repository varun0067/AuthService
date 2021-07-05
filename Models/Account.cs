using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationMS.Models
{
    public class Account
    {
        [Key]
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public long AccountNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string GuardianType { get; set; }
        [Required]
        public string GuardianName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Citizenship { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public long ContactNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string CitizenStatus { get; set; }
        [Required]
        public long InitialDepositAmount { get; set; }
        [Required]
        public string IdentificationType { get; set; }
        [Required]
        public string IdentificationDocumentNumber { get; set; }
        [Required]
        public string ReferenceAccountHolderName { get; set; }
        [Required]
        public long ReferenceAccountHolderAccountNumber { get; set; }
        [Required]
        public string ReferenceAccountHolderAddress { get; set; }
    }
}

