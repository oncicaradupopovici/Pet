import React, { useCallback, useState } from 'react';
import { ResponsiveLine } from '@nivo/line';
import { actionCreators } from '../../../store/ExpenseMonth';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
// make sure parent container have a defined height when using
// responsive component, otherwise height will be 0 and
// no chart will be rendered.
// website examples showcase many properties,
// you'll often use just a few of them.
const LineChartMonthProgress = ({ data, onPointClick, setCurrentExpenseMonth }) => {
    const [hiddenIds, setHiddenIds] = useState([]);

    const handleChartClick = useCallback((point, _event) => {
        if (!point || !point.data) {
            return;
        }

        setCurrentExpenseMonth({ expenseMonthId: point.data.expenseMonthId });
        onPointClick(point);
    }, [setCurrentExpenseMonth, onPointClick]);

    const handleLegendClick = useCallback((point, _event) => {
        const index = hiddenIds.indexOf(point.id);
        index === -1
            ? setHiddenIds(prevHiddenIds => [...prevHiddenIds, point.id])
            : setHiddenIds(prevHiddenIds => [...prevHiddenIds.slice(0, index), ...prevHiddenIds.slice(index + 1)]);
    }, [hiddenIds]);

    const tooltip = ({ point }) => {
        return (<div style={{ pointerEvents: 'none', position: 'absolute', zIndex: 10, top: '0px', left: '0px', transform: 'translate3d(10px, 10px, 0px)' }}>
            <div style={{ background: 'white', color: 'inherit', fontSize: 'inherit', borderRadius: '2px', boxShadow: 'rgba(0, 0, 0, 0.25) 0px 1px 2px', padding: '5px 9px' }}>
                <div style={{ whiteSpace: 'pre', display: 'flex', alignItems: 'left', flexDirection: 'column' }}>
                    <div style={{ display: 'flex' }}>
                        <span style={{ width: '12px', height: '12px', background: 'rgb(241, 225, 91)', marginRight: '7px', marginTop: '7px' }}>                    </span>
                        <span>Category: <strong>{point.serieId}</strong></span>
                    </div>
                    <div >
                        <span >Amount: <strong>{point.data.y}</strong></span>
                    </div>
                </div>
            </div>
        </div>);
    }

    return (
        <ResponsiveLine
            data={data.filter(d => !hiddenIds.includes(d.id))}
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
                    ],
                    onClick: handleLegendClick
                }
            ]}
            onClick={handleChartClick}
            tooltip={tooltip}
        />
    );
}

export default connect(() => ({}), dispatch => bindActionCreators(actionCreators, dispatch))(LineChartMonthProgress)