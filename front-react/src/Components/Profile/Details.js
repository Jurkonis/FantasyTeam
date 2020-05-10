import React, { Component } from "react";
import Axios from "axios";
import { Modal } from "reactstrap";
import { updateUser, changePassword, enableAuth, disable } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import MessageList from "../Messages/MessagesList.js";

export class Details extends Component {
  state = {
    user: { icon: "default" },
    editUserData: {
      firstname: "",
      secondname: "",
    },
    changePasswordData: {
      oldPassword: "",
      newPassword: "",
      newPassword2: "",
    },
    editUserModal: false,
    changePasswordModal: false,
    config: {
      headers: { Authorization: "bearer " + sessionStorage.getItem("token") },
    },
  };

  componentDidMount() {
    this.getUser();
  }

  getUser() {
    Axios.get("http://localhost:5001/api/Users/" + window.sessionStorage.getItem("id"), this.state.config)
      .then((response) => {
        this.setState({
          user: response.data,
        });
        window.sessionStorage.setItem("coins", response.data.coins);
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data);
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log("Error", error.message);
        }
      });
  }

  handleUpdate() {
    let { firstname, secondname } = this.state.editUserData;
    this.props.updateUser(firstname, secondname);
    this.setState({
      editUserModal: !this.state.editUserModal,
    });
    this.getUser();
  }

  handleChangePassword() {
    let { oldPassword, newPassword, newPassword2 } = this.state.changePasswordData;
    this.props.changePassword(oldPassword, newPassword, newPassword2);
    this.setState({
      changePasswordModal: !this.state.changePasswordModal,
    });
  }

  toggleEditUserModal() {
    this.setState({
      editUserModal: !this.state.editUserModal,
    });
  }

  toggleChangePasswordModal() {
    this.setState({
      changePasswordModal: !this.state.changePasswordModal,
    });
  }

  editUser(firstname, secondname) {
    this.setState({
      editUserData: { firstname, secondname },
      editUserModal: !this.state.editUserModal,
    });
  }

  changeIcon() {
    window.location.href = "/MyIcons";
  }

  render() {
    const logo = require("../../Images/Icons/" + this.state.user.icon + ".png");
    const user = this.state.user;
    return (
      <div>
        <MessageList />
        <div className="profile">
          <Modal isOpen={this.state.editUserModal} toggle={this.toggleEditUserModal.bind(this)}>
            <div className="header">
              <div className="left">Update info</div>
              <div className="right" onClick={this.toggleEditUserModal.bind(this)}>
                &times;
              </div>
            </div>
            <div className="body">
              <div>
                <label>Firstname</label>
                <div>
                  <input
                    className="username"
                    placeholder="FIRSTNAME"
                    value={this.state.editUserData.firstname}
                    onChange={(e) => {
                      let { editUserData } = this.state;
                      editUserData.firstname = e.target.value;
                      this.setState({ editUserData });
                    }}
                  />
                </div>
                <label>Lastname</label>
                <div>
                  <input
                    className="username"
                    placeholder="SECONDNAME"
                    value={this.state.editUserData.secondname}
                    onChange={(e) => {
                      let { editUserData } = this.state;
                      editUserData.secondname = e.target.value;
                      this.setState({ editUserData });
                    }}
                  />
                </div>
              </div>
            </div>
            <div className="footer">
              <div className="btn" onClick={this.handleUpdate.bind(this)}>
                Update
              </div>
              <div className="btn" onClick={this.toggleEditUserModal.bind(this)}>
                Cancel
              </div>
            </div>
          </Modal>

          <Modal isOpen={this.state.changePasswordModal} toggle={this.toggleChangePasswordModal.bind(this)}>
            <div className="header">
              <div className="left">Update info</div>
              <div className="right" onClick={this.toggleChangePasswordModal.bind(this)}>
                &times;
              </div>
            </div>
            <div className="body">
              <div>
                <label>Current password</label>
                <div>
                  <input
                    type="password"
                    className="username"
                    onChange={(e) => {
                      let { changePasswordData } = this.state;
                      changePasswordData.oldPassword = e.target.value;
                      this.setState({ changePasswordData });
                    }}
                  />
                </div>
                <label>New password</label>
                <div>
                  <input
                    type="password"
                    className="username"
                    onChange={(e) => {
                      let { changePasswordData } = this.state;
                      changePasswordData.newPassword = e.target.value;
                      this.setState({ changePasswordData });
                    }}
                  />
                </div>
                <label>Repeat new password</label>
                <div>
                  <input
                    type="password"
                    className="username"
                    onChange={(e) => {
                      let { changePasswordData } = this.state;
                      changePasswordData.newPassword2 = e.target.value;
                      this.setState({ changePasswordData });
                    }}
                  />
                </div>
              </div>
            </div>
            <div className="footer">
              <div className="btn" onClick={this.handleChangePassword.bind(this)}>
                Submit
              </div>
              <div className="btn" onClick={this.toggleChangePasswordModal.bind(this)}>
                Cancel
              </div>
            </div>
          </Modal>
          <div className="profileLeft">
            <div className="wrapper">
              <div className="profilePicture" onClick={this.changeIcon.bind(this)}>
                <img src={logo} alt="profilePicture" />
              </div>
              <p>Hello, {user.username}!</p>
            </div>
            <div className="wrapper">
              <div className="balance">
                <h5>Ballance</h5>
                <p>{user.coins}</p>
              </div>
            </div>
          </div>
          <div className="profileRight">
            <div className="personalDetails">
              <div>
                <div>
                  <h4>
                    <b>Personal details</b>
                  </h4>
                </div>
                <div>
                  <div className="myButton2" onClick={this.editUser.bind(this, user.firstName, user.secondName)}>
                    Edit
                  </div>
                </div>
              </div>
              <div className="line">
                <div className="lineLeft">Username</div>
                <div className="lineRight">{user.username}</div>
              </div>
              <div className="line">
                <div className="lineLeft">Name</div>
                <div className="lineRight">
                  {user.firstName} {user.secondName}
                </div>
              </div>
            </div>
            <div className="security">
              <h4>
                <b>Security</b>
              </h4>
              <div className="line">
                <div className="lineLeft">Password</div>
                <div className="lineRight">
                  <div className="myButton2" onClick={this.toggleChangePasswordModal.bind(this)}>
                    Change
                  </div>
                </div>
              </div>
              <div className="line">
                <div className="lineLeft">Google authenticator</div>
                <div className="lineRight">
                  {user.tfa === false ? (
                    <div className="myButton2" onClick={this.props.enableAuth.bind(this)}>
                      Activate
                    </div>
                  ) : (
                    <div className="myButton2" onClick={this.props.disable.bind(this)}>
                      Deactivate
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

Details.propTypes = {
  updateUser: PropTypes.func.isRequired,
  changePassword: PropTypes.func.isRequired,
  enableAuth: PropTypes.func.isRequired,
  disable: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { updateUser, changePassword, enableAuth, disable })(Details);
