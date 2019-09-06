import merge from 'lodash.merge';

const UPDATE_ENTITIES = 'UPDATE_ENTITIES';

const initialState = {
    expenseCategories: {},
    expenseRecipients:{},
    expenses: {}
};

export function updateEntities(entities) {
    return { type: UPDATE_ENTITIES, payload: { entities } };
}

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === UPDATE_ENTITIES) {
        return merge({}, state, action.payload.entities);
    }

    return state;
};
