import React, { Component } from "react";
import Axios from "axios";
import { Link } from "react-router-dom";
import Moment from "moment";

export class TournamentUser extends Component {
  state = {
    user: {},
    config: {
      headers: { Authorization: "bearer " + window.sessionStorage.getItem("token") },
    },
  };

  componentDidMount() {
    Axios.get("http://localhost:5001/api/Users/" + this.props.id, this.state.config).then((response) => {
      this.setState({
        user: response.data,
      });
    });
  }
  render() {
    let date = Moment().format();
    return (
      <div className="flex">
        {this.props.endTime < date ? <div className="userPlace"> {this.props.index}</div> : ""}
        <div className="userName">
          <Link to={`/User/${this.state.user.id}/${this.state.user.username}/FantasyTeam`} style={{ textDecoration: "none" }}>
            {this.state.user.username}
          </Link>
        </div>
        {this.props.endTime < date ? <div className="userCoins"> {this.props.points} points</div> : ""}
      </div>
    );
  }
}

export default TournamentUser;
