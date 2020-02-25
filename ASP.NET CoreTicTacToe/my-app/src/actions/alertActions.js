import * as alerts from '../constants/alertConstants';

export function alertClear() {
    return {
        type: alerts.ALERT_CLEAR
    }
}

export function alertError(message) {
    return {
        type: alerts.ALERT_ERROR,
        message
    }
}

export function alertSuccess(message) {
    return {
        type: alerts.ALERT_SUCCESS,
        message
    }
}



