import { USER_LOGIN_FAILURE, USER_LOGIN_REQUEST, USER_LOGIN_SUCCESS, USER_LOGOUT } from '../constants/userConstants';

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
                user: action.user
            };
        case USER_LOGOUT:
            return {};
        default:
            return state;
    }
}

export default authenticationReducer;