namespace Pet.ExpenseTracking.Domain.ExpenseAggregate
{
    public enum ExpenseType
    {
        PosPayment = 1,
        CashWithdrawal = 2,
        BankTransfer = 3,
        DirectDebitPayment = 4,
        CashExpense = 5,
        OpenBankingPayment = 6
    }
}
