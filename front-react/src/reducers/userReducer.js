import { POST_USER, PICK_PLAYER, DELETE_PLAYER, GET_FANTASY_TEAM, FETCH_PLAYER, FETCH_ICONS } from "../actions/types";

const initialState = {
  user: {},
  players: [],
  fantasyTeam: [],
  myIcons: [],
};

export default function (state = initialState, action) {
  switch (action.type) {
    case GET_FANTASY_TEAM:
      return {
        ...state,
        fantasyTeam: action.payload,
      };
    case DELETE_PLAYER:
      return {
        ...state,
        fantasyTeam: state.fantasyTeam.filter((item, index) => index !== action.index),
      };
    case POST_USER:
      return {
        ...state,
      };
    case PICK_PLAYER:
      return {
        ...state,
      };
    case FETCH_PLAYER:
      return {
        ...state,
        players: action.payload,
      };
    case FETCH_ICONS:
      return {
        ...state,
        myIcons: action.payload,
      };
    default:
      return state;
  }
}
