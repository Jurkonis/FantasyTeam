import React, { Component } from "react";
import { connect } from "react-redux";
import { disableAuth } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";

export class DisableAuth extends Component {
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
        <h5>Disable auth</h5>
        <h3>Enter code to disable auth!</h3>
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
          <div className="btn" onClick={() => this.props.disableAuth(this.state.authData.Passcode)}>
            Submit
          </div>
        </div>
      </div>
    );
  }
}

DisableAuth.propTypes = {
  disableAuth: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({
  user: state.users.user,
});

export default connect(mapStateToProps, { disableAuth })(DisableAuth);
