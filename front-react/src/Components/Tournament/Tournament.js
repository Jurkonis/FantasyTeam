import React, { Component } from "react";
import { fetchTournament, tournamentTeams, joinTournament, registeredUsers, endTournament, updateTournament } from "../../actions/tournamentActions.js";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import Moment from "moment";
import Team from "./TournamentTeam.js";
import User from "./TournamentUser.js";
import MessageList from "../Messages/MessagesList.js";
import { Modal } from "reactstrap";
import DatePicker from "react-datepicker";

export class Tournament extends Component {
  state = {
    editTournamentData: {
      id: 0,
      name: "",
      startTime: "",
      endTime: "",
    },
    editTournamentModal: false,
  };

  componentDidMount() {
    this.props.fetchTournament(this.props.match.params.tid);
    this.props.tournamentTeams(this.props.match.params.tid);
    this.props.registeredUsers(this.props.match.params.tid);
  }

  toggleEditTournamentModal() {
    this.setState({
      editTournamentModal: !this.state.editTournamentModal,
    });
  }

  editTournament(id, name, startTime, endTime) {
    this.setState({
      editTournamentData: { id, name, startTime, endTime },
      editTournamentModal: !this.state.editTournamentModal,
    });
  }

  handleClick() {
    this.props.joinTournament(this.props.match.params.tid);
  }

  handleUpdate() {
    let { id, name, startTime, endTime } = this.state.editTournamentData;
    this.props.updateTournament(id, name, startTime, endTime);
    this.setState({
      editTournamentModal: !this.state.editTournamentModal,
    });
  }

  endTournament() {
    this.props.endTournament(this.props.match.params.tid);
  }

  render() {
    if (window.sessionStorage.getItem("id") === null) window.location.href = "/Login";
    let date = Moment().format();

    let tourn = this.props.tournament;
    let teams = this.props.teams.map((team) => (
      <li key={team.id}>
        <Team id={team.teamId} />
      </li>
    ));
    let users = this.props.users.map((user, index) => (
      <li key={user.id}>
        <User id={user.userId} points={user.points} endTime={tourn.endTime} index={index + 1} />
      </li>
    ));

    return (
      <div>
        <Modal isOpen={this.state.editTournamentModal} toggle={this.toggleEditTournamentModal.bind(this)}>
          <div className="header">
            <div className="left">Update info</div>
            <div className="right" onClick={this.toggleEditTournamentModal.bind(this)}>
              &times;
            </div>
          </div>
          <div className="body">
            <div>
              <label>Name</label>
              <div>
                <input
                  className="username"
                  placeholder="Name"
                  value={this.state.editTournamentData.name}
                  onChange={(e) => {
                    let { editTournamentData } = this.state;
                    editTournamentData.name = e.target.value;
                    this.setState({ editTournamentData });
                  }}
                />
              </div>
              <label>Start time</label>
              <div>
                <DatePicker
                  className="timeAdd2"
                  placeholderText="Start time"
                  value={Moment(this.state.editTournamentData.startTime).format("l")}
                  onChange={(date) => {
                    let { editTournamentData } = this.state;
                    editTournamentData.startTime = date;
                    this.setState({ editTournamentData });
                  }}
                />
              </div>
              <label>End time</label>
              <div>
                <DatePicker
                  className="timeAdd2"
                  placeholderText="End time"
                  value={Moment(this.state.editTournamentData.endTime).format("l")}
                  onChange={(date) => {
                    let { editTournamentData } = this.state;
                    editTournamentData.endTime = date;
                    this.setState({ editTournamentData });
                  }}
                />
              </div>
            </div>
          </div>
          <div className="footer">
            <div className="btn" onClick={this.handleUpdate.bind(this)}>
              Update
            </div>
            <div className="btn" onClick={this.toggleEditTournamentModal.bind(this)}>
              Cancel
            </div>
          </div>
        </Modal>
        {this.props.loaded ? (
          <div style={{ margin: "100px 0 0 0 " }}>
            <MessageList />
            <div className="tournamentWrapper">
              <div className="tournamentNameInDetails">
                <h3>
                  <b>{tourn.name}</b>
                </h3>
              </div>
              <div className="time">
                <div>
                  <h5>Start time: {Moment(tourn.startTime).format("MMMM Do , h:mm A")}</h5>
                </div>
                <div>
                  <h5>End time : {Moment(tourn.endTime).format("MMM Do, h:mm A")}</h5>
                </div>
              </div>
              <div className="tournamentDetailsButtonsToLeft">
                {window.sessionStorage.getItem("admin") === "true" || window.sessionStorage.getItem("admin") === true ? (
                  tourn.startTime > date ? (
                    <div className="myButton" onClick={this.editTournament.bind(this, tourn.id, tourn.name, tourn.startTime, tourn.endTime)}>
                      Edit
                    </div>
                  ) : (
                    ""
                  )
                ) : (
                  ""
                )}
                {tourn.startTime > date ? (
                  <div className="myButton" onClick={this.handleClick.bind(this)}>
                    Register
                  </div>
                ) : (
                  <div className="myButton" onClick={this.handleClick.bind(this)}>
                    Register for delete
                  </div>
                )}
                {window.sessionStorage.getItem("admin") === "true" || window.sessionStorage.getItem("admin") === true ? (
                  tourn.startTime < date ? (
                    <div className="myButton" onClick={this.endTournament.bind(this)}>
                      End it
                    </div>
                  ) : (
                    ""
                  )
                ) : (
                  ""
                )}
              </div>
            </div>
            <div className="playingTeamsAndUsers">
              <div className="playingTeams">
                <h5>Playing teams</h5>
                <div>
                  <ul>{teams}</ul>
                </div>
              </div>
              <div className="registeredUsers">
                <h5>Registered users</h5>
                <div>{users}</div>
              </div>
              <div className="container">
                <h5>Rewards</h5>
                <div className="reward">
                  <div className="left">1st</div>
                  <div className="right">1000 coins</div>
                </div>
                <div className="reward">
                  <div className="left">2nd</div>
                  <div className="right">500 coins</div>
                </div>
                <div className="reward">
                  <div className="left">3rd</div>
                  <div className="right">400 coins</div>
                </div>
                <div className="reward">
                  <div className="left">4 - 10</div>
                  <div className="right">200 coins</div>
                </div>
                <div className="reward">
                  <div className="left">11 - 25</div>
                  <div className="right">150 coins</div>
                </div>
                <div className="reward">
                  <div className="left">26 - 50</div>
                  <div className="right">100 coins</div>
                </div>
              </div>
            </div>
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

Tournament.propTypes = {
  fetchTournament: PropTypes.func.isRequired,
  endTournament: PropTypes.func.isRequired,
  tournamentTeams: PropTypes.func.isRequired,
  joinTournament: PropTypes.func.isRequired,
  registeredUsers: PropTypes.func.isRequired,
  updateTournament: PropTypes.func.isRequired,
  tournament: PropTypes.object.isRequired,
  teams: PropTypes.array.isRequired,
  users: PropTypes.array.isRequired,
  loaded: PropTypes.bool.isRequired,
};

const mapStateToProps = (state) => ({
  tournament: state.tournament.tournament,
  teams: state.tournament.teamsInTournament,
  users: state.tournament.usersInTournament,
  loaded: state.tournament.loaded,
});

export default connect(mapStateToProps, { fetchTournament, tournamentTeams, joinTournament, registeredUsers, endTournament, updateTournament })(Tournament);
