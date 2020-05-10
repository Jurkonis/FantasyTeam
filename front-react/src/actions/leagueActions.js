import { FETCH_LEAGUES, FETCH_MATCHES, FETCH_TEAM, FETCH_RECENT_MATCHES, LOADING_STATE, LEAGUE_NAME } from "./types";
import axios from "axios";

export const fetchLeagues = () => (dispatch) => {
  axios.get(`http://localhost:5001/api/leagues`).then((res) => {
    dispatch({ type: FETCH_LEAGUES, payload: res.data });
    dispatch({ type: LEAGUE_NAME, payload: res.data[0].name });
    let id = res.data[0].id;
    axios
      .get(`http://localhost:5001/api/leagues/schedule/` + id + "/true")
      .then((res) => {
        dispatch({ type: FETCH_MATCHES, payload: res.data });
        axios
          .get(`http://localhost:5001/api/leagues/schedule/` + id + "/false")
          .then((res) => {
            dispatch({ type: FETCH_RECENT_MATCHES, payload: res.data });
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
  });
};

export const fetchMatches = (id, name) => (dispatch) => {
  dispatch({ type: LOADING_STATE, payload: false });
  dispatch({ type: LEAGUE_NAME, payload: name });
  axios
    .get(`http://localhost:5001/api/leagues/schedule/` + id + "/true")
    .then((res) => {
      dispatch({ type: FETCH_MATCHES, payload: res.data });
      axios
        .get(`http://localhost:5001/api/leagues/schedule/` + id + "/false")
        .then((res) => {
          dispatch({ type: FETCH_RECENT_MATCHES, payload: res.data });
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

export const fetchTeam = (id) => (dispatch) => {
  dispatch({ type: LOADING_STATE, payload: false });
  axios
    .get(`http://localhost:5001/api/leagues/team/` + id)
    .then((res) => {
      dispatch({ type: FETCH_TEAM, payload: res.data });
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

/*export const fetchPlayingTeams = (id, bool) => (dispatch) => {
  axios
    .get(`http://localhost:5001/api/leagues/schedule/` + id + "/teams", { bool })
    .then((res) => {
      dispatch({ type: FETCH_PLAYINGTEAMS, payload: res.data });
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
};*/

export const loading = () => (dispatch) => {
  dispatch({ type: LOADING_STATE, payload: "true" });
};
