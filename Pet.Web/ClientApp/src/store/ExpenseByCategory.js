import { combineReducers } from 'redux';
import { get } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseByCategorySchema } from './schemas';
import { updateEntities } from './Entities';
import { ExpenseAdded, ExpenseCategoryChanged } from './serverEvents';

const EXPENSE_BY_CATEGORY_LIST_LOADING = 'EXPENSE_BY_CATEGORY_LIST_LOADING';
const EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY';
const EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADING = 'EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADING';
const EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADED_SUCCESSFULLY';
const initialState = { byExpenseMonths: {}, byInterval:{} };
const emptyList = { list: [], loading: false, loaded: false };

const getIntervalKey = (_fromExpenseMonthId, _toExpenseMonthId) => 'ALL';

export const actionCreators = {
  loadExpenseByCategoryList: (expenseMonthId) => async (dispatch, getState) => {
    if (expenseMonthId === null) {
      return;
    }

    const { loading, loaded } = getState().expenseByCategory.byExpenseMonths[expenseMonthId] || emptyList;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_BY_CATEGORY_LIST_LOADING, payload: { expenseMonthId } });
    const analytics = await get(`api/analytics/bycategory?expenseMonthId=${expenseMonthId}`);
    const { entities, result } = Object.assign({}, normalize(analytics, [expenseByCategorySchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_BY_CATEGORY_LIST_LOADED_SUCCESSFULLY, payload: { expenseMonthId, list: result } });
  },

  loadExpenseByCategoryInRangeList: (fromExpenseMonthId, toExpenseMonthId) => async (dispatch, getState) => {
    const intervalKey = getIntervalKey(fromExpenseMonthId, toExpenseMonthId);

    const { loading, loaded } = getState().expenseByCategory.byInterval[intervalKey] || emptyList;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADING, payload: { intervalKey  } });
    const analytics = await get(`api/analytics/bycategory/range?fromExpenseMonthId=${fromExpenseMonthId}&toExpenseMonthId=${toExpenseMonthId}`);
    const { entities, result } = Object.assign({}, normalize(analytics, [expenseByCategorySchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADED_SUCCESSFULLY, payload: { intervalKey, list: result } });
  }
}



const byExpenseMonthsReducer = (state, action) => {
  state = state || initialState.byExpenseMonths;

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
      [action.payload.expenseMonth]: emptyList
    };
  }

  return state;
};

const byIntervalReducer = (state, action) => {
  state = state || initialState.byInterval;

  if (action.type === EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADING) {
    return {
      ...state,
      [action.payload.intervalKey]: {
        ...state[action.payload.intervalKey],
        list: [],
        loading: true,
        loaded: false
      }
    };
  }

  if (action.type === EXPENSE_BY_CATEGORY_IN_RANGE_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      [action.payload.intervalKey]: {
        ...state[action.payload.intervalKey],
        list: action.payload.list,
        loading: false,
        loaded: true
      }
    };
  }

  if (action.type === ExpenseAdded || action.type === ExpenseCategoryChanged) {
    return initialState.byInterval;
  }

  return state;
};

export const reducer = combineReducers({
  byExpenseMonths: byExpenseMonthsReducer,
  byInterval: byIntervalReducer
});

export const selectors = {
  expenseByCategoryList: (state, expenseMonthId) => denormalize((state.expenseByCategory.byExpenseMonths[expenseMonthId] || emptyList).list, [expenseByCategorySchema], state.entities),
  expenseByCategoryInRangeList: (state, fromExpenseMonthId, toExpenseMonthId) => denormalize((state.expenseByCategory.byInterval[getIntervalKey(fromExpenseMonthId, toExpenseMonthId)] || emptyList).list, [expenseByCategorySchema], state.entities)
}
