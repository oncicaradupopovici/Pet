import { get } from "../api/axiosApi";
import { normalize, denormalize } from 'normalizr';
import { expenseCategorySchema } from './schemas';
import { updateEntities } from './Entities';
import { combineReducers } from 'redux';
import { ExpenseCategoryAdded } from './serverEvents';

const EXPENSE_CATEGORY_LIST_LOADING = 'EXPENSE_CATEGORY_LIST_LOADING';
const EXPENSE_CATEGORY_LIST_LOADED_SUCCESSFULLY = 'EXPENSE_CATEGORY_LIST_LOADED_SUCCESSFULLY';


const initialState = {
  list: {
    values: [],
    loading: false,
    loaded: false,
  }
};

export const actionCreators = {
  loadExpenseCategoryList: () => async (dispatch, getState) => {
    const { loading, loaded } = getState().expenseCategory.list;
    if (loaded || loading) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: EXPENSE_CATEGORY_LIST_LOADING });
    const expenseCategories = await get('api/expenseCategories');
    const { entities, result } = Object.assign({}, normalize(expenseCategories, [expenseCategorySchema]));
    dispatch(updateEntities(entities));
    dispatch({ type: EXPENSE_CATEGORY_LIST_LOADED_SUCCESSFULLY, payload: { values: result } });
  }

};

const listReducer = (state, action) => {
  state = state || initialState.list;

  if (action.type === EXPENSE_CATEGORY_LIST_LOADING) {
    return {
      ...state,
      values: [],
      loading: true,
      loaded: false
    };
  }

  if (action.type === EXPENSE_CATEGORY_LIST_LOADED_SUCCESSFULLY) {
    return {
      ...state,
      values: action.payload.values,
      loading: false,
      loaded: true
    };
  }

  if (action.type === ExpenseCategoryAdded) {
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
  expenseCategoryList: state => denormalize(state.expenseCategory.list.values, [expenseCategorySchema], state.entities),
  expenseCategoryListLoaded: state => state.expenseCategory.list.loaded
}
