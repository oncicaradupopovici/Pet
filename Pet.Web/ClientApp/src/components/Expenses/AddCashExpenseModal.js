import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as expense from '../../store/Expense';
import { Button, Form, FormGroup, Modal, ModalHeader, ModalBody, ModalFooter, Input, Label } from 'reactstrap';
import moment from 'moment';

const initialState = {
    value: '',
    expenseDate: '',
    details1: '',
    details2: null
};

class AddCashExpenseModal extends Component {
    state = initialState

    componentDidUpdate(prevProps) {
        if (this.props.isOpen && this.props.saved && this.props.saved !== prevProps.saved) {
            this.setState(initialState);
            this.props.toggle();
        }

        if (this.props.expenseCategoriesLoaded !== prevProps.expenseCategoriesLoaded && !this.props.expenseCategoriesLoaded) {
            this.ensureDataFetched();
        }
    }

    handleValueChange = e => {
        const value = e.target.value;
        this.setState({ value });
    }

    handleExpenseDateChange = e => {
        const expenseDate = e.target.value;
        this.setState({ expenseDate });
    }

    handleDetails1Change = e => {
        const details1 = e.target.value;
        this.setState({ details1 });
    }

    handleDetails2Change = e => {
        const details2 = e.target.value;
        this.setState({ details2 });
    }

    handleSave = () => {
        const { value, expenseDate, details1, details2 } = this.state;
        const formatedDate = moment(expenseDate).format("YYYY-MM-DD");
        this.props.addCashExpense({ value, expenseDate: formatedDate, details1, details2 });
    }

    render() {
        const { isOpen, toggle, saving } = this.props;
        const { value, expenseDate, details1 } = this.state;

        return (
            <Modal isOpen={isOpen} toggle={toggle}>
                <ModalHeader toggle={toggle}>Expense category</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <Label>Value</Label>
                            <Input type="number" value={value} onChange={this.handleValueChange}  />
                        </FormGroup>
                        <FormGroup>
                            <Label>Date</Label>
                            <Input type="date" value={expenseDate} onChange={this.handleExpenseDateChange}  />
                        </FormGroup>
                        <FormGroup>
                            <Label>Details</Label>
                            <Input type="text" value={details1} onChange={this.handleDetails1Change}  />
                        </FormGroup>

                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.handleSave}>{saving ? 'Saving...' : 'Save'}</Button>{' '}
                    <Button color="secondary" onClick={toggle}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

export default connect(
    state => ({
        saving: expense.selectors.newCashExpense(state).saving,
        saved: expense.selectors.newCashExpense(state).saved
    }),
    dispatch => bindActionCreators(expense.actionCreators, dispatch)
)(AddCashExpenseModal);