import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators, selectors } from '../../store/ExpenseByCategory';

class ExpenseByCategoryList extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.ensureDataFetched();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    this.ensureDataFetched();
  }

  ensureDataFetched() {
    this.props.loadExpenseByCategoryList(this.props.expenseMonthId);
  }

  render() {
    const { expenses } = this.props;
    return (
      <div>
        <table className='table'>
          <thead>
            <tr>
              <th>Category</th>
              <th>Value</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(expense =>
              <tr key={expense.id}>
                <td>{expense.expenseCategory && expense.expenseCategory.name}</td>
                <td>{expense.value}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }
}

export default connect(
  (state, ownProps) => ({
    expenses: selectors.expenseByCategoryList(state, ownProps.expenseMonthId)
  }),
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseByCategoryList);
