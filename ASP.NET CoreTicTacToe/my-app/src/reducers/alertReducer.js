import { ALERT_CLEAR, ALERT_ERROR, ALERT_SUCCESS } from '../constants/alertConstants';

export function alertReducer(state = {}, action)
{
    switch (action.type)
    {
        case ALERT_CLEAR:
            return {};
        case ALERT_ERROR:
            return {
                type: 'alert-danger',
                message: action.message
            };
        case ALERT_SUCCESS:
            return {
                type: 'alert-success',
                message: action.message
            }
        default:
            return state;
    }
}

export default alertReducer;