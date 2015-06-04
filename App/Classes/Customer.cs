using App.Interfaces;
using System;

namespace App
{
    public class Customer
    {

        //public ICompany company = null;

        //public Customer(ICompany _company)  // inject the concrete class
        //{
        //    this.company = _company;
        //}

        public Customer()
        {
            // TODO: Complete member initialization
        }

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Company Company { get; set; }
    }
}