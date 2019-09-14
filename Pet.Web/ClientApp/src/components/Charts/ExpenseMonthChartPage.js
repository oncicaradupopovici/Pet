import React, { useState } from 'react';
import { connect } from 'react-redux';
import { Container, Row, Col, TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';
import * as expenseMonth from '../../store/ExpenseMonth';
import CurrentExpenseMonthSelector from '../CurrentExpenseMonthSelector';
import ExpenseMonthByCategory from './ExpenseMonthByCategory';
import ExpenseMonthByRecipient from './ExpenseMonthByRecipient';
import classnames from 'classnames';

const ExpenseMonthChartPage = (props) => {
    const { currentExpenseMonth } = props;
    const [activeTab, setActiveTab] = useState('1');

    return (
        <div>
            <Container>
                <Row className={"mb-2"}>
                    <Col><CurrentExpenseMonthSelector /></Col>
                </Row>
                <Row>
                    <Nav tabs>
                        <NavItem>
                            <NavLink
                                className={classnames({ active: activeTab === '1' })}
                                onClick={() => setActiveTab('1')}
                            >
                                By category
                            </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink
                                className={classnames({ active: activeTab === '2' })}
                                onClick={() => setActiveTab('2')}
                            >
                                By recipient
                            </NavLink>
                        </NavItem>
                    </Nav>
                    <TabContent activeTab={activeTab} style={{ width: '-webkit-fill-available' }}>
                        <TabPane tabId="1">
                            <ExpenseMonthByCategory expenseMonthId={currentExpenseMonth.expenseMonthId} />
                        </TabPane>
                        <TabPane tabId="2">
                            <ExpenseMonthByRecipient expenseMonthId={currentExpenseMonth.expenseMonthId} />
                        </TabPane>
                    </TabContent>
                </Row>
            </Container>
        </div>
    );
}

export default connect(
    (state) => ({ currentExpenseMonth: expenseMonth.selectors.currentExpenseMonth(state) })
)(ExpenseMonthChartPage);
