import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchUser } from "../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "./Messages/MessagesList.js";

export class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loginData: {
        Username: "",
        Password: "",
      },
    };
  }

  render() {
    return (
      <div className="loginWrapper">
        <MessageList />
        <h5>Login</h5>
        <input
          className="username"
          placeholder="USERNAME"
          value={this.state.loginData.Username}
          onChange={(e) => {
            let { loginData } = this.state;
            loginData.Username = e.target.value;
            this.setState({ loginData });
          }}
        />
        <input
          type="password"
          className="password"
          placeholder="PASSWORD"
          value={this.state.loginData.Password}
          onChange={(e) => {
            let { loginData } = this.state;
            loginData.Password = e.target.value;
            this.setState({ loginData });
          }}
        />
        <div className="button">
          <div className="btn" onClick={() => this.props.fetchUser(this.state.loginData.Username, this.state.loginData.Password)}>
            Sign In
          </div>
        </div>
      </div>
    );
  }
}

Login.propTypes = {
  fetchUser: PropTypes.func.isRequired,
  user: PropTypes.object.isRequired,
};

const mapStateToProps = (state) => ({
  user: state.users.user,
});

export default connect(mapStateToProps, { fetchUser })(Login);
