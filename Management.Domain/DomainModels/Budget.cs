using System;
using Management.Domain.DomainModels.Users;

namespace Management.Domain.DomainModels
{
    public class Budget
    {
        public double MoneyEstimate { get;} //Should not be able to change the estimate too easily
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AdministratitiveDirector ApprovedBy { get; set; } //Should only be set at approval 
        public DateTime ApprovalDateTime { get; set; }

        public Budget(double moneyEstimate, DateTime startDate, DateTime endDate)
        {
            MoneyEstimate = moneyEstimate;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Approve(AdministratitiveDirector approvedBy) //Approval class?
        {
            ApprovedBy = approvedBy;
            ApprovalDateTime = DateTime.Now;
        }

        
    }
}