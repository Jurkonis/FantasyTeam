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
} from "../actions/types";

const initialState = {
  tournaments: [],
  tournament: {},
  teamsInTournament: [],
  usersInTournament: [],
  loaded: false,
};

export default function (state = initialState, action) {
  switch (action.type) {
    case FETCH_TOURNAMENTS:
      return {
        ...state,
        tournaments: action.payload,
        loaded: true,
      };
    case FETCH_TOURNAMENT:
      return {
        ...state,
        tournament: action.payload,
        loaded: true,
      };
    case ADD_TOURNAMENT:
      return {
        ...state,
      };
    case DELETE_TOURNAMENT:
      return {
        ...state,
        tournaments: state.tournaments.filter((item, index) => index !== action.index),
      };
    case END_TOURNAMENT:
      return {
        ...state,
      };
    case TEAMS_IN_TOURNAMENT:
      return {
        ...state,
        teamsInTournament: action.payload,
      };
    case REGISTER_TOURNAMENT:
      return {
        ...state,
      };
    case REGISTERED_USERS:
      return {
        ...state,
        usersInTournament: action.payload,
      };
    case LOADING_STATE:
      return {
        ...state,
        loaded: action.payload,
      };
    default:
      return state;
  }
}
