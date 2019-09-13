import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../store/ReportUploads";
import {
  Button,
  Form,
  FormGroup,
  Col
} from "reactstrap";

class ImportReport extends React.PureComponent {
  state = {
    value: ""
  };

  fileInputRef = React.createRef();

  uploadBankReport = bank => event => {
    event.preventDefault();
    const file = this.fileInputRef.current.files[0];
    this.props.uploadReport(file, bank);
  }

  fetchFinq = event => {
    event.preventDefault();
    this.props.fetchFinq();
  }

  render() {
    return (
      <div>
        <h1>Import bank data</h1>

        <Form>
          <FormGroup row>
            <Col sm={2}>Ing</Col>
            <Col sm={8}>
              <input
                type="file"
                className="form-control-file"
                id="exampleFormControlFile1"
                ref={this.fileInputRef}
              />
            </Col>
            <Col sm={2}>
              <Button color="primary" onClick={this.uploadBankReport("ing")}>{this.props.uploading ? "Uploading..." : "Upload"}</Button>
            </Col>
          </FormGroup>
          <FormGroup row>
            <Col sm={2}>Finqware</Col>
            <Col sm={8}></Col>
            <Col sm={2}>
              <Button color="success" onClick={this.fetchFinq}>{this.props.finqImporting ? "Fetching data..." : "Fetch"}</Button>
            </Col>
          </FormGroup>
        </Form>
      </div>
    );
  }
}

export default connect(
  state => state.reportUploads,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ImportReport);
