import React, { useEffect } from 'react';
import { ResponsiveLine } from '@nivo/line';
import { actionCreators, selectors } from '../../store/ExpenseByCategory';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { groupBy } from '../../utils/groupBy';
// make sure parent container have a defined height when using
// responsive component, otherwise height will be 0 and
// no chart will be rendered.
// website examples showcase many properties,
// you'll often use just a few of them.
const MyChart = ({ data /* see data tab */ }) => (
    <ResponsiveLine
        data={data}
        margin={{ top: 50, right: 110, bottom: 50, left: 60 }}
        xScale={{ type: 'point' }}
        yScale={{ type: 'linear', stacked: false, min: 'auto', max: 'auto' }}
        axisTop={null}
        axisRight={null}
        axisBottom={{
            orient: 'bottom',
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'Expense Month',
            legendOffset: 36,
            legendPosition: 'middle'
        }}
        axisLeft={{
            orient: 'left',
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'Amount',
            legendOffset: -40,
            legendPosition: 'middle'
        }}
        colors={{ scheme: 'nivo' }}
        pointSize={10}
        pointColor={{ theme: 'background' }}
        pointBorderWidth={2}
        pointBorderColor={{ from: 'serieColor' }}
        pointLabelYOffset={-12}
        useMesh={true}
        legends={[
            {
                anchor: 'bottom-right',
                direction: 'column',
                justify: false,
                translateX: 100,
                translateY: 0,
                itemsSpacing: 0,
                itemDirection: 'left-to-right',
                itemWidth: 80,
                itemHeight: 20,
                itemOpacity: 0.75,
                symbolSize: 12,
                symbolShape: 'circle',
                symbolBorderColor: 'rgba(0, 0, 0, .5)',
                effects: [
                    {
                        on: 'hover',
                        style: {
                            itemBackground: 'rgba(0, 0, 0, .03)',
                            itemOpacity: 1
                        }
                    }
                ]
            }
        ]}
    />
)

export const ExpenseCategoryEvolutionChart = ({ expenses, loadExpenseByCategoryInRangeList, fromExpenseMonthId, toExpenseMonthId }) => {
    useEffect(() => { loadExpenseByCategoryInRangeList(fromExpenseMonthId, toExpenseMonthId) });
    const keySelector = expense => expense.expenseCategory || { expenseCategoryId: 0, name: '' };
    const keyComparer = (leftKey, rightKey) => leftKey.expenseCategoryId === rightKey.expenseCategoryId;
    const expensesGroupBy = groupBy(expenses, keySelector, keyComparer);
    const data = expensesGroupBy.map(e => ({ id: e.key.name, data: e.value.map(v => ({ x: v.expenseMonthName, y: v.value })) }));

    return (
        <div style={{ height: '500px' }}>
            <MyChart data={data} />
        </div>
    );
}

export default connect(
    (state, ownProps) => ({
        expenses: selectors.expenseByCategoryInRangeList(state, ownProps.fromExpenseMonthId, ownProps.toExpenseMonthId)
    }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ExpenseCategoryEvolutionChart);