import React, { Component } from "react";
import top from "../Images/Roles/top.png";
import jungle from "../Images/Roles/jungle.png";
import mid from "../Images/Roles/mid.png";
import bottom from "../Images/Roles/bottom.png";
import support from "../Images/Roles/support.png";

class Player extends Component {
  renderSwitch(param) {
    switch (param) {
      case "top":
        return top;
      case "jungle":
        return jungle;
      case "mid":
        return mid;
      case "bottom":
        return bottom;
      case "support":
        return support;
      default:
        return;
    }
  }

  render() {
    return (
      <div className="playerWrapper">
        <div className="playerRole">
          <img src={this.renderSwitch(this.props.player.role)} alt="role" />
        </div>
        <div className="playerDetails">
          <div className="summonerName">{this.props.player.summonerName}</div>
          <div>
            {this.props.player.firstName} {this.props.player.lastName}
          </div>
        </div>
        <div className="playerImg">
          <img key={this.props.player.id} src={this.props.player.image} alt="player" />
        </div>
      </div>
    );
  }
}

export default Player;
