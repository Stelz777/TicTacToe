import { USER_REGISTER_FAILURE, USER_REGISTER_REQUEST, USER_REGISTER_SUCCESS } from '../constants/userConstants';

let user = JSON.parse(localStorage.getItem('user'));
const intitialState = user ? { loggedIn: true, user } : {};

export function registerReducer(state = intitialState, action)
{
    switch (action.type)
    {
        case USER_REGISTER_FAILURE:
            return {};
        case USER_REGISTER_REQUEST:
            return {
                loggingIn: true,
                user: action.user
            };
        case USER_REGISTER_SUCCESS:
            return {
                loggedIn: true,
                user: action.user,
                lobbyPlayerName: action.user.name
            };
        default:
            return state;
    }
}

export default registerReducer;