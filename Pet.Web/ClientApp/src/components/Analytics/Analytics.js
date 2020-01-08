import React, { Component } from 'react';
import { TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';
import classnames from 'classnames';
import ExpenseByCategoryList from './ExpenseByCategoryList';
import ExpenseByRecipientList from './ExpenseByRecipientList';
import ExpenseMonthByCategory from '../Charts/ExpenseMonthByCategory';
import ExpenseMonthByRecipient from '../Charts/ExpenseMonthByRecipient';


export default class Analytics extends Component {
  state = {
    activeTab: '1'
  }

  toggle = (tab) => {
    if (this.state.activeTab !== tab) {
      this.setState({
        activeTab: tab
      });
    }
  }

  render() {
    const { expenseMonthId } = this.props;
    return (
      <div>
        <Nav tabs>
          <NavItem>
            <NavLink
              className={classnames({ active: this.state.activeTab === '1' })}
              onClick={() => { this.toggle('1'); }}
            >
              By category
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              className={classnames({ active: this.state.activeTab === '2' })}
              onClick={() => { this.toggle('2'); }}
            >
              By recipient
            </NavLink>
          </NavItem>
        </Nav>
        <TabContent activeTab={this.state.activeTab}>
          <TabPane tabId="1">
            <ExpenseByCategoryList expenseMonthId={expenseMonthId} />
            <ExpenseMonthByCategory expenseMonthId={expenseMonthId} />
          </TabPane>
          <TabPane tabId="2">
            <ExpenseByRecipientList expenseMonthId={expenseMonthId} />
            <ExpenseMonthByRecipient expenseMonthId={expenseMonthId} />
          </TabPane>
        </TabContent>
      </div>
    );
  }
}
