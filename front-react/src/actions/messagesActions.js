import { ADD_FLASH_MESSAGE, DELETE_MESSAGE } from "./types.js";

export function addFlashMessage(message) {
  return {
    type: ADD_FLASH_MESSAGE,
    message,
  };
}

export function deleteMessage(id) {
  return {
    type: DELETE_MESSAGE,
    id,
  };
}
