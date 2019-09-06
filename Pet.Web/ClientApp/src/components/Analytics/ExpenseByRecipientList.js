import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators, selectors } from '../../store/ExpenseByRecipient';

class ExpenseByRecipientList extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.ensureDataFetched();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    this.ensureDataFetched();
  }

  ensureDataFetched() {
    this.props.loadExpenseByRecipientList(this.props.expenseMonthId);
  }

  render() {
    const { expenses } = this.props;
    return (
      <div>
        <table className='table'>
          <thead>
            <tr>
              <th>Recipient</th>
              <th>Value</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(expense =>
              <tr key={expense.id}>
                <td>{expense.expenseRecipient && expense.expenseRecipient.name}</td>
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
    expenses: selectors.expenseByRecipientList(state, ownProps.expenseMonthId)
  }),
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseByRecipientList);
