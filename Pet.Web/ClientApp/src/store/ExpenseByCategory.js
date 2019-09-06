import { get } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseByCategorySchema } from './schemas';
import { updateEntities } from './Entities';
import { ExpenseAdded, ExpenseCategoryChanged } from './serverEvents';

const EXPENSE_BY_CATEGORY_LIST_LOADING = 'EXPENSE_BY_CATEGORY_LIST_LOADING';
const EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY';
const initialState = {};
const emptyExpenseMonth = { list: [], loading: false, loaded: false };

export const actionCreators = {
  loadExpenseByCategoryList: (expenseMonthId) => async (dispatch, getState) => {
    if (expenseMonthId === null) {
      return;
    }

    const { loading, loaded } = getState().expenseByCategory[expenseMonthId] || emptyExpenseMonth;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_BY_CATEGORY_LIST_LOADING, payload: { expenseMonthId } });
    const analytics = await get(`api/analytics/bycategory?expenseMonthId=${expenseMonthId}`);
    const { entities, result } = Object.assign({}, normalize(analytics, [expenseByCategorySchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY, payload: { expenseMonthId, list: result } });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === EXPENSE_BY_CATEGORY_LIST_LOADING) {
    return {
      ...state,
      [action.payload.expenseMonthId]: {
        ...state[action.payload.expenseMonthId],
        list: [],
        loading: true,
        loaded: false
      }
    };
  }

  if (action.type === EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      [action.payload.expenseMonthId]: {
        ...state[action.payload.expenseMonthId],
        list: action.payload.list,
        loading: false,
        loaded: true
      }
    };
  }

  if (action.type === ExpenseAdded || action.type === ExpenseCategoryChanged) {
    return {
      ...state,
      [action.payload.expenseMonth]: emptyExpenseMonth
    };
  }

  return state;
};

export const selectors = {
  expenseByCategoryList: (state, expenseMonthId) => denormalize((state.expenseByCategory[expenseMonthId] || emptyExpenseMonth).list, [expenseByCategorySchema], state.entities)
}
