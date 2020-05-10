import React, { Component } from "react";
import Message from "./Message.js";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { deleteMessage } from "../../actions/messagesActions.js";

export class MessagesList extends Component {
  render() {
    const messages = this.props.messages.map((message) => <Message key={message.id} message={message} deleteMessage={this.props.deleteMessage} />);
    return <div>{messages}</div>;
  }
}

MessagesList.propTypes = {
  messages: PropTypes.array.isRequired,
  deleteMessage: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({
  messages: state.messages,
});

export default connect(mapStateToProps, { deleteMessage })(MessagesList);
