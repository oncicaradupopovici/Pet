import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators, selectors } from '../../store/ExpenseByCategory';
import PieChart from './common/PieChart';

const ExpenseMonthByCategory = (props) => {
    const { expenses, loadExpenseByCategoryList, expenseMonthId } = props;
    useEffect(() => { loadExpenseByCategoryList(expenseMonthId) });

    const data = expenses.map(e => {
        const expenseCategory = e.expenseCategory || { expenseCategoryId: 0, name: '' };
        return { id: expenseCategory.name, label: expenseCategory.name, value: e.value };
    });
    console.log(JSON.stringify(data));
    return (<PieChart data={data} />);
}

export default connect(
    (state, ownProps) => ({ expenses: selectors.expenseByCategoryList(state, ownProps.expenseMonthId) }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseMonthByCategory);
