import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as expenseRecipient from '../../store/ExpenseRecipient';
import * as expense from '../../store/Expense';
import { Button, Form, FormGroup, Modal, ModalHeader, ModalBody, ModalFooter, CustomInput, Input } from 'reactstrap';
import Select from 'react-select';

const initialState = {
    selectedExpenseRecipient: null,
    newExpenseRecipientName: '',
    isNew: false,
};

class ExpenseRecipientModal extends Component {
    state = initialState;

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate(prevProps) {
        if (this.props.expense !== prevProps.expense && this.props.expense) {
            this.setState({ 
                ...initialState, 
                newExpenseRecipientName: this.props.expense.expenseRecipientDetailCode,
                selectedExpenseRecipient: this.props.expense.expenseRecipient
             })
        }

        if (this.props.isOpen && this.props.saved && this.props.saved !== prevProps.saved) {
            this.props.toggle();
        }

        if (this.props.expenseRecipientsLoaded !== prevProps.expenseRecipientsLoaded && !this.props.expenseRecipientsLoaded) {
            this.ensureDataFetched();
        }
    }

    ensureDataFetched() {
        // const posTerminalId = this.props.match.params.posTerminalId;
        // this.props.loadPosTerminal(posTerminalId);
        this.props.loadExpenseRecipientList();
    }

    handleNewExpenseRecipientNameChange = e => {
        const newExpenseRecipientName = e.target.value;
        this.setState({ newExpenseRecipientName });
    }

    handleSelectedExpenseRecipientChange = (selectedExpenseRecipient) => {
        this.setState({ selectedExpenseRecipient });
    }

    handleIsNewChange = e => {
        const isNew = e.target.checked;
        this.setState({ isNew });
    }

    handleSave = () => {
        const { selectedExpenseRecipient, newExpenseRecipientName, isNew } = this.state;
        this.props.saveRecipient(this.props.expense.expenseId, {
            expenseRecipientId: isNew ? null : selectedExpenseRecipient.expenseRecipientId,
            name: isNew ? newExpenseRecipientName : null,
            isNew
        });
    }

    render() {
        const { isOpen, toggle, expenseRecipients, saving } = this.props;
        const { selectedExpenseRecipient, newExpenseRecipientName, isNew } = this.state;

        return (
            <Modal isOpen={isOpen} toggle={toggle}>
                <ModalHeader toggle={toggle}>Expense recipient</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <CustomInput type="checkbox" id="expenseRecipientIsNew" label="Is new" checked={isNew} onChange={this.handleIsNewChange} />
                        </FormGroup>
                        <FormGroup>
                            {isNew
                                ? <Input type="text" value={newExpenseRecipientName} onChange={this.handleNewExpenseRecipientNameChange} />
                                : <Select isClearable options={expenseRecipients} getOptionLabel={option => option.name} getOptionValue={option => option.expenseRecipientId} value={selectedExpenseRecipient} onChange={this.handleSelectedExpenseRecipientChange} />
                            }
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
        saving: expense.selectors.recipient(state).saving,
        saved: expense.selectors.recipient(state).saved,
        expenseRecipients: expenseRecipient.selectors.expenseRecipientList(state),
        expenseRecipientsLoaded: expenseRecipient.selectors.expenseRecipientListLoaded(state),
    }),
    dispatch => bindActionCreators({ ...expenseRecipient.actionCreators, ...expense.actionCreators }, dispatch)
)(ExpenseRecipientModal);