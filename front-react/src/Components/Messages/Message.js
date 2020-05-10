import React, { Component } from "react";
import PropTypes from "prop-types";
import classnames from "classnames";

export class Message extends Component {
  onClick() {
    this.props.deleteMessage(this.props.message.id);
  }
  render() {
    const { type, text } = this.props.message;
    return (
      <div
        className={classnames("alert", {
          "alert-success": type === "success",
          "alert-danger": type === "failure",
        })}
      >
        <button onClick={this.onClick.bind(this)} className="close">
          <span>&times;</span>
        </button>
        {text}
      </div>
    );
  }
}

Message.propTypes = {
  message: PropTypes.object.isRequired,
};

export default Message;
