import React, { Component } from "react";
import { connect } from "react-redux";
import { enableAuth2 } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";

export class EnableAuth extends Component {
  constructor(props) {
    super(props);
    this.state = {
      authData: {
        Passcode: "",
      },
    };
  }

  render() {
    return (
      <div className="loginWrapper">
        <MessageList />
        <h5>Enable auth</h5>
        <h3>Scan barcode and enter the code!</h3>
        <div className="barcode">
          <img src={window.sessionStorage.getItem("tempData")} alt="bar" />
        </div>
        <input
          className="password"
          placeholder="Code"
          value={this.state.authData.Password}
          onChange={(e) => {
            let { authData } = this.state;
            authData.Passcode = e.target.value;
            this.setState({ authData });
          }}
        />
        <div className="button">
          <div className="btn" onClick={() => this.props.enableAuth2(this.state.authData.Passcode)}>
            Submit
          </div>
        </div>
      </div>
    );
  }
}

EnableAuth.propTypes = {
  enableAuth2: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({
  user: state.users.user,
});

export default connect(mapStateToProps, { enableAuth2 })(EnableAuth);
