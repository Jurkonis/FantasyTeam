import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchTeam } from "../../actions/leagueActions.js";
import PropTypes from "prop-types";
import Player from "./Player";

class TeamDetails extends Component {
  componentDidMount() {
    this.props.fetchTeam(this.props.match.params.tid);
  }
  render() {
    let players = this.props.team.players.map((p) => <Player key={p.id} player={p} teamImg={this.props.team.image} roleImg={"../Images/Roles/" + p.role + ".png"} />);
    return (
      <div>
        <div>
          <h1>{this.props.match.params.tid}</h1>
        </div>
        <div className="teamPlayersList">{players}</div>
      </div>
    );
  }
}

TeamDetails.propTypes = {
  fetchTeam: PropTypes.func.isRequired,
  team: PropTypes.object.isRequired,
};

const mapStateToProps = (state) => ({
  team: state.leagues.team,
});

export default connect(mapStateToProps, { fetchTeam })(TeamDetails);
