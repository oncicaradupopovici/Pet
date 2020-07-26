using NBB.Domain;
using System;

namespace Pet.ExpenseTracking.Domain.IncomeAggregate
{
    public class Income : EventedAggregateRoot<Guid>
    {
        public Guid IncomeId { get; private set; }
        public IncomeType IncomeType { get; private set; }
        public decimal Value { get; private set; }
        public DateTime IncomeDate { get; private set; }
        public int IncomeMonth { get; private set; }
        public string From { get; private set; }
        public string Details1 { get; private set; }
        public string Details2 { get; private set; }
        public Guid? SourceId { get; private set; }

        private Income()
        {
        }

        internal Income(IncomeType incomeType, decimal value, DateTime incomeDate, int incomeMonth, string from, string details1, string details2, Guid? sourceId)
        {
            IncomeId = Guid.NewGuid();
            IncomeType = incomeType;
            Value = value;
            IncomeDate = incomeDate;
            IncomeMonth = incomeMonth;
            From = from;
            Details1 = details1;
            Details2 = details2;
            SourceId = sourceId;
        }

        public override Guid GetIdentityValue() => IncomeId;
    }
}
