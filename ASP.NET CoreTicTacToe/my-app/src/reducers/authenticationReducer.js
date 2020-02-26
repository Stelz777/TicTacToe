import { USER_LOGIN_FAILURE, USER_LOGIN_REQUEST, USER_LOGIN_SUCCESS, USER_NAME_SET_IN_LOBBY, USER_LOGOUT } from '../constants/userConstants';

let user = JSON.parse(localStorage.getItem('user'));
const intitialState = user ? { loggedIn: true, user } : {};

export function authenticationReducer(state = intitialState, action)
{
    switch (action.type)
    {
        case USER_LOGIN_FAILURE:
            return {};
        case USER_LOGIN_REQUEST:
            return {
                loggingIn: true,
                user: action.user
            };
        case USER_LOGIN_SUCCESS:
            return {
                loggedIn: true,
                user: action.user,
                lobbyPlayerName: action.user.name
            };
        case USER_LOGOUT:
            return {};
        case USER_NAME_SET_IN_LOBBY:
            return ({
                ...state,
                lobbyPlayerName: action.playerNameInLobby
            });
        default:
            return state;
    }
}

export default authenticationReducer;