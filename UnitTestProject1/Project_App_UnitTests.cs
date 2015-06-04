using System;
using NUnit.Framework;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using App;
using App.Interfaces;

namespace Project_App_UnitTests
{
    [TestFixture]
    public class Project_App_UnitTests
    {
        #region "Class Tests"
        [Test]
        public void CustomerService_Test()
        {
           var mockcompanyrep = new MockCompanyRepository();
           var mockda = new MockCustomerDataAccess();

           var customerservice = new CustomerService(mockcompanyrep, mockda);

           Company getcompany = customerservice.GetById(11);

           Assert.IsInstanceOf<Company>(getcompany);
          
        }

        #endregion

        #region "Helper Tests"
        [Test]
        public void ValidateName_Test()
        {
            //should really only have a single assert
            //but for the sake of time in test 
            //no separated

            var testfunction = new CustomerService();

            string firstname;
            string surname;
            
            firstname = "Bernie";
            surname = "Cresswell";

            Assert.IsTrue(testfunction.ValidateName(firstname, surname));

            firstname = "";
            surname = "Cresswell";

            Assert.IsFalse(testfunction.ValidateName(firstname, surname));

            firstname = "Bernie";
            surname = null;

            Assert.IsFalse(testfunction.ValidateName(firstname, surname));

        }

        [Test]
        public void ValidateEmail_Test()
        {
            var testfunction = new CustomerService();

            string email;

            email = "B.cresswell@btinternet.com";

            Assert.IsTrue(testfunction.ValidateEmail(email));

            email = "Bcresswell@btinternetcom";

            Assert.IsFalse(testfunction.ValidateEmail(email));

        }

        [Test]
        public void ValidateDoB_Test()
        {
            var testfunction = new CustomerService();

            DateTime DoB;

            DoB =  new DateTime(1958, 04, 28);

            Assert.IsTrue(testfunction.ValidateDoB(DoB));

            DoB = new DateTime(1994, 04, 28);

            Assert.IsFalse(testfunction.ValidateDoB(DoB));

        }

         [Test]
        public void ValidateCreditLimit_Test()
        {
            var testfunction = new CustomerService();
            var customer = new Customer();
            customer.HasCreditLimit = true;
            customer.CreditLimit = 499;

            Assert.IsFalse(testfunction.ValidateCreditLimit(customer));

            customer.HasCreditLimit = false;
            customer.CreditLimit = 499;

            Assert.IsTrue(testfunction.ValidateCreditLimit(customer));

        }



        
    #endregion

        #region "Mocks"
        private class MockCompanyRepository:ICompanyRepository
        {
            public Company GetById(int id)
            {
                var company = new Company();
                company.Classification = Classification.Bronze;
                company.Id = id;
                company.Name = "Bernies ACME ltd";
                return company;
            }


        }

        private class MockCustomerDataAccess:ICustomerDataAccess
        {
            public void AddCustomer(Customer customer)
            {
                //customer added
            }

        }

        #endregion
    }
}
