import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchMyIcons, changeMyIcon } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";
import Icon from "./MyIcon.js";

export class MyIcons extends Component {
  componentDidMount() {
    this.props.fetchMyIcons();
  }
  render() {
    let icons = this.props.myIcons.map((icon) => (
      <li key={icon.id}>
        <Icon id={icon.id} icon={icon} />
      </li>
    ));
    return (
      <div className="shopWrapper">
        <MessageList />
        <h2>My icons</h2>
        <ul className="shopList">{icons}</ul>
      </div>
    );
  }
}

MyIcons.propTypes = {
  fetchMyIcons: PropTypes.func.isRequired,
  changeMyIcon: PropTypes.func.isRequired,
  myIcons: PropTypes.array.isRequired,
};

const mapStateToProps = (state) => ({
  myIcons: state.users.myIcons,
});

export default connect(mapStateToProps, { fetchMyIcons, changeMyIcon })(MyIcons);
