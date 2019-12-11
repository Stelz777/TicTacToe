import { combineReducers } from 'redux';
import GameReducer from './gameReducer'

const rootReducer = combineReducers(
    {
        
        
        gameReducer: GameReducer
    }
);

export default rootReducer;