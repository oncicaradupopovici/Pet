import { get } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseRecipientSchema } from './schemas';
import { updateEntities } from './Entities';
import { combineReducers } from 'redux';
import { ExpenseRecipientAdded } from './serverEvents';

const EXPENSE_RECIPENT_LIST_LOADING = 'EXPENSE_RECIPENT_LIST_LOADING';
const EXPENSE_RECIPENT_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_RECIPENT_LIST_LOADED_SUCCESSFULLY';


const initialState = {
  list: {
    values: [],
    loading: false,
    loaded: false,
  }
};

export const actionCreators = {
  loadExpenseRecipientList: () => async (dispatch, getState) => {
    const { loading, loaded } = getState().expenseRecipient.list;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_RECIPENT_LIST_LOADING });
    const expenseRecipients = await get('api/expenseRecipients');
    const { entities, result } = Object.assign({}, normalize(expenseRecipients, [expenseRecipientSchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_RECIPENT_LIST_LOADED_SUCCESSFULLY, payload: { values: result } });
  }

};

const listReducer = (state, action) => {
  state = state || initialState.list;

  if (action.type === EXPENSE_RECIPENT_LIST_LOADING) {
    return {
      ...state,
      values: [],
      loading: true,
      loaded: false
    };
  }

  if (action.type === EXPENSE_RECIPENT_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      values: action.payload.values,
      loading: false,
      loaded: true
    };
  }

  if (action.type === ExpenseRecipientAdded) {
    return {
      ...state,
      loaded: false
    };
  }

  return state;
};

export const reducer = combineReducers({
  list: listReducer
});

export const selectors = {
  expenseRecipientList: state => denormalize(state.expenseRecipient.list.values, [expenseRecipientSchema], state.entities),
  expenseRecipientListLoaded: state => state.expenseRecipient.list.loaded
}
