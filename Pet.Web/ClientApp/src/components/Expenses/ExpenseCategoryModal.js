import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as expenseCategory from '../../store/ExpenseCategory';
import * as expense from '../../store/Expense';
import { Button, Form, FormGroup, Modal, ModalHeader, ModalBody, ModalFooter, CustomInput, Input } from 'reactstrap';
import Select from 'react-select';

const initialState = {
    selectedExpenseCategory: null,
    newExpenseCategoryName: '',
    isNew: false,
    justThisOne: false
}

class ExpenseCategoryModal extends Component {
    state = initialState

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate(prevProps) {
        if (this.props.expense !== prevProps.expense && this.props.expense) {
            this.setState({
                ...initialState,
                selectedExpenseCategory: this.props.expense.expenseCategory
            });
        }

        if (this.props.isOpen && this.props.saved && this.props.saved !== prevProps.saved) {
            this.props.toggle();
        }

        if (this.props.expenseCategoriesLoaded !== prevProps.expenseCategoriesLoaded && !this.props.expenseCategoriesLoaded) {
            this.ensureDataFetched();
        }
    }

    ensureDataFetched() {
        this.props.loadExpenseCategoryList();
    }

    handleNewExpenseCategoryNameChange = e => {
        const newExpenseCategoryName = e.target.value;
        this.setState({ newExpenseCategoryName });
    }

    handleSelectedExpenseCategoryChange = (selectedExpenseCategory) => {
        this.setState({ selectedExpenseCategory });
    }

    handleIsNewChange = e => {
        const isNew = e.target.checked;
        this.setState({ isNew });
    }

    handleJustThisOneChange = e => {
        const justThisOne = e.target.checked;
        this.setState({ justThisOne });
    }

    handleSave = () => {
        const { selectedExpenseCategory, newExpenseCategoryName, isNew, justThisOne } = this.state;
        this.props.saveCategory(this.props.expense.expenseId, {
            expenseCategoryId: isNew ? null : selectedExpenseCategory.expenseCategoryId,
            name: isNew ? newExpenseCategoryName : null,
            isNew
        }, justThisOne);
    }

    render() {
        const { isOpen, toggle, expenseCategories, saving } = this.props;
        const { selectedExpenseCategory, newExpenseCategoryName, isNew, justThisOne } = this.state;

        return (
            <Modal isOpen={isOpen} toggle={toggle}>
                <ModalHeader toggle={toggle}>Expense category</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <CustomInput type="checkbox" id="justThisOne" label="Just this one" checked={justThisOne} onChange={this.handleJustThisOneChange} />
                        </FormGroup>
                        <FormGroup>
                            <CustomInput type="checkbox" id="expenseCategoryIsNew" label="Is new" checked={isNew} onChange={this.handleIsNewChange} />
                        </FormGroup>
                        <FormGroup>
                            {isNew
                                ? <Input type="text" value={newExpenseCategoryName} onChange={this.handleNewExpenseCategoryNameChange} />
                                : <Select isClearable options={expenseCategories} getOptionLabel={option => option.name} getOptionValue={option => option.expenseCategoryId} value={selectedExpenseCategory} onChange={this.handleSelectedExpenseCategoryChange} />
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
        saving: expense.selectors.category(state).saving,
        saved: expense.selectors.category(state).saved,
        expenseCategories: expenseCategory.selectors.expenseCategoryList(state),
        expenseCategoriesLoaded: expenseCategory.selectors.expenseCategoryListLoaded(state),
    }),
    dispatch => bindActionCreators({ ...expenseCategory.actionCreators, ...expense.actionCreators }, dispatch)
)(ExpenseCategoryModal);