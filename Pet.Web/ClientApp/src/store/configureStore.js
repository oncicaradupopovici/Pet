import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as Entities from './Entities';
import * as ReportUploads from './ReportUploads';
import * as Expense from './Expense';
import * as ExpenseByCategory from './ExpenseByCategory';
import * as ExpenseByRecipient from './ExpenseByRecipient';
import * as ExpenseMonth from './ExpenseMonth';
import * as ExpenseRecipient from './ExpenseRecipient';
import * as ExpenseCategory from './ExpenseCategory';

export default function configureStore (history, initialState) {
  const reducers = {
    entities: Entities.reducer,
    reportUploads: ReportUploads.reducer,
    expense: Expense.reducer,
    expenseMonth: ExpenseMonth.reducer,
    expenseRecipient: ExpenseRecipient.reducer,
    expenseCategory: ExpenseCategory.reducer,
    expenseByCategory: ExpenseByCategory.reducer,
    expenseByRecipient: ExpenseByRecipient.reducer,
  };

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
