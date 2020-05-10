import React, { Component } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { fetchTournaments } from "../../actions/tournamentActions.js";
import PropTypes from "prop-types";
import TournamentDetails from "./TournamentDetails.js";
import MessageList from "../Messages/MessagesList.js";

export class TournamentList extends Component {
  componentDidMount() {
    this.props.fetchTournaments();
  }
  render() {
    let tournament = this.props.tournaments.map((tourn, index) => (
      <li key={tourn.id}>
        <TournamentDetails tournament={tourn} index={index} />
      </li>
    ));
    return (
      <div>
        <MessageList />
        {this.props.loaded ? (
          <div>
            <div className="buttonTournaments">
              {window.sessionStorage.getItem("admin") === "true" || window.sessionStorage.getItem("admin") === true ? (
                <div className="addButton">
                  <Link to="/AddTournament">Add tournament</Link>
                </div>
              ) : (
                ""
              )}
              <h3>Tournaments</h3>
            </div>
            <ul className="tournamentList">{tournament}</ul>
          </div>
        ) : (
          <div className="loading">
            <h3>Loading...</h3>
          </div>
        )}
      </div>
    );
  }
}

TournamentList.propTypes = {
  fetchTournaments: PropTypes.func.isRequired,
  tournaments: PropTypes.array.isRequired,
  loaded: PropTypes.bool.isRequired,
};

const mapStateToProps = (state) => ({
  tournaments: state.tournament.tournaments,
  loaded: state.tournament.loaded,
});

export default connect(mapStateToProps, { fetchTournaments })(TournamentList);
