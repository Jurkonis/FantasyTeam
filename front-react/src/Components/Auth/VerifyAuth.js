import React, { Component } from "react";
import { connect } from "react-redux";
import { verifyAuth } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";

export class VerifyAuth extends Component {
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
        <h5>Verify yourself</h5>
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
          <div className="btn" onClick={() => this.props.verifyAuth(this.state.authData.Passcode)}>
            Submit
          </div>
        </div>
      </div>
    );
  }
}

VerifyAuth.propTypes = {
  verifyAuth: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({
  user: state.users.user,
});

export default connect(mapStateToProps, { verifyAuth })(VerifyAuth);
