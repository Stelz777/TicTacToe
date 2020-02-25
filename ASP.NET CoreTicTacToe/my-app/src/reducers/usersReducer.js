import { USER_GETALL_FAILURE, USER_GETALL_REQUEST, USER_GETALL_SUCCESS } from '../constants/userConstants';

export function usersReducer(state = {}, action)
{
    switch (action.type)
    {
        case USER_GETALL_FAILURE:
            return {
                error: action.error
            };
        case USER_GETALL_REQUEST:
            return {
                loading: true
            };
        case USER_GETALL_SUCCESS:
            return {
                items: action.users
            };
        default:
            return state;
    }
}

export default usersReducer;