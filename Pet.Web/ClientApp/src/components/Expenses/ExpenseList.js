import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
//import { Link } from 'react-router-dom';
import * as expense from '../../store/Expense';
import ExpenseRecipientModal from './ExpenseRecipientModal';
import ExpenseCategoryModal from './ExpenseCategoryModal';
import MoveToSavingsModal from './MoveToSavingsModal';
import AddCashExpenseModal from './AddCashExpenseModal';
import { Button } from 'reactstrap';
import moment from 'moment';

function getExpenseTypeName(expense) {
  switch (expense.expenseType) {
    case 1: return 'Pos payment'
    case 2: return 'Cash withdrawal'
    case 3: return 'Bank transfer'
    case 4: return 'Direct debit payment'
    case 5: return 'Cash expense'
    case 6: return 'Finq Payment'
    default: return 'Unknown'
  }
}

function isCashWithdrawal(expense) {
  return expense.expenseType === 2;
}

function isBankTransfer(expense) {
  return expense.expenseType === 3;
}

function isOpenBankingPayment(expense){
  return expense.expenseType === 6;
}

function canMoveToSavings(expense){
  return (isBankTransfer(expense) && expense.expenseRecipient === null) || 
  (isOpenBankingPayment(expense) && expense.sourceCategory !== null)
}

class ExpenseList extends Component {
  state = {
    recipientModalOpen: false,
    categoryModalOpen: false,
    moveToSavingsModalOpen: false,
    addCashExpenseModalOpen: false,
    expenseInEdit: null
  }

  componentDidMount() {
    // This method is called when the component is first added to the document
    this.ensureDataFetched();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    this.ensureDataFetched();
  }

  ensureDataFetched() {
    this.props.loadExpenseList(this.props.expenseMonthId);
  }

  showRecipientModal = expense => () => {
    this.setState({ expenseInEdit: expense, recipientModalOpen: true })
  }

  showCategoryModal = expense => () => {
    this.setState({ expenseInEdit: expense, categoryModalOpen: true })
  }

  showMoveToSavingsModal = expense => () => {
    this.setState({ expenseInEdit: expense, moveToSavingsModalOpen: true })
  }

  toggleRecipientModal = () => {
    this.setState(prevState => ({ recipientModalOpen: !prevState.recipientModalOpen }));
  }

  toggleCategoryModal = () => {
    this.setState(prevState => ({ categoryModalOpen: !prevState.categoryModalOpen }));
  }

  toggleMoveToSavingsModal = () => {
    this.setState(prevState => ({ moveToSavingsModalOpen: !prevState.moveToSavingsModalOpen }));
  }

  toggleAddCashExpenseModal = () => {
    this.setState(prevState => ({ addCashExpenseModalOpen: !prevState.addCashExpenseModalOpen }));
  }



  render() {
    const { expenses } = this.props;
    const { recipientModalOpen, categoryModalOpen, moveToSavingsModalOpen, addCashExpenseModalOpen, expenseInEdit } = this.state;
    return (
      <div>
        <ExpenseRecipientModal isOpen={recipientModalOpen} toggle={this.toggleRecipientModal} expense={expenseInEdit} />
        <ExpenseCategoryModal isOpen={categoryModalOpen} toggle={this.toggleCategoryModal} expense={expenseInEdit} />
        <MoveToSavingsModal isOpen={moveToSavingsModalOpen} toggle={this.toggleMoveToSavingsModal} expense={expenseInEdit} />
        <AddCashExpenseModal isOpen={addCashExpenseModalOpen} toggle={this.toggleAddCashExpenseModal} expense={expenseInEdit} />

        <div><Button color="primary" onClick={this.toggleAddCashExpenseModal}>Add cash expense</Button></div>

        <table className='table'>
          <thead>
            <tr>
              <th>Date</th>
              <th>Source</th>
              <th>Recipient</th>
              <th>Category</th>
              <th>Value</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map(expense =>
              <tr key={expense.expenseId}>
                <td>{moment(expense.expenseDate).format("MMM Do")}</td>
                <td>
                  <div>
                    <p className="text-muted">{getExpenseTypeName(expense)}</p>
                    <p className="text-success">{expense.details1}</p>
                    <p className="text-success">{expense.details2}</p>
                  </div>
                </td>
                <td>{expense.expenseRecipient
                  ? <Button color="link" onClick={this.showRecipientModal(expense)}>{expense.expenseRecipient.name}</Button>
                  : (!isCashWithdrawal(expense) && <Button color="primary" onClick={this.showRecipientModal(expense)}>Assign</Button>)}</td>
                <td>{expense.expenseCategory
                  ? <Button color="link" onClick={this.showCategoryModal(expense)}>{expense.expenseCategory.name}</Button>
                  : <Button color="primary" onClick={this.showCategoryModal(expense)}>Assign</Button>}
                </td>
                <td>{expense.value}</td>
                <td>{canMoveToSavings(expense) && <Button color="primary" onClick={this.showMoveToSavingsModal(expense)}>To savings</Button>}</td>
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
    expenses: expense.selectors.expenseList(state, ownProps.expenseMonthId),
  }),
  dispatch => bindActionCreators(expense.actionCreators, dispatch)
)(ExpenseList);
