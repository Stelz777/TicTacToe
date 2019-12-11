import { HISTORY_CHANGED } from '../actions';
import { HISTORY_CONCAT } from '../actions';
import { STEP_CHANGED } from '../actions';
import { STEP } from '../actions';
import { X_IS_NEXT } from '../actions';
import { X_IS_NEXT_BY_STEP } from '../actions';
import { COLUMN_CHANGED } from '../actions';
import { ROW_CHANGED } from '../actions';
import { HIGHLIGHTS_CHANGED } from '../actions';
import { CHECKED } from '../actions'; 
import { INIT } from '../actions';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    stepNumber: 0,
    xIsNext: true,
    currentColumn: 1,
    currentRow: 1,
    highlights: Array(9).fill(false),
    checked: false,
}

export default function gameReducer(state = initialState, action)
{
    switch (action.type)
    {
        case INIT:
            return Object.assign({}, state, { history: initialState.history, stepNumber: initialState.stepNumber });
        case HISTORY_CHANGED:
            return Object.assign({}, state, { history: state.history.slice().reverse() });
        case HISTORY_CONCAT:
            return Object.assign({}, state, { history: action.history.concat([{ squares: action.squares, }]) });
        case STEP_CHANGED:
            return Object.assign({}, state, { stepNumber: action.history.length });
        case STEP:
            return Object.assign({}, state, { stepNumber: action.stepInput });
        case X_IS_NEXT:
            return Object.assign({}, state, { xIsNext: !state.xIsNext });
        case X_IS_NEXT_BY_STEP:
            return Object.assign({}, state, { xIsNext: (action.step % 2) === 0 });
        case COLUMN_CHANGED:
            return state;
        case ROW_CHANGED:
            return state;
        case HIGHLIGHTS_CHANGED:
            return Object.assign({}, state, { highlights: action.highlightsInput });
        case CHECKED:
            return Object.assign({}, state, { checked: !state.checked })
        default: 
            return state;
    }
}