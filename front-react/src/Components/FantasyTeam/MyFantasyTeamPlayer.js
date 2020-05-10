import React, { Component } from "react";
import { connect } from "react-redux";
import { deletePlayer, pickPlayer } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import Axios from "axios";
import top from "../../Images/Roles/top.png";
import jungle from "../../Images/Roles/jungle.png";
import mid from "../../Images/Roles/mid.png";
import bottom from "../../Images/Roles/bottom.png";
import support from "../../Images/Roles/support.png";

class MyFantasyTeamPlayer extends Component {
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
  state = {
    player: {},
    config: {
      headers: { Authorization: "bearer " + sessionStorage.getItem("token") },
    },
  };

  componentDidMount() {
    this.getPlayer();
  }

  getPlayer() {
    Axios.get("http://localhost:5001/api/UsersFantasyTeams/player/" + this.props.playerId, this.state.config)
      .then((response) => {
        this.setState({
          player: response.data,
        });
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
    if (this.props.remove === "false") {
      this.props.pickPlayer(
        this.state.player.teamId,
        this.state.player.role,
        this.state.player.firstName,
        this.state.player.secondName,
        this.state.player.username,
        this.state.player.image,
        this.state.player.logo
      );
    } else this.props.deletePlayer(this.props.id, this.props.index);
  }

  render() {
    let player = this.state.player;

    return (
      <div className="fantasyTeamPlayer">
        <div className="role">
          <img src={this.renderSwitch(player.role)} alt="role" />
        </div>
        <div className="fantasyTeamPlayerImg">
          <img src={player.logo} alt="img" />
        </div>
        <h2>{player.username}</h2>
        <h6>
          {player.firstName} {player.secondName}
        </h6>
        <div className="fantasyTeamPlayerLogo">
          <img src={player.image} alt="logo" />
        </div>
        <div className="myButtonInFantasyPlayer" onClick={this.handleClick.bind(this)}>
          {this.props.remove === "false" ? "Add to my team" : "Remove"}
        </div>
      </div>
    );
  }
}

MyFantasyTeamPlayer.propTypes = {
  deletePlayer: PropTypes.func.isRequired,
  pickPlayer: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { deletePlayer, pickPlayer })(MyFantasyTeamPlayer);
