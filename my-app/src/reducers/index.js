import { HISTORY_CHANGED } from '../actions';
import { HISTORY_CONCAT } from '../actions';
import { STEP_CHANGED } from '../actions';
import { STEP } from '../actions';
import { X_IS_NEXT_FLIP } from '../actions';
import { X_IS_NEXT_BY_STEP } from '../actions';
import { HIGHLIGHTS_CHANGED } from '../actions';
import { CHECKED_FLIP } from '../actions'; 
import { INIT } from '../actions';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    stepNumber: 0,
    xIsNext: true,
    highlights: Array(9).fill(false),
    checked: false,
}

function rootReducer(state = initialState, action)
{
    switch (action.type)
    {
        case INIT:
            return initialState;
        case HISTORY_CHANGED:
            return Object.assign({}, state, { history: state.history.slice().reverse() });
        case HISTORY_CONCAT:
            return Object.assign({}, state, { history: action.history.concat([{ squares: action.squares, }]) });
        case STEP_CHANGED:
            return Object.assign({}, state, { stepNumber: action.history.length });
        case STEP:
            return Object.assign({}, state, { stepNumber: action.stepInput });
        case X_IS_NEXT_FLIP:
            return Object.assign({}, state, { xIsNext: !state.xIsNext });
        case X_IS_NEXT_BY_STEP:
            return Object.assign({}, state, { xIsNext: (action.step % 2) === 0 });
        case HIGHLIGHTS_CHANGED:
            return Object.assign({}, state, { highlights: action.highlightsInput });
        case CHECKED_FLIP:
            return Object.assign({}, state, { checked: !state.checked })
        default: 
            return state;
    }
}

export default rootReducer;