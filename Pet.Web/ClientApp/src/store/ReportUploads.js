import { upload } from "../api/axiosApi";

const uploadReportStartedType = 'UPLOAD_REPORT_STARTED';
const uploadReportFinishedType = 'UPLOAD_REPORT_FINISHED';
const initialState = { uploading: false };

export const actionCreators = {
    uploadReport: file => async (dispatch, getState) => {
        dispatch({ type: uploadReportStartedType, payload: { file } });
        await upload('api/imports', { file });
        dispatch({ type: uploadReportFinishedType, payload: { file } });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === uploadReportStartedType) {
        return {
            ...state,
            uploading: true
        };
    }

    if (action.type === uploadReportFinishedType) {
        return {
            ...state,
            uploading: false
        };
    }

    return state;
};


function timeout(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}