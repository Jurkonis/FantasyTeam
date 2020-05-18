import React, { Component } from "react";
import top from "../../Images/Roles/top.png";
import jungle from "../../Images/Roles/jungle.png";
import mid from "../../Images/Roles/mid.png";
import bottom from "../../Images/Roles/bottom.png";
import support from "../../Images/Roles/support.png";
import { connect } from "react-redux";
import { pickPlayer } from "../../actions/userActions.js";
import PropTypes from "prop-types";

class SelectPlayer extends Component {
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
      <div
        className="clickPlayer"
        onClick={() => {
          this.props.pickPlayer(
            this.props.player.id,
            this.props.teamId,
            this.props.player.role,
            this.props.player.firstName,
            this.props.player.lastName,
            this.props.player.summonerName,
            this.props.teamImg,
            this.props.player.image,
            1
          );
        }}
      >
        <div className="playerWrapper">
          <div className="playerRole">
            {console.log(this.props.player)}
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
      </div>
    );
  }
}

SelectPlayer.propTypes = {
  pickPlayer: PropTypes.func.isRequired,
  user: PropTypes.object.isRequired,
};

const mapStateToProps = (state) => ({
  user: state.users.user,
});

export default connect(mapStateToProps, { pickPlayer })(SelectPlayer);
