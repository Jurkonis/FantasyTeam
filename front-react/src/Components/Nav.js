import React, { Component } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { logout } from "../actions/userActions.js";

class Nav extends Component {
  handleClick() {
    this.props.logout();
  }
  render() {
    return (
      <nav className="navbar fixed-top navbar-expand-md  ">
        <Link to="/">
          <span className="navbar-brand">Logo</span>
        </Link>
        <div className="navbar-collapse collapse">
          <ul className="nav navbar-nav">
            <Link to="/">
              <li className="nav-link">Home</li>
            </Link>
            <Link to="/Schedule">
              <li className="nav-link">Schedule</li>
            </Link>
            {window.sessionStorage.getItem("id") !== null ? (
              <Link to="/Tournaments">
                <li className="nav-link">Tournaments</li>
              </Link>
            ) : (
              ""
            )}
            {window.sessionStorage.getItem("id") !== null ? (
              <Link to="/MyFantasyTeam">
                <li className="nav-link">MyTeam</li>
              </Link>
            ) : (
              ""
            )}
            {window.sessionStorage.getItem("id") !== null ? (
              <Link to="/Shop">
                <li className="nav-link">Shop</li>
              </Link>
            ) : (
              ""
            )}
          </ul>
        </div>
        <ul className="nav navbar-nav navbar-right ">
          {window.sessionStorage.getItem("coins") !== null ? (
            <b>
              <li className="nav-link"> Coins: {window.sessionStorage.getItem("coins")}</li>{" "}
            </b>
          ) : (
            ""
          )}
          {window.sessionStorage.getItem("username") !== null ? (
            <Link to="/Profile">
              <li className="nav-link"> Profile</li>
            </Link>
          ) : (
            <Link to="/Register">
              <li className="nav-link">Sign Up</li>
            </Link>
          )}
          {window.sessionStorage.getItem("username") !== null ? (
            <Link to="\">
              <li className="nav-link" onClick={this.handleClick.bind(this)}>
                Logout
              </li>
            </Link>
          ) : (
            <Link to="/Login">
              <li className="nav-link"> Login </li>
            </Link>
          )}
        </ul>
      </nav>
    );
  }
}

Nav.propTypes = {
  logout: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { logout })(Nav);
