import React, { Component } from "react";
import MessageList from "./Messages/MessagesList.js";

class Home extends Component {
  render() {
    return (
      <div className="home">
        <MessageList />
        <h2>Welcome to the Fantasy team website!</h2>
        <h3>Here you can create your own team and compete with others!</h3>
      </div>
    );
  }
}

export default Home;
