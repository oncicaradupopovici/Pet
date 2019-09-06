import { get, post } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseSchema } from './schemas';
import { updateEntities } from './Entities';
import { ExpenseAdded, ExpenseRecipientChanged, ExpenseCategoryChanged, ExpenseDeleted } from './serverEvents';
import { combineReducers } from 'redux';
import { dispatchServerEvents } from './serverEvents';

const EXPENSE_LIST_LOADING = 'EXPENSE_LIST_LOADING';
const EXPENSE_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_LIST_LOADED_SUCCESSFULLY';

const EXPENSE_RECIPIENT_SAVING = 'EXPENSE_RECIPIENT_SAVING';
const EXPENSE_RECIPIENT_SAVED_SUCCESSFULLY = 'EXPENSE_RECIPIENT_SAVED_SUCCESSFULLY';

const EXPENSE_CATEGORY_SAVING = 'EXPENSE_CATEGORY_SAVING';
const EXPENSE_CATEGORY_SAVED_SUCCESSFULLY = 'EXPENSE_CATEGORY_SAVED_SUCCESSFULLY';

const EXPENSE_TO_SAVINGS_SAVING = 'EXPENSE_TO_SAVINGS_SAVING';
const EXPENSE_TO_SAVINGS_SAVED_SUCCESSFULLY = 'EXPENSE_TO_SAVINGS_SAVED_SUCCESSFULLY';

const NEW_CASH_EXPENSE_SAVING = 'NEW_CASH_EXPENSE_SAVING';
const NEW_CASH_EXPENSE_SAVED_SUCCESSFULLY = 'NEW_CASH_EXPENSE_SAVED_SUCCESSFULLY';

const initialState = {
  listByMonthId: {
  },
  recipient: {
    saving: false,
    saved: false
  },
  category: {
    saving: false,
    saved: false
  },
  toSavings: {
    saving: false,
    saved: false
  },
  newCashExpense: {
    saving: false,
    saved: false
  }
};
const emptyExpenseMonth = { values: [], loading: false, loaded: false };

export const actionCreators = {
  loadExpenseList: (expenseMonthId) => async (dispatch, getState) => {
    if (expenseMonthId === null) {
      return;
    }

    const { loading, loaded } = getState().expense.listByMonthId[expenseMonthId] || emptyExpenseMonth;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_LIST_LOADING, payload: { expenseMonthId } });
    const expenses = await get(`api/expenses?expenseMonthId=${expenseMonthId}`);
    const { entities, result } = Object.assign({}, normalize(expenses, [expenseSchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_LIST_LOADED_SUCCESSFULLY, payload: { expenseMonthId, list: result } });
  },

  saveRecipient: (expenseId, recipient) => async (dispatch, getState) => {
    const { saving } = getState().expense.recipient;
    if (saving) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_RECIPIENT_SAVING, payload: { expenseId, recipient } });
    var { events } = await post(`api/expenses/${expenseId}/recipient`, { expenseId, recipient });
    dispatchServerEvents(dispatch, events);

    dispatch({ type: EXPENSE_RECIPIENT_SAVED_SUCCESSFULLY });
  },

  saveCategory: (expenseId, category, justThisOne) => async (dispatch, getState) => {
    const { saving } = getState().expense.category;
    if (saving) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_CATEGORY_SAVING, payload: { expenseId, category } });
    var { events } = await post(`api/expenses/${expenseId}/category`, { expenseId, category, justThisOne });
    dispatchServerEvents(dispatch, events);

    dispatch({ type: EXPENSE_CATEGORY_SAVED_SUCCESSFULLY });
  },

  moveToSavings: (expenseId) => async (dispatch, getState) => {
    const { saving } = getState().expense.toSavings;
    if (saving) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_TO_SAVINGS_SAVING, payload: { expenseId } });
    var { events } = await post(`api/expenses/${expenseId}/toSavings`, { expenseId });
    dispatchServerEvents(dispatch, events);

    dispatch({ type: EXPENSE_TO_SAVINGS_SAVED_SUCCESSFULLY });
  },

  addCashExpense: (expense) => async (dispatch, getState) => {
    const { saving } = getState().expense.newCashExpense;
    if (saving) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: NEW_CASH_EXPENSE_SAVING, payload: { expense } });
    var { events } = await post(`api/expenses/cashExpense`, expense);
    dispatchServerEvents(dispatch, events);

    dispatch({ type: NEW_CASH_EXPENSE_SAVED_SUCCESSFULLY });
  }
};

const listByMonthIdReducer = (state, action) => {
  state = state || initialState.listByMonthId;

  if (action.type === EXPENSE_LIST_LOADING) {
    return {
      ...state,
      [action.payload.expenseMonthId]: {
        ...state[action.payload.expenseMonthId],
        values: [],
        loading: true,
        loaded: false
      }
    };
  }

  if (action.type === EXPENSE_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      [action.payload.expenseMonthId]: {
        ...state[action.payload.expenseMonthId],
        values: action.payload.list,
        loading: false,
        loaded: true
      }
    };
  }

  if ([ExpenseAdded, ExpenseRecipientChanged, ExpenseCategoryChanged, ExpenseDeleted].includes(action.type)) {
    return {
      ...state,
      [action.payload.expenseMonth]: emptyExpenseMonth
    };
  }

  return state;
};

const recipientReducer = (state, action) => {
  state = state || initialState.recipient;

  if (action.type === EXPENSE_RECIPIENT_SAVING) {
    return {
      ...state,
      saving: true,
      saved: false
    };
  }

  if (action.type === EXPENSE_RECIPIENT_SAVED_SUCCESSFULLY) {
    return {
      ...state,
      saving: false,
      saved: true
    };
  }

  return state;
};

const categoryReducer = (state, action) => {
  state = state || initialState.category;

  if (action.type === EXPENSE_CATEGORY_SAVING) {
    return {
      ...state,
      saving: true,
      saved: false
    };
  }

  if (action.type === EXPENSE_CATEGORY_SAVED_SUCCESSFULLY) {
    return {
      ...state,
      saving: false,
      saved: true
    };
  }

  return state;
};

const toSavingsReducer = (state, action) => {
  state = state || initialState.toSavings;

  if (action.type === EXPENSE_TO_SAVINGS_SAVING) {
    return {
      ...state,
      saving: true,
      saved: false
    };
  }

  if (action.type === EXPENSE_TO_SAVINGS_SAVED_SUCCESSFULLY) {
    return {
      ...state,
      saving: false,
      saved: true
    };
  }

  return state;
};

const newCashExpenseReducer = (state, action) => {
  state = state || initialState.newCashExpense;

  if (action.type === NEW_CASH_EXPENSE_SAVING) {
    return {
      ...state,
      saving: true,
      saved: false
    };
  }

  if (action.type === NEW_CASH_EXPENSE_SAVED_SUCCESSFULLY) {
    return {
      ...state,
      saving: false,
      saved: true
    };
  }

  return state;
};

export const reducer = combineReducers({
  listByMonthId: listByMonthIdReducer,
  recipient: recipientReducer,
  category: categoryReducer,
  toSavings: toSavingsReducer,
  newCashExpense: newCashExpenseReducer
});

export const selectors = {
  expenseList: (state, expenseMonthId) => denormalize((state.expense.listByMonthId[expenseMonthId] || emptyExpenseMonth).values, [expenseSchema], state.entities),
  recipient: (state) => state.expense.recipient,
  category: (state) => state.expense.category,
  toSavings: (state) => state.expense.toSavings,
  newCashExpense: (state)=> state.expense.newCashExpense
}
