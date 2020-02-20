import { combineReducers } from 'redux';
import botReducer from './botReducer'
import commonReducer from './commonReducer';
import historyReducer from './historyReducer';
import nameReducer from './nameReducer';

const rootReducer = combineReducers({
    botReducer: botReducer,
    commonReducer: commonReducer,
    historyReducer: historyReducer,
    nameReducer: nameReducer
});

export default rootReducer;