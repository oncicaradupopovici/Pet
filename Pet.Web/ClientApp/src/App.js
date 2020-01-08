import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import ImportReport from './components/ImportReport';
import ExpensesPage from './components/Expenses/ExpensesPage';
import AnalyticsPage from './components/Analytics/AnalyticsPage';
import Tenant from './components/Tenant';
import ExpenseCategoryEvolutionChart from './components/Charts/ExpenseCategoryEvolutionChart';
import ExpenseMonthChartPage from './components/Charts/ExpenseMonthChartPage';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/import-report' component={ImportReport} />
    <Route path='/expenses' component={ExpensesPage} />
    <Route path='/analytics' component={AnalyticsPage} />
    <Route path='/tenant/:tenantId' component={Tenant} />
    <Route exact path='/chart' component={ExpenseCategoryEvolutionChart} />
    <Route exact path='/chart/month' component={ExpenseMonthChartPage} />
  </Layout>
);