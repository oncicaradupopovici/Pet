import { get } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseMonthSchema } from './schemas';
import { updateEntities } from './Entities';
import { combineReducers } from 'redux';
import { ExpenseAdded, ExpenseDeleted, SavingsTransactionAdded } from './serverEvents';

const EXPENSE_MONTH_LIST_LOADING = 'EXPENSE_MONTH_LIST_LOADING';
const EXPENSE_MONTH_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_MONTH_LIST_LOADED_SUCCESSFULLY';

const CURRENT_EXPENSE_MONTH_LOADING = 'CURRENT_EXPENSE_MONTH_LOADING';
const CURRENT_EXPENSE_MONTH_LOADED_SUCCESSFULLY = 'CURRENT_EXPENSE_MONTH_LOADED_SUCCESSFULLY';
const SET_CURRENT_EXPENSE_MONTH = 'SET_CURRENT_EXPENSE_MONTH';


const initialState = {
  list: {
    values: [],
    loading: false,
    loaded: false,
  },
  current: {
    expenseMonthId: null,
    loading: false,
    loaded: false
  }
};

export const actionCreators = {
  loadExpenseMonthList: () => async (dispatch, getState) => {
    const { loading, loaded } = getState().expenseMonth.list;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_MONTH_LIST_LOADING });
    const expenseMonths = await get('api/expenseMonths');
    const { entities, result } = Object.assign({}, normalize(expenseMonths, [expenseMonthSchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_MONTH_LIST_LOADED_SUCCESSFULLY, payload: { values: result } });
  },

  loadCurrentExpenseMonth: () => async (dispatch, getState) => {
    const { loading, loaded } = getState().expenseMonth.current;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: CURRENT_EXPENSE_MONTH_LOADING });
    const currentExpenseMonth = await get('api/expenseMonths/current');
    const { entities, result } = Object.assign({}, normalize(currentExpenseMonth, expenseMonthSchema));
    dispatch(updateEntities(entities));
    dispatch({ type: CURRENT_EXPENSE_MONTH_LOADED_SUCCESSFULLY, payload: { expenseMonthId: result } });
  },

  setCurrentExpenseMonth: expenseMonth => ({ type: SET_CURRENT_EXPENSE_MONTH, payload: { expenseMonth } })

};

const listReducer = (state, action) => {
  state = state || initialState.list;

  if (action.type === EXPENSE_MONTH_LIST_LOADING) {
    return {
      ...state,
      values: [],
      loading: true,
      loaded: false
    };
  }

  if (action.type === EXPENSE_MONTH_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      values: action.payload.values,
      loading: false,
      loaded: true
    };
  }

  return state;
};

const currentReducer = (state, action) => {
  state = state || initialState.current;

  if (action.type === CURRENT_EXPENSE_MONTH_LOADING) {
    return {
      ...state,
      expenseMonthId: null,
      loading: true,
      loaded: false
    };
  }

  if (action.type === CURRENT_EXPENSE_MONTH_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      expenseMonthId: action.payload.expenseMonthId,
      loading: false,
      loaded: true
    };
  }

  if (action.type === SET_CURRENT_EXPENSE_MONTH) {
    return {
      ...state,
      expenseMonthId: action.payload.expenseMonth.expenseMonthId,
      loading: false,
      loaded: true
    };
  }

  if ([ExpenseAdded, ExpenseDeleted, SavingsTransactionAdded].includes(action.type)) {
    return {
      ...state,
      loaded: false
    };
  }

  return state;
};

export const reducer = combineReducers({
  list: listReducer,
  current: currentReducer
});

export const selectors = {
  expenseMonthList: state => denormalize(state.expenseMonth.list.values, [expenseMonthSchema], state.entities),
  currentExpenseMonth: state => denormalize(state.expenseMonth.current.expenseMonthId, expenseMonthSchema, state.entities) || { expenseMonthId: null }
}
