import React, { useEffect, useCallback } from 'react';
import LineChartMonthProgress from './common/LineChartMonthProgress';
import { actionCreators, selectors } from '../../store/ExpenseByCategory';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { groupBy } from '../../utils/groupBy';

export const ExpenseCategoryEvolutionChart = (props) => {
    const { expenses, loadExpenseByCategoryInRangeList, fromExpenseMonthId, toExpenseMonthId, history } = props;
    useEffect(() => { loadExpenseByCategoryInRangeList(fromExpenseMonthId, toExpenseMonthId) });
    const handlePointClick = useCallback(_point => history.push('chart/month'), [history]);

    const keySelector = expense => expense.expenseCategory || { expenseCategoryId: 0, name: '' };
    const keyComparer = (leftKey, rightKey) => leftKey.expenseCategoryId === rightKey.expenseCategoryId;
    const expensesGroupBy = groupBy(expenses, keySelector, keyComparer);
    const data = expensesGroupBy.map(e => ({ id: e.key.name, data: e.value.map(v => ({ x: v.expenseMonthName, y: v.value, expenseMonthId: v.expenseMonth })) }));

    return (
        <div style={{ height: '500px' }}>
            <LineChartMonthProgress data={data} onPointClick={handlePointClick} />
        </div>
    );
}

export default connect(
    (state, ownProps) => ({
        expenses: selectors.expenseByCategoryInRangeList(state, ownProps.fromExpenseMonthId, ownProps.toExpenseMonthId)
    }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseCategoryEvolutionChart);