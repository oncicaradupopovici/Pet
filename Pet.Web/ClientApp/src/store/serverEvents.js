export const ExpenseRecipientPosTerminalMatchPatternChanged = 'ExpenseRecipientPosTerminalMatchPatternChanged';
export const PosTerminalExpenseRecipientSet = 'PosTerminalExpenseRecipientSet';
export const ExpenseAdded = "ExpenseAdded";
export const ExpenseRecipientChanged = "ExpenseRecipientChanged";
export const ExpenseCategoryChanged = "ExpenseCategoryChanged";
export const ExpenseDeleted = "ExpenseDeleted";
export const ExpenseRecipientAdded = "ExpenseRecipientAdded";
export const ExpenseCategoryAdded = "ExpenseCategoryAdded";

export function dispatchServerEvents(dispatch, events) {
    for (const { eventType, payload } of events) {
        dispatch({ type: eventType, payload });
    }
}
