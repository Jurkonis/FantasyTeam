import { FETCH_SHOP, ADD_FLASH_MESSAGE } from "./types.js";
import Axios from "axios";

const config = {
  headers: { Authorization: "bearer " + window.sessionStorage.getItem("token") },
};

export const fetchShop = () => (dispatch) => {
  Axios.get(`http://localhost:5001/api/Icons`, config)
    .then((res) => {
      dispatch({ type: FETCH_SHOP, payload: res.data });
    })
    .catch((error) => {
      // Error
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
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

export const buyIcon = (iconId) => (dispatch) => {
  Axios.post(`http://localhost:5001/api/Users/BuyIcon/` + window.sessionStorage.getItem("id") + "/" + iconId, {}, config)
    .then((res) => {
      window.sessionStorage.setItem("coins", res.data);
      dispatch({ type: ADD_FLASH_MESSAGE, message: { type: "success", text: "Thank you for purchasing" } });
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
