﻿using MediatR;
using NBB.Data.Abstractions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pet.Banking.Domain.PosPaymentAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.ExpenseAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate;
using Pet.ExpenseTracking.Domain.ExpenseRecipientAggregate.DomainEvents;
using Pet.ExpenseTracking.Domain.Services;
using Pet.OpenBanking.Domain.OpenBankingPaymentAggregate.DomainEvents;

namespace Pet.Application.DomainOrchestration
{
    public class OpenBankingPaymentOrchestration :
        INotificationHandler<OpenBankingPaymentAdded>,
        INotificationHandler<OpenBankingMerchantAdded>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;
        private readonly IExpenseRecipientRepository _expenseRecipientRepository;
        private readonly ExpenseRecipientService _expenseRecipientService;

        public OpenBankingPaymentOrchestration(IExpenseRepository expenseRepository, ExpenseService expenseService, IExpenseRecipientRepository expenseRecipientRepository, ExpenseRecipientService expenseRecipientService)
        {
            _expenseRepository = expenseRepository;
            _expenseService = expenseService;
            _expenseRecipientRepository = expenseRecipientRepository;
            _expenseRecipientService = expenseRecipientService;
        }

        public async Task Handle(OpenBankingPaymentAdded notification, CancellationToken cancellationToken)
        {
            var expenseRecipient = await _expenseRecipientService.AddOpenBankingMerchantWhen(notification);
            if (expenseRecipient != null)
            {
                await _expenseRecipientRepository.SaveChangesAsync();
            }

            var expense = await _expenseService.CreateExpenseWhen(notification);
            if (expense != null)
            {
                await _expenseRepository.AddAsync(expense);
                await _expenseRepository.SaveChangesAsync();
            }
        }

        public async Task Handle(OpenBankingMerchantAdded notification, CancellationToken cancellationToken)
        {
            var expenses = await _expenseService.UpdateExpensesWhen(notification);
            await Task.WhenAll(expenses.Select(_expenseRepository.UpdateAsync));

            await _expenseRepository.SaveChangesAsync();
        }
    }
}