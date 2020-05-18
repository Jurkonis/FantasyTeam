import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchTeam } from "../../actions/leagueActions.js";
import PropTypes from "prop-types";
import SelectPlayer from "./SelectPlayer";
import MessageList from "../Messages/MessagesList.js";

class SelectPlayerList extends Component {
  componentDidMount() {
    this.props.fetchTeam(this.props.match.params.tid);
  }
  render() {
    let players = this.props.team.players.map((p) => (
      <SelectPlayer key={p.id} teamId={this.props.team.id} player={p} teamImg={this.props.team.image} roleImg={"../Images/Roles/" + p.role + ".png"} />
    ));
    return (
      <div>
        <MessageList />
        <div>
          <h1>{this.props.match.params.tid}</h1>
        </div>
        <div className="teamPlayersList">
          {this.props.loaded ? (
            players
          ) : (
            <div className="loading">
              <h3>Loading...</h3>
            </div>
          )}
        </div>
      </div>
    );
  }
}

SelectPlayerList.propTypes = {
  fetchTeam: PropTypes.func.isRequired,
  team: PropTypes.object.isRequired,
  loaded: PropTypes.bool.isRequired,
};

const mapStateToProps = (state) => ({
  team: state.leagues.team,
  loaded: state.leagues.loaded,
});

export default connect(mapStateToProps, { fetchTeam })(SelectPlayerList);
