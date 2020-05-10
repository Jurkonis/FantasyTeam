import React, { Component } from "react";
import Moment from "moment";
import { Link } from "react-router-dom";
import { deleteTournament } from "../../actions/tournamentActions.js";
import { connect } from "react-redux";
import PropTypes from "prop-types";

export class TournamentDetails extends Component {
  handleClick() {
    this.props.deleteTournament(this.props.tournament.id, this.props.index);
  }

  render() {
    let tourn = this.props.tournament;
    return (
      <div className="tournamentWrapper">
        <Link to={`/Tournament/details/${tourn.id}`} style={{ textDecoration: "none" }}>
          <div className="tournamentName">
            <h4>
              <b>{tourn.name}</b>
            </h4>
          </div>
        </Link>
        <div className="time">
          <div>Starts: {Moment(tourn.startTime).format("MMMM Do , h:mm A")}</div>
          <div>Ends: {Moment(tourn.startTime).format("MMMM Do , h:mm A")}</div>
        </div>
        <div className="tournamentDetailsButtonsToLeft">
          <div className="tournamentDetailsButton">
            <Link to={`/Tournament/details/${tourn.id}`} style={{ textDecoration: "none" }}>
              Details
            </Link>
          </div>
          {window.sessionStorage.getItem("admin") === "true" || window.sessionStorage.getItem("admin") === true ? (
            <div className="myButton" onClick={this.handleClick.bind(this)}>
              Remove
            </div>
          ) : (
            ""
          )}
        </div>
      </div>
    );
  }
}

TournamentDetails.propTypes = {
  deleteTournament: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { deleteTournament })(TournamentDetails);
