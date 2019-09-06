import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as expense from '../../store/Expense';
import { Button, Form, FormGroup, Modal, ModalHeader, ModalBody, ModalFooter, Input } from 'reactstrap';

class MoveToSavingsModal extends Component {

    componentDidMount() {
    }

    componentDidUpdate(prevProps) {
        if (this.props.isOpen && this.props.saved && this.props.saved !== prevProps.saved) {
            this.props.toggle();
        }
    }

    handleSave = () => {
        this.props.moveToSavings(this.props.expense.expenseId);
    }

    render() {
        const { isOpen, toggle, saving, expense } = this.props;

        return (
            <Modal isOpen={isOpen} toggle={toggle}>
                <ModalHeader toggle={toggle}>Move to savings</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <Input type="text" value={expense && expense.details1} disabled />
                        </FormGroup>
                        <FormGroup>
                            <Input type="text" value={expense && expense.details2} disabled />
                        </FormGroup>
                        <FormGroup>
                            <Input type="number" value={expense && expense.value} disabled />
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
        saving: expense.selectors.toSavings(state).saving,
        saved: expense.selectors.toSavings(state).saved
    }),
    dispatch => bindActionCreators(expense.actionCreators, dispatch)
)(MoveToSavingsModal);