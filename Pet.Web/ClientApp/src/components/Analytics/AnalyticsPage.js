import React, { Component } from 'react';
import { connect } from 'react-redux';
import * as expenseMonth from '../../store/ExpenseMonth';
import CurrentExpenseMonthSelector from '../CurrentExpenseMonthSelector';
import Analytics from './Analytics';
import { Container, Row, Col } from 'reactstrap';


class AnalyticsPage extends Component {
  render() {
    const { currentExpenseMonth } = this.props;
    return (
      <div>
        <h1>Analytics</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        <Container>
          <Row className={"mb-2"}>
            <Col><CurrentExpenseMonthSelector /></Col>
          </Row>
          <Row>
            <Col><Analytics expenseMonthId={currentExpenseMonth.expenseMonthId} /></Col>
          </Row>
        </Container>
      </div>
    );
  }
}

export default connect(
  state => ({
    currentExpenseMonth: expenseMonth.selectors.currentExpenseMonth(state),
  })
)(AnalyticsPage);
