import {
  FETCH_TOURNAMENTS,
  FETCH_TOURNAMENT,
  ADD_TOURNAMENT,
  DELETE_TOURNAMENT,
  END_TOURNAMENT,
  TEAMS_IN_TOURNAMENT,
  REGISTER_TOURNAMENT,
  REGISTERED_USERS,
  LOADING_STATE,
  ADD_FLASH_MESSAGE,
} from "./types";
import Axios from "axios";

const config = {
  headers: { Authorization: "bearer " + window.sessionStorage.getItem("token") },
};

export const fetchTournaments = () => (dispatch) => {
  dispatch({ type: LOADING_STATE, payload: false });
  Axios.get(`http://localhost:5001/api/FantasyTournaments/`)
    .then((res) => {
      dispatch({ type: FETCH_TOURNAMENTS, payload: res.data });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const fetchTournament = (id) => (dispatch) => {
  dispatch({ type: LOADING_STATE, payload: false });
  Axios.get(`http://localhost:5001/api/FantasyTournaments/` + id)
    .then((res) => {
      dispatch({ type: FETCH_TOURNAMENT, payload: res.data });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const addTournament = (Name, StartTime, Endtime) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/FantasyTournaments/`, { Name, StartTime, Endtime }, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Tournament added!" } });
      dispatch({ type: ADD_TOURNAMENT });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to add tournament add" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const deleteTournament = (id, index) => (dispatch) => {
  Axios.delete(`http://localhost:5001/api/FantasyTournaments/` + id, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Tournament deleted!" } });
      dispatch({ type: DELETE_TOURNAMENT, index: index });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to delete tournament add" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const endTournament = (id) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/FantasyTournaments/End/` + id, {}, config)
    .then((res) => {
      dispatch({ type: END_TOURNAMENT });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const tournamentTeams = (id) => (dispatch) => {
  Axios.get(`http://localhost:5001/api/FantasyTournaments/` + id + "/Teams")
    .then((res) => {
      dispatch({ type: TEAMS_IN_TOURNAMENT, payload: res.data });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const joinTournament = (tournamentId) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/FantasyTournaments/Register/` + window.sessionStorage.getItem("id") + "/" + tournamentId, {}, config)
    .then((res) => {
      dispatch({ type: REGISTER_TOURNAMENT });
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Successfully registered to the tournament!!" } });
    })
    .catch((error) => {
      if (error.response) {
        dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: error.response.data } });
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const registeredUsers = (tournamentId) => (dispatch) => {
  Axios.get(`http://localhost:5001/api/FantasyTournaments/` + tournamentId + `/RegisteredUsers`, config)
    .then((res) => {
      dispatch({ type: REGISTERED_USERS, payload: res.data });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const updateTournament = (Id, Name, StartTime, Endtime) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/FantasyTournaments/` + Id, { Id, Name, StartTime, Endtime }, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Tournament has been updated!" } });
      window.location.href = "/Tournament/Details/" + Id;
    })
    .catch((error) => {
      if (error.response) {
        dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to update!" } });
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};
