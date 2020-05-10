import React, { Component } from "react";
import { Link } from "react-router-dom";
import Moment from "moment";

class Match extends Component {
  render() {
    let match = this.props.match;
    let winner = match.match.teams[0].result.outcome === "win" ? "resultLeft winner" : "resultLeft";
    let winner2 = match.match.teams[1].result.outcome === "win" ? "resultRight winner" : "resultRight";
    return (
      <div className="match">
        {console.log(match.match.teams[1].result)}
        <div className="matchTime">
          <h4>{Moment(match.startTime).format("MMMM DD  h:mm A")}</h4>
          <div className="matchResult">
            {match.state === "completed" ? (
              <div className="flex">
                <div className={winner}>{match.match.teams[0].result.gameWins}</div>
                <div className="matchStrategy"></div>
                <div className={winner2}>{match.match.teams[1].result.gameWins}</div>
              </div>
            ) : (
              ""
            )}
          </div>
          <h5>
            <b>{match.league.name}</b>
          </h5>
        </div>
        <div className="matchDetails">
          {match.match.teams[0].name !== "TBD" ? (
            <div className="matchTeamLeft">
              <Link to={`/Team/${match.match.teams[0].name.split(" ").join("-")}`}>
                {match.match.teams[0].name}
                <img src={match.match.teams[0].image} alt="match" />
              </Link>
            </div>
          ) : (
            <div className="matchTeamLeft">
              {match.match.teams[0].name}
              <img src={match.match.teams[0].image} alt="match" />
            </div>
          )}

          <div className="matchStrategy">BO{match.match.strategy.count}</div>
          {match.match.teams[1].name !== "TBD" ? (
            <div className="matchTeamRight">
              <Link to={`/Team/${match.match.teams[1].name.split(" ").join("-")}`}>
                <img src={match.match.teams[1].image} alt="match" />
                {match.match.teams[1].name}
              </Link>
            </div>
          ) : (
            <div className="matchTeamRight">
              {match.match.teams[1].name}
              <img src={match.match.teams[1].image} alt="match" />
            </div>
          )}
        </div>
      </div>
    );
  }
}

export default Match;
