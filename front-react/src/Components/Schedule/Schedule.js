import React, { Component } from "react";
import League from "./League.js";
import Match from "./Match.js";
import { connect } from "react-redux";
import { fetchLeagues, fetchMatches } from "../../actions/leagueActions.js";
import PropTypes from "prop-types";

class Schedule extends Component {
  state = {
    bool: true,
  };
  componentDidMount() {
    this.props.fetchLeagues("false");
  }

  upcoming() {
    this.setState({ bool: true });
  }

  recent() {
    this.setState({ bool: false });
  }

  render() {
    let name = this.state.bool ? "myActiveButton" : "myButton2";
    let name2 = this.state.bool ? "myButton2" : "myActiveButton";
    let league = this.props.leagues.map((leag) => (
      <li
        key={leag.id}
        onClick={() => {
          this.props.fetchMatches(leag.id, leag.name, "false");
        }}
      >
        <League id={leag.id} name={leag.name} slug={leag.slug} image={leag.image} region={leag.region} bool={leag.name !== this.props.league ? true : false} />
      </li>
    ));

    let match = this.props.matches.map((m) => (
      <li key={m.match.id}>
        <Match match={m} />
      </li>
    ));

    let recentMatch = this.props.recentMatches.map((m) => (
      <li key={m.match.id}>
        <Match match={m} />
      </li>
    ));
    return (
      <div className="Schedule">
        <ul className="matchList">
          <div className="matchTypes">
            <div className={name} onClick={this.upcoming.bind(this)}>
              Upcoming
            </div>
            <div className={name2} onClick={this.recent.bind(this)}>
              Recent
            </div>
          </div>
          {this.props.loaded ? (
            this.state.bool === false ? (
              recentMatch
            ) : (
              match
            )
          ) : (
            <div className="loading">
              <h3>Loading...</h3>
            </div>
          )}
        </ul>
        <ul className="leagueList">{league}</ul>
      </div>
    );
  }
}

Schedule.propTypes = {
  fetchLeagues: PropTypes.func.isRequired,
  fetchMatches: PropTypes.func.isRequired,
  leagues: PropTypes.array.isRequired,
  recentMatches: PropTypes.array.isRequired,
  matches: PropTypes.array.isRequired,
  loaded: PropTypes.bool.isRequired,
  league: PropTypes.string.isRequired,
};

const mapStateToProps = (state) => ({
  leagues: state.leagues.leagues,
  matches: state.leagues.matches,
  recentMatches: state.leagues.recentMatches,
  loaded: state.leagues.loaded,
  league: state.leagues.league,
});

export default connect(mapStateToProps, { fetchLeagues, fetchMatches })(Schedule);
