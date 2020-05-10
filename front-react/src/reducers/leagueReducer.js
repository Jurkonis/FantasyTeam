import { FETCH_LEAGUES, FETCH_MATCHES, FETCH_TEAM, FETCH_RECENT_MATCHES, FETCH_PLAYINGTEAMS, LOADING_STATE, LEAGUE_NAME } from "../actions/types";

const initialState = {
  leagues: [],
  matches: [],
  recentMatches: [],
  team: { players: [] },
  playingTeams: [],
  loaded: false,
  league: "",
};

export default function (state = initialState, action) {
  switch (action.type) {
    case FETCH_LEAGUES:
      return {
        ...state,
        leagues: action.payload,
      };
    case FETCH_MATCHES:
      return {
        ...state,
        matches: action.payload,
      };
    case FETCH_RECENT_MATCHES:
      return {
        ...state,
        recentMatches: action.payload,
        loaded: true,
      };
    case FETCH_TEAM:
      return {
        ...state,
        team: action.payload,
        loaded: true,
      };
    case FETCH_PLAYINGTEAMS:
      return {
        ...state,
        playingTeams: action.payload,
      };
    case LOADING_STATE:
      return {
        ...state,
        loaded: action.payload,
      };
    case LEAGUE_NAME:
      return {
        ...state,
        league: action.payload,
      };
    default:
      return state;
  }
}
