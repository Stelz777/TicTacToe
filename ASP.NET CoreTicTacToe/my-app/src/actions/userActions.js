import * as users from '../constants/userConstants';
import { getAll, login, logout } from '../services/userService';
import { alertError } from '../actions/alertActions';
import { history } from '../helpers/history';

export function userLogin(username, password)
{
    return dispatch => {
        dispatch(userLoginRequest({ username }));
        login(username, password)
            .then(
                user => {
                    dispatch(userLoginSuccess(user));
                    history.push('/');
                },
                error => {
                    dispatch(userLoginFailure(error));
                    dispatch(alertError(error));
                }
            );
    }

    function userLoginFailure(error)
    {
        return {
            type: users.USER_LOGIN_FAILURE,
            error
        }
    }

    function userLoginRequest(user)
    {
        console.log("userLoginRequest!");
        return {
            type: users.USER_LOGIN_REQUEST,
            user
        }
    }

    function userLoginSuccess(user)
    {
        console.log("userLoginSuccess user: ", user);
        return {
            type: users.USER_LOGIN_SUCCESS,
            user
        }
    }
}

export function userLogout()
{
    logout();
    return {
        type: users.USER_LOGOUT
    };
}

export function userGetAll()
{
    return dispatch => {
        dispatch(userGetallRequest());
        getAll()
            .then(
                users => dispatch(userGetallSuccess(users)),
                error => {
                    dispatch(userGetallFailure(error));
                    dispatch(alertError(error));
                }
            );
    };

    function userGetallFailure(error)
    {
        return {
            type: users.USER_GETALL_FAILURE,
            error
        }
    }

    function userGetallRequest()
    {
        return {
            type: users.USER_GETALL_REQUEST
        }
    }

    function userGetallSuccess(users)
    {
        return {
            type: users.USER_GETALL_SUCCESS,
            users
        }
    }
}

export function userNameSetInLobby(playerNameInLobby)
{
    return {
        type: users.USER_NAME_SET_IN_LOBBY,
        playerNameInLobby
    }
}