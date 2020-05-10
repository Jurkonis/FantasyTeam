import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchMyTeam } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MyFantasyTeamPlayer from "./MyFantasyTeamPlayer.js";
import MessageList from "../Messages/MessagesList.js";

class MyFantasyTeam extends Component {
  componentDidMount() {
    this.props.fetchMyTeam();
  }

  render() {
    let team = this.props.team.map((player, index) => (
      <li key={player.id}>
        <MyFantasyTeamPlayer id={player.id} playerId={player.playerId} index={index} />
      </li>
    ));
    return (
      <div className="fantasyTeamWrapper">
        <MessageList />
        <h1>My team</h1>
        <div className="fantasyTeamList">{team}</div>
      </div>
    );
  }
}

MyFantasyTeam.propTypes = {
  fetchMyTeam: PropTypes.func.isRequired,
  team: PropTypes.array.isRequired,
};

const mapStateToProps = (state) => ({
  team: state.users.fantasyTeam,
});

export default connect(mapStateToProps, { fetchMyTeam })(MyFantasyTeam);
