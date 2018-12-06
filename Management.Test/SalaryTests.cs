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
    public class UnitTest1
    {

        public Salary salery;
        public Shift Shift;
        
        
        [SetUp]
        public void Init()
        {
            User newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.Email = "email@email.com";
            newUser.Name = "TestName";
            newUser.AccessLevel = UserRoles.Manager;
            newUser.BaseWage = 10;
            newUser.EmploymentDate = DateTime.Today.Subtract(TimeSpan.FromDays(740));
            
            
            SalaryConfiguration conf = new SalaryConfiguration();
            List<DayOfWeek> days =  new List<DayOfWeek>();
            days.Add((DayOfWeek.Thursday));
            days.Add(DayOfWeek.Friday);
            days.Add(DayOfWeek.Saturday);
            days.Add(DayOfWeek.Sunday);
            List<HourInfo> hours = new List<HourInfo>();
            hours.Add(new HourInfo(16,00));
            conf.UseQuarterTimeScheduling();
            
            
            Shift shift = new Shift();
            shift.Id = Guid.NewGuid();
            shift.ShiftStart = DateTime.Now;
            shift.ShiftEnd = DateTime.Now.AddHours(10);
           
            
            
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
            var result = salery.ResolvePaymentsForShift(Shift).Seniority;

            Assert.AreEqual(2, result);  
        }

        /// <summary>
        /// Testring of the ResolveWorksHours method
        /// </summary>
        [Test]
        public void TestOfSortedHours()
        {
            var result = salery.ResolvePaymentsForShift(Shift).SortedWorkHours;
            
            Assert.AreEqual(10, result.Hours);
            
        }

        /// <summary>
        /// Testing of the resolveTotalPayment method
        /// </summary>
        [Test]
        public void TestOfTotalpayment()
        {
            var result = salery.ResolvePaymentsForShift(Shift).TotalPayment;
            
            Assert.AreEqual(200, result);
        }

    }
}