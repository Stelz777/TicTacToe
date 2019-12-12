import { combineReducers } from 'redux';
import game from './game';
import history from './history';

const reducer = combineReducers({
        game,
        history
    }
);

export default reducer;