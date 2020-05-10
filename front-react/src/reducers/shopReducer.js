import { FETCH_SHOP } from "../actions/types";

const initialState = {
  shop: [],
};

export default function (state = initialState, action) {
  switch (action.type) {
    case FETCH_SHOP:
      return {
        ...state,
        shop: action.payload,
      };
    default:
      return state;
  }
}
