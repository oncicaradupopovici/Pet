import { upload, post } from "../api/axiosApi";

const uploadReportStartedType = "UPLOAD_REPORT_STARTED";
const uploadReportFinishedType = "UPLOAD_REPORT_FINISHED";
const finqImportStartedType = "FINQ_IMPORT_STARTED";
const finqImportFinishedType = "FINQ_IMPORT_FINISHED";
const initialState = { uploading: false, finqImporting: false };

export const actionCreators = {
  uploadReport: (file, bank) => async dispatch => {
    dispatch({ type: uploadReportStartedType, payload: { file } });
    await upload("api/imports/bankreport", { file, bank });
    dispatch({ type: uploadReportFinishedType, payload: { file } });
  },
  fetchFinq: () => async dispatch => {
    dispatch({ type: finqImportStartedType, payload: {} });
    await post("api/imports/finq", {});
    dispatch({ type: finqImportFinishedType, payload: {} });
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

  if (action.type === finqImportStartedType) {
    return {
      ...state,
      finqImporting: true
    };
  }

  if (action.type === finqImportFinishedType) {
    return {
      ...state,
      finqImporting: false
    };
  }

  return state;
};
