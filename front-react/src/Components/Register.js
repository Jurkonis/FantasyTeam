import React, { Component } from "react";
import { connect } from "react-redux";
import { postUser } from "../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "./Messages/MessagesList.js";

export class Register extends Component {
  constructor(props) {
    super(props);
    this.state = {
      newUserData: {
        Username: "",
        Password: "",
      },
    };
  }

  render() {
    return (
      <div className="loginWrapper">
        <MessageList />
        <h5>Register</h5>
        <input
          className="username"
          placeholder="USERNAME"
          value={this.state.newUserData.Username}
          onChange={(e) => {
            let { newUserData } = this.state;
            newUserData.Username = e.target.value;
            this.setState({ newUserData });
          }}
        />
        <input
          type="password"
          className="password"
          placeholder="PASSWORD"
          value={this.state.newUserData.Password}
          onChange={(e) => {
            let { newUserData } = this.state;
            newUserData.Password = e.target.value;
            this.setState({ newUserData });
          }}
        />
        <div className="button">
          <div className="btn" onClick={() => this.props.postUser(this.state.newUserData.Username, this.state.newUserData.Password)}>
            Register
          </div>
        </div>
      </div>
    );
  }
}

Register.propTypes = {
  postUser: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { postUser })(Register);
