import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators, selectors } from '../../store/ExpenseByRecipient';
import PieChart from './common/PieChart';

const ExpenseMonthByRecipient = (props) => {
    const { expenses, loadExpenseByRecipientList, expenseMonthId } = props;
    useEffect(() => { loadExpenseByRecipientList(expenseMonthId) });

    const data = expenses.map(e => {
        const expenseRecipient = e.expenseRecipient || { expenseRecipientId: 0, name: '' };
        return { id: expenseRecipient.name, label: expenseRecipient.name, value: e.value };
    });
    console.log(JSON.stringify(data));
    return (<PieChart data={data} />);
}

export default connect(
    (state, ownProps) => ({ expenses: selectors.expenseByRecipientList(state, ownProps.expenseMonthId) }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseMonthByRecipient);
