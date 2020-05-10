import { combineReducers } from "redux";
import leagueReducer from "./leagueReducer.js";
import userReducer from "./userReducer.js";
import tournamentReducer from "./tournamentReducer.js";
import shopReducer from "./shopReducer.js";
import messages from "./messagesReducer.js";

export default combineReducers({
  leagues: leagueReducer,
  users: userReducer,
  tournament: tournamentReducer,
  shop: shopReducer,
  messages,
});
