import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Container, Row, Col } from 'reactstrap';
import { actionCreators, selectors } from '../../store/ExpenseByCategory';
import * as expenseMonth from '../../store/ExpenseMonth';
import CurrentExpenseMonthSelector from '../CurrentExpenseMonthSelector';
import PieChart from './common/PieChart';

const ExpenseCategoryMonthPieChart = (props) => {
    const { expenses, loadExpenseByCategoryList, currentExpenseMonth } = props;
    useEffect(() => { loadExpenseByCategoryList(currentExpenseMonth.expenseMonthId) });
    const data = expenses.map(e => {
        const expenseCategory = e.expenseCategory || { expenseCategoryId: 0, name: '' };
        return { id: expenseCategory.name, label: expenseCategory.name, value: e.value };
    });
    console.log(JSON.stringify(data));
    return (
        <div>
            <Container>
                <Row className={"mb-2"}>
                    <Col><CurrentExpenseMonthSelector /></Col>
                </Row>
                <Row>
                    <Col><PieChart data={data} /> </Col>
                </Row>
            </Container>
        </div>
    );
}

export default connect(
    (state) => {
        const currentExpenseMonth = expenseMonth.selectors.currentExpenseMonth(state);
        return {
            currentExpenseMonth,
            expenses: selectors.expenseByCategoryList(state, currentExpenseMonth.expenseMonthId)
        }
    },
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseCategoryMonthPieChart);
