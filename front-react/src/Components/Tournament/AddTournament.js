import React, { Component } from "react";
import DatePicker from "react-datepicker";
import { connect } from "react-redux";
import { addTournament } from "../../actions/tournamentActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";

import "react-datepicker/dist/react-datepicker.css";

export class AddTournament extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tournamentData: {
        Name: "",
        StartTime: "",
        EndTime: "",
      },
    };
  }

  render() {
    return (
      <div className="loginWrapper">
        <MessageList />
        <h5>Add tournament</h5>
        <div>
          <input
            className="username"
            placeholder="Name"
            value={this.state.tournamentData.Name}
            onChange={(e) => {
              let { tournamentData } = this.state;
              tournamentData.Name = e.target.value;
              this.setState({ tournamentData });
            }}
          />
        </div>
        <div>
          <DatePicker
            className="timeAdd"
            selected={this.state.tournamentData.StartTime}
            placeholderText="Start time"
            onChange={(date) => {
              let { tournamentData } = this.state;
              tournamentData.StartTime = date;
              this.setState({ tournamentData });
            }}
          />
        </div>
        <div>
          <DatePicker
            className="timeAdd"
            selected={this.state.tournamentData.EndTime}
            placeholderText="End time"
            onChange={(date) => {
              let { tournamentData } = this.state;
              tournamentData.EndTime = date;
              this.setState({ tournamentData });
            }}
          />
        </div>
        <div className="button">
          <div className="btn" onClick={() => this.props.addTournament(this.state.tournamentData.Name, this.state.tournamentData.StartTime, this.state.tournamentData.EndTime)}>
            Add
          </div>
        </div>
      </div>
    );
  }
}

AddTournament.propTypes = {
  addTournament: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { addTournament })(AddTournament);
