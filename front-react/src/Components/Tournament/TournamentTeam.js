import React, { Component } from "react";
import Axios from "axios";
import { Link } from "react-router-dom";

export class TournamentTeam extends Component {
  state = {
    team: [],
  };

  componentDidMount() {
    Axios.get("http://localhost:5001/api/FantasyTournaments/Team/" + this.props.id).then((response) => {
      this.setState({
        team: response.data,
      });
    });
  }

  render() {
    return (
      <div className="tournamentTeamDetails">
        <Link to={`/Team/${this.state.team.name}`} style={{ textDecoration: "none" }}>
          <div className="teamImg">
            <img key={this.state.team.id} src={this.state.team.image} alt="team" />
          </div>
          <div className="teamName">{this.state.team.name}</div>
        </Link>
      </div>
    );
  }
}

export default TournamentTeam;
