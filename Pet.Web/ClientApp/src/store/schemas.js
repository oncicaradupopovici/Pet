import { schema } from 'normalizr';

export const expenseRecipientSchema = new schema.Entity('expenseRecipients', {}, { idAttribute: x => x.expenseRecipientId });
export const expenseCategorySchema = new schema.Entity('expenseCategories', {}, { idAttribute: x => x.expenseCategoryId });
export const expenseSchema = new schema.Entity('expenses', { expenseCategory: expenseCategorySchema, expenseRecipient: expenseRecipientSchema }, { idAttribute: x => x.expenseId });
export const expenseByCategorySchema = new schema.Entity('expensesByCategory', { expenseCategory: expenseCategorySchema }, { idAttribute: x => x.id });
export const expenseByRecipientSchema = new schema.Entity('expensesByRecipint', { expenseRecipient: expenseRecipientSchema }, { idAttribute: x => x.id });
export const expenseMonthSchema = new schema.Entity('expenseMonths', {  }, { idAttribute: x => x.expenseMonthId });
