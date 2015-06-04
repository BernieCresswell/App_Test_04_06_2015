using App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService:ICompanyRepository, ICustomerDataAccess
    {

        ICompanyRepository repository = null;
        ICustomerDataAccess daLayer = null;

        public CustomerService(ICompanyRepository _repository, ICustomerDataAccess _daLayer)  // inject the concrete repository class
        {
            this.repository = _repository;
            this.daLayer = _daLayer;
        }

        public CustomerService(){}  // to enable unit test only


        public Company GetById(int id)
        {
            return repository.GetById(id);
        }
        
        
        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {

            #region "Validation"
            if (!ValidateName(firstname, surname)) return false;
            if (!ValidateEmail(email)) return false;
            if (!ValidateDoB(dateOfBirth)) return false;
            #endregion

            //var companyRepository = new CompanyRepository();
            //now injected
            var company = GetById(companyId);

            var customer = new Customer
                               {
                                   Company = company,
                                   DateOfBirth = dateOfBirth,
                                   EmailAddress = email,
                                   Firstname = firstname,
                                   Surname = surname
                               };

            //company.Name seems a strange key for this 
            //classification seems more natural but as business logic unknown
            //leaving as is
            // but making a switch statement

            switch (company.Name)
            {
                case "VeryImportantClient":
                    customer.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    // Do credit check and double credit limit
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        customer.CreditLimit = creditLimit;
                    }
                    break;
                default:
                    // Do credit check
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                    break;
            }
     
             #region "Validation"
            if (!ValidateCreditLimit(customer)) return false;
             #endregion

            AddCustomer(customer);

            return true;
        }

        #region "Helper Functions"

        public bool ValidateName(string firstname, string surname)
        {
            return !(string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname));
        }

        public bool ValidateEmail(string email)
        {
            return (email.Contains("@") && email.Contains("."));
        }

        public bool ValidateDoB(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

                return age > 21;
        }

        public bool ValidateCreditLimit(Customer customer)
        {
            //running out of time so left as is
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }
            {
                return true;
            }
        }
        #endregion



        public void AddCustomer(Customer customer)
        {
            daLayer.AddCustomer(customer);
        }
    }

}
