import React, { Component } from "react";
import { connect } from "react-redux";
import { changeMyIcon } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import Axios from "axios";

export class MyIcon extends Component {
  state = {
    icon: { name: "default" },
    config: {
      headers: { Authorization: "bearer " + sessionStorage.getItem("token") },
    },
  };
  componentDidMount() {
    this.getIcon();
  }

  getIcon() {
    Axios.get(`http://localhost:5001/api/Icons/` + this.props.icon.iconId, this.state.config)
      .then((res) => {
        this.setState({ icon: res.data });
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
  handleClick() {
    this.props.changeMyIcon(this.state.icon.name);
  }

  render() {
    let icon = this.state.icon;
    const logo = require("../../Images/Icons/" + icon.name + ".png");
    return (
      <div className="icon">
        <div className="iconImg">
          <img src={logo} alt="icon" />
        </div>
        <div className="myButton3" onClick={this.handleClick.bind(this)}>
          Select
        </div>
      </div>
    );
  }
}

MyIcon.propTypes = {
  changeMyIcon: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { changeMyIcon })(MyIcon);
