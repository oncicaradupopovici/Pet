﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBB.Core.Abstractions;
using NBB.Data.EntityFramework;
using Pet.Banking.Data.Repositories;
using Pet.Banking.Domain.BankTransferAggregate;
using Pet.Banking.Domain.CashWithdrawalAggregate;
using Pet.Banking.Domain.DirectDebitPaymentAggregate;
using Pet.Banking.Domain.PosPaymentAggregate;
using Pet.Tenant.Abstractions;

namespace Pet.Banking.Data
{
    public static class DependencyInjectionExtensions
    {
        public static void AddBankingDataAccess(this IServiceCollection services)
        {
            services.AddEntityFrameworkDataAccess();

            services.AddEfCrudRepository<PosPayment, BankingDbContext>();
            services.AddScoped<IPosPaymentRepository, PosPaymentRepository>();

            services.AddEfCrudRepository<DirectDebitPayment, BankingDbContext>();
            services.AddScoped<IDirectDebitPaymentRepository, DirectDebitPaymentRepository>();

            services.AddScoped<IUow<BankTransfer>, EfUow<BankTransfer, BankingDbContext>>();
            services.AddScoped<IBankTransferRepository, BankTransferRepository>();

            services.AddScoped<IUow<CashWithdrawal>, EfUow<CashWithdrawal, BankingDbContext>>();
            services.AddScoped<ICashWithdrawalRepository, CashWithdrawalRepository>();


            services.AddEntityFrameworkSqlServer()
                .AddDbContext<BankingDbContext>(
                    (serviceProvider, options) =>
                    {
                        var configuration = serviceProvider.GetService<ITenantConfiguration>();
                        var connectionString = configuration.GetConnectionString();
                        options.UseSqlServer(connectionString, builder => { builder.EnableRetryOnFailure(3); });
                    });


        }
    }
}
