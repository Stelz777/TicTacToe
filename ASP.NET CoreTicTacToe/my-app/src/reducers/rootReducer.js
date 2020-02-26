import { combineReducers } from 'redux';
import alertReducer from './alertReducer';
import authenticationReducer from './authenticationReducer';
import botReducer from './botReducer'
import commonReducer from './commonReducer';
import historyReducer from './historyReducer';
import usersReducer from './usersReducer';

const rootReducer = combineReducers({
    alertReducer: alertReducer,
    authenticationReducer: authenticationReducer,
    botReducer: botReducer,
    commonReducer: commonReducer,
    historyReducer: historyReducer,
    usersReducer: usersReducer
});

export default rootReducer;