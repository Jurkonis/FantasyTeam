import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchUserTeam } from "../../actions/userActions.js";
import PropTypes from "prop-types";
import MyFantasyTeamPlayer from "./MyFantasyTeamPlayer.js";
import MessageList from "../Messages/MessagesList.js";

class CompareFantasyTeams extends Component {
  componentDidMount() {
    this.props.fetchUserTeam(this.props.match.params.id);
  }

  render() {
    let team = this.props.team.map((player, index) => (
      <li key={player.id}>
        <MyFantasyTeamPlayer id={player.id} playerId={player.playerId} index={index} remove="false" />
      </li>
    ));
    return (
      <div>
        {" "}
        <MessageList />
        <div className="fantasyTeamWrapper">
          <h1>{this.props.match.params.username} team</h1>
          <div className="fantasyTeamList">{team}</div>
        </div>
      </div>
    );
  }
}

CompareFantasyTeams.propTypes = {
  fetchUserTeam: PropTypes.func.isRequired,
  team: PropTypes.array.isRequired,
};

const mapStateToProps = (state) => ({
  team: state.users.fantasyTeam,
});

export default connect(mapStateToProps, { fetchUserTeam })(CompareFantasyTeams);
