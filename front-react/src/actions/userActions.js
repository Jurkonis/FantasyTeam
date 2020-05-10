import { POST_USER, DELETE_PLAYER, PICK_PLAYER, FETCH_PLAYER, GET_FANTASY_TEAM, ADD_FLASH_MESSAGE, FETCH_ICONS } from "./types";
import Axios from "axios";

const config = {
  headers: { Authorization: "bearer " + window.sessionStorage.getItem("token") },
};

export const fetchUser = (Username, Password) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/users/authenticate`, { Username, Password })
    .then((res) => {
      if (res.data.tfa === true) {
        window.location.href = "/VerifyAuth";
        window.sessionStorage.setItem("usernameTemp", res.data.username);
        window.sessionStorage.setItem("idTemp", res.data.id);
        window.sessionStorage.setItem("tokenTemp", res.data.token);
        window.sessionStorage.setItem("coinsTemp", res.data.coins);
        window.sessionStorage.setItem("iconTemp", res.data.icon);
        window.sessionStorage.setItem("adminTemp", res.data.admin);
      } else {
        window.sessionStorage.setItem("username", res.data.username);
        window.sessionStorage.setItem("id", res.data.id);
        window.sessionStorage.setItem("token", res.data.token);
        window.sessionStorage.setItem("coins", res.data.coins);
        window.sessionStorage.setItem("icon", res.data.icon);
        window.sessionStorage.setItem("admin", res.data.admin);
        window.location.href = "/";
      }
    })
    .catch((error) => {
      // Error
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: error.response.data } });
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the
        // browser and an instance of
        // http.ClientRequest in node.js
        console.log(error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        console.log("Error", error.message);
      }
    });
};

export const postUser = (Username, Password) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/users/register`, { Username, Password })
    .then((res) => {
      dispatch({ type: POST_USER });
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "You singed up successfully. Welcome!" } });
    })
    .catch((error) => {
      if (error.response) {
        dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: error.response.data } });
        console.log(error.response);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const logout = () => {
  sessionStorage.clear();
  window.location.href = "/";
};

export const fetchPlayer = (id) => (dispatch) => {
  Axios.get(`http://localhost:5001/api/UsersFantasyTeams/player/` + id, config)
    .then((res) => {
      dispatch({ type: FETCH_PLAYER, payload: res.data });
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

export const pickPlayer = (Id, TeamId, Role, FirstName, SecondName, Username, Image, Logo) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/UsersFantasyTeams/` + window.sessionStorage.getItem("id"), { Id, TeamId, Role, FirstName, SecondName, Username, Image, Logo }, config)
    .then((res) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Player added to your team!" } });
      dispatch({ type: PICK_PLAYER });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to add player!" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const deletePlayer = (id, index) => (dispatch) => {
  Axios.delete(`http://localhost:5001/api/UsersFantasyTeams/` + id, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Player deleted from your team!" } });
      dispatch({ type: DELETE_PLAYER, index: index });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to delete player!" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const fetchUserTeam = (id) => (dispatch) => {
  Axios.get(`http://localhost:5001/api/UsersFantasyTeams/` + id, config)
    .then((res) => {
      dispatch({ type: GET_FANTASY_TEAM, payload: res.data });
    })
    .catch((error) => {
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const fetchMyTeam = () => (dispatch) => {
  Axios.get(`http://localhost:5001/api/UsersFantasyTeams/` + window.sessionStorage.getItem("id"), config)
    .then((res) => {
      dispatch({ type: GET_FANTASY_TEAM, payload: res.data });
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

export const updateUser = (FirstName, SecondName) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/Users/` + window.sessionStorage.getItem("id"), { FirstName, SecondName }, config)
    .then((res) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Information was updated!" } });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to upate your info!" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const changePassword = (OldPassword, NewPassword, NewPassword2) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/Users/ChangePassword/` + window.sessionStorage.getItem("id"), { OldPassword, NewPassword, NewPassword2 }, config)
    .then((res) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Password changed!" } });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed to change password!" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const enableAuth = () => (dispatch) => {
  Axios.get(`http://localhost:5001/api/Users/EnableAuth/` + window.sessionStorage.getItem("id"), config)
    .then((res) => {
      window.sessionStorage.setItem("tempData", res.data);
      window.location.href = "/EnableAuth";
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Failed with auth!" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const enableAuth2 = (Passcode) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/Users/EnableAuth/` + window.sessionStorage.getItem("id"), { Passcode }, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Auth enabled!" } });
      window.sessionStorage.removeItem("tempData");
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Wrong code" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const disable = () => {
  window.location.href = "/DisableAuth";
};

export const disableAuth = (Passcode) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/Users/DisableAuth/` + window.sessionStorage.getItem("id"), { Passcode }, config)
    .then(() => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Auth disabled" } });
    })
    .catch((error) => {
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "failure", text: "Wrong code" } });
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const verifyAuth = (Passcode) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/Users/VerifyAuth/` + window.sessionStorage.getItem("idTemp"), { Passcode }, config)
    .then((res) => {
      window.sessionStorage.setItem("username", window.sessionStorage.getItem("usernameTemp"));
      window.sessionStorage.setItem("id", window.sessionStorage.getItem("idTemp"));
      window.sessionStorage.setItem("token", window.sessionStorage.getItem("tokenTemp"));
      window.sessionStorage.setItem("coins", window.sessionStorage.getItem("coinsTemp"));
      window.sessionStorage.setItem("icon", window.sessionStorage.getItem("iconTemp"));
      window.sessionStorage.setItem("admin", window.sessionStorage.getItem("adminTemp"));
      window.location.href = "/";
    })
    .catch((error) => {
      window.sessionStorage.removeItem("usernameTemp");
      window.sessionStorage.removeItem("idTemp");
      window.sessionStorage.removeItem("tokenTemp");
      window.sessionStorage.removeItem("coinsTemp");
      window.sessionStorage.removeItem("iconTemp");
      window.sessionStorage.removeItem("adminTemp");
      window.location.href = "/Login";
      if (error.response) {
        console.log(error.response.data);
      } else if (error.request) {
        console.log(error.request);
      } else {
        console.log("Error", error.message);
      }
    });
};

export const fetchMyIcons = () => (dispatch) => {
  Axios.get(`http://localhost:5001/api/Users/Icons/` + window.sessionStorage.getItem("id"), config)
    .then((res) => {
      dispatch({ type: FETCH_ICONS, payload: res.data });
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

export const changeMyIcon = (name) => (dispatch) => {
  Axios.put(`http://localhost:5001/api/Users/Icons/` + window.sessionStorage.getItem("id") + "/" + name, {}, config)
    .then((res) => {
      window.location.href = "/Profile";
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
