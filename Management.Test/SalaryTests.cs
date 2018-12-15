using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Management.Domain.DomainElements.BudgetPlanner;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;
using Management.Domain.Handlers;
using Management.Persistence.Model;
using NUnit.Framework;
using StructureMap.Diagnostics;


namespace Management.Test
{
    
    [TestFixture]
    public class SalaryTest
    {

        public Salary salery;
        public Shift Shift;
        
        
        [SetUp]
        public void Init()
        {
            // ARRANGE
            User newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.Email = "email@email.com";
            newUser.Name = "TestName";
            newUser.AccessLevel = UserRoles.Manager;
            newUser.BaseWage = 10;
            newUser.EmploymentDate = DateTime.Today.Subtract(TimeSpan.FromDays(740));
            
            // ARRANGE
            SalaryConfiguration conf = new SalaryConfiguration();
            List<DayOfWeek> days =  new List<DayOfWeek>();
            days.Add(DayOfWeek.Saturday);
            days.Add(DayOfWeek.Sunday);
            List<HourInfo> hours = new List<HourInfo>();
            hours.Add(new HourInfo(00,23));
            conf.UseQuarterTimeScheduling();
            
            // ARRANGE
            Shift shift = new Shift();
            shift.Id = Guid.NewGuid();
            shift.ShiftStart = new DateTime(2018, 12, 08, 10, 0, 0);
            shift.ShiftEnd = new DateTime(2018, 12, 08, 20, 0, 0);
           
            conf.AddSupplement(new SupplementInfo(Guid.NewGuid(), "Weekend", "Supplement in weekend", days, new Supplement(true, 20), hours));

            Shift = shift;
            salery = new Salary(newUser , conf);
        }
        
        /// <summary>
        /// Testing of the resolveSenoirty method
        /// </summary>
        [Test]
        public void TestSenoirty()
        {
            // ACT
            var result = salery.ResolvePaymentsForShift(Shift).Seniority;
            
            // ASSERT
            Assert.AreEqual(2, result);  
        }

        /// <summary>
        /// Testring of the ResolveWorksHours method
        /// </summary>
        [Test]
        public void TestOfSortedHours()
        {
            // ACT
            var result = salery.ResolvePaymentsForShift(Shift).SortedWorkHours;
            
            // ASSERT
            Assert.AreEqual(10, result.Hours);
            
        }

        /// <summary>
        /// Testing of the resolveTotalPayment method
        /// </summary>
        [Test]
        public void TestOfTotalpayment()
        {
            // ACT
            var result = salery.ResolvePaymentsForShift(Shift).TotalPayment;
            
            // ASSERT
            Assert.AreEqual(302, result);
        }

    }
}