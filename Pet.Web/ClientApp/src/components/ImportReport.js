import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/ReportUploads';

class ImportReport extends React.PureComponent {

  state = {
    value: ''
  }

  fileInputRef = React.createRef();

  handleSubmit = (event) => {
    event.preventDefault();
    const file = this.fileInputRef.current.files[0];
    this.props.uploadReport(file);
  }

  render() {
    return (
      <div>
        <h1>Import report</h1>

        <p>Import from bank report.</p>

        <form onSubmit={this.handleSubmit}>
          <div className="form-group row">
            <label className="col-sm-2 col-form-label">Bank report</label>
            <div className="col-sm-10">
              <input type="file" className="form-control-file" id="exampleFormControlFile1" ref={this.fileInputRef} />
            </div>
          </div>
          <div className="form-group row">
            <div className="col-sm-10">
              <button type="submit" className="btn btn-primary">{this.props.uploading ? 'Uploading...' : 'Upload'}</button>
            </div>
          </div>
        </form>
      </div>
    );
  }
}

export default connect(
  state => state.reportUploads,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ImportReport);
