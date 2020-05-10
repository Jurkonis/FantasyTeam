import React, { Component } from "react";

class League extends Component {
  render() {
    const pav = this.props.bool ? "league" : "leagueActive";
    return (
      <span>
        <div className={pav}>
          <div className="leagueImg">
            <img src={this.props.image} alt="league" />
          </div>
          <div className="leagueDetails">
            <p>{this.props.name}</p>
            <p>{this.props.region}</p>
          </div>
        </div>
      </span>
    );
  }
}

export default League;
