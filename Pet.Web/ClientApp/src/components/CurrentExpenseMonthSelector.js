import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
//import { Link } from 'react-router-dom';
import * as expenseMonth from '../store/ExpenseMonth';
import Select from 'react-select';
import { Container, Row, Col } from 'reactstrap';

class CurrentExpenseMonthSelector extends Component {
  componentDidMount() {
    // This method is called when the component is first added to the document
    this.ensureDataFetched();
  }

  componentDidUpdate() {
    // This method is called when the route parameters change
    this.ensureDataFetched();
  }

  ensureDataFetched() {
    this.props.loadExpenseMonthList();
    this.props.loadCurrentExpenseMonth();
  }

  handleExpenseMonthChange = (selectedExpenseMonth) => {
    this.props.setCurrentExpenseMonth(selectedExpenseMonth);
  }

  render() {
    const { expenseMonths, currentExpenseMonth } = this.props;
    return (
      <div>
        <Container>
          <Row>
            <Col xs="6"><Select options={expenseMonths} getOptionLabel={option => option.name} getOptionValue={option => option.expenseMonthId} value={currentExpenseMonth} onChange={this.handleExpenseMonthChange} /></Col>
            <Col xs="2">Expenses: <p>lei{currentExpenseMonth.totalExpenses}</p></Col>
            <Col xs="2">Savings: <p>lei{currentExpenseMonth.totalSavings}</p></Col>
          </Row>
        </Container>
        
      </div>
    );
  }
}

export default connect(
  state => ({
    expenseMonths: expenseMonth.selectors.expenseMonthList(state),
    currentExpenseMonth: expenseMonth.selectors.currentExpenseMonth(state),
  }),
  dispatch => bindActionCreators(expenseMonth.actionCreators, dispatch)
)(CurrentExpenseMonthSelector);
