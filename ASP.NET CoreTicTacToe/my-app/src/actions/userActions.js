import * as users from '../constants/userConstants';
import { userService } from '../services/userService';
import { alertActions } from '../actions/alertActions';
import { history } from '../helpers/history';

function login(username, password)
{
    return dispatch => {
        dispatch(userLoginRequest({ username }));
        userService.login(username, password)
            .then(
                user => {
                    dispatch(userLoginSuccess(user));
                    history.push('/');
                },
                error => {
                    dispatch(userLoginFailure(error));
                    dispatch(alertActions.alertError(error));
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
        return {
            type: users.USER_LOGIN_REQUEST,
            user
        }
    }

    function userLoginSuccess(user)
    {
        return {
            type: users.USER_LOGIN_SUCCESS,
            user
        }
    }
}

function userLogout()
{
    userService.logout();
    return {
        type: users.USER_LOGOUT
    };
}

function getAll()
{
    return dispatch => {
        dispatch(userGetallRequest());
        userService.getAll()
            .then(
                users => dispatch(userGetallSuccess(users)),
                error => {
                    dispatch(userGetallFailure(error));
                    dispatch(alertActions.alertError(error));
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