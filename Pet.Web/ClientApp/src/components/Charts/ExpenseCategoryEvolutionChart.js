import React, { Component } from 'react';
import { ResponsiveLine } from '@nivo/line';
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
        yScale={{ type: 'linear', stacked: true, min: 'auto', max: 'auto' }}
        axisTop={null}
        axisRight={null}
        axisBottom={{
            orient: 'bottom',
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'transportation',
            legendOffset: 36,
            legendPosition: 'middle'
        }}
        axisLeft={{
            orient: 'left',
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'count',
            legendOffset: -40,
            legendPosition: 'middle'
        }}
        colors={{ scheme: 'nivo' }}
        pointSize={10}
        pointColor={{ theme: 'background' }}
        pointBorderWidth={2}
        pointBorderColor={{ from: 'serieColor' }}
        pointLabel="y"
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

const data = [
    {
        "id": "japan",
        "color": "hsl(249, 70%, 50%)",
        "data": [
            {
                "x": "plane",
                "y": 267
            },
            {
                "x": "helicopter",
                "y": 215
            },
            {
                "x": "boat",
                "y": 155
            },
            {
                "x": "train",
                "y": 286
            },
            {
                "x": "subway",
                "y": 67
            },
            {
                "x": "bus",
                "y": 62
            },
            {
                "x": "car",
                "y": 191
            },
            {
                "x": "moto",
                "y": 212
            },
            {
                "x": "bicycle",
                "y": 210
            },
            {
                "x": "horse",
                "y": 251
            },
            {
                "x": "skateboard",
                "y": 232
            },
            {
                "x": "others",
                "y": 13
            }
        ]
    },
    {
        "id": "france",
        "color": "hsl(129, 70%, 50%)",
        "data": [
            {
                "x": "plane",
                "y": 102
            },
            {
                "x": "helicopter",
                "y": 178
            },
            {
                "x": "boat",
                "y": 150
            },
            {
                "x": "train",
                "y": 95
            },
            {
                "x": "subway",
                "y": 190
            },
            {
                "x": "bus",
                "y": 28
            },
            {
                "x": "car",
                "y": 43
            },
            {
                "x": "moto",
                "y": 182
            },
            {
                "x": "bicycle",
                "y": 219
            },
            {
                "x": "horse",
                "y": 128
            },
            {
                "x": "skateboard",
                "y": 181
            },
            {
                "x": "others",
                "y": 283
            }
        ]
    },
    {
        "id": "us",
        "color": "hsl(282, 70%, 50%)",
        "data": [
            {
                "x": "plane",
                "y": 165
            },
            {
                "x": "helicopter",
                "y": 201
            },
            {
                "x": "boat",
                "y": 281
            },
            {
                "x": "train",
                "y": 284
            },
            {
                "x": "subway",
                "y": 289
            },
            {
                "x": "bus",
                "y": 108
            },
            {
                "x": "car",
                "y": 149
            },
            {
                "x": "moto",
                "y": 140
            },
            {
                "x": "bicycle",
                "y": 59
            },
            {
                "x": "horse",
                "y": 68
            },
            {
                "x": "skateboard",
                "y": 154
            },
            {
                "x": "others",
                "y": 194
            }
        ]
    },
    {
        "id": "germany",
        "color": "hsl(310, 70%, 50%)",
        "data": [
            {
                "x": "plane",
                "y": 105
            },
            {
                "x": "helicopter",
                "y": 71
            },
            {
                "x": "boat",
                "y": 185
            },
            {
                "x": "train",
                "y": 62
            },
            {
                "x": "subway",
                "y": 270
            },
            {
                "x": "bus",
                "y": 55
            },
            {
                "x": "car",
                "y": 115
            },
            {
                "x": "moto",
                "y": 242
            },
            {
                "x": "bicycle",
                "y": 229
            },
            {
                "x": "horse",
                "y": 259
            },
            {
                "x": "skateboard",
                "y": 168
            },
            {
                "x": "others",
                "y": 175
            }
        ]
    },
    {
        "id": "norway",
        "color": "hsl(151, 70%, 50%)",
        "data": [
            {
                "x": "plane",
                "y": 253
            },
            {
                "x": "helicopter",
                "y": 105
            },
            {
                "x": "boat",
                "y": 70
            },
            {
                "x": "train",
                "y": 212
            },
            {
                "x": "subway",
                "y": 237
            },
            {
                "x": "bus",
                "y": 27
            },
            {
                "x": "car",
                "y": 168
            },
            {
                "x": "moto",
                "y": 60
            },
            {
                "x": "bicycle",
                "y": 24
            },
            {
                "x": "horse",
                "y": 299
            },
            {
                "x": "skateboard",
                "y": 237
            },
            {
                "x": "others",
                "y": 10
            }
        ]
    }
];

export default class ExpenseCategoryEvolutionChart extends Component {
    render() {
        return (
            <div style={{ height: '500px' }}>
                <MyChart data={data} />
            </div>
        )
    }
}