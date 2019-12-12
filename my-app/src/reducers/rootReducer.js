import { HISTORY_BUTTON_SWITCHED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    stepNumber: 0,
    xIsNext: true,
    highlights: Array(9).fill(false),
    reverseIsChecked: false,
}

function rootReducer(state = initialState, action)
{
    switch (action.type)
    {
        case HISTORY_ITEM_CLICKED:
            return Object.assign({}, state, { highlights: action.highlightsInput, xIsNext: (action.stepInput % 2) === 0, stepNumber: action.stepInput });
        case HISTORY_BUTTON_SWITCHED:
            return Object.assign({}, state, { history: state.history.slice().reverse(), reverseIsChecked: !state.reverseIsChecked });
        case GAME_BOARD_CLICKED:
            return Object.assign({}, state, { history: action.history.concat([{ squares: action.squares, }]), stepNumber: action.history.length, xIsNext: !state.xIsNext });
        default: 
            return state;
    }
}

export default rootReducer;