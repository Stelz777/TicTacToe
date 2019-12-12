import { HISTORY_BUTTON_SWITCHED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    reverseIsChecked: false
}

function history(state = initialState, action)
{
    switch (action.type)
    {
        
        case HISTORY_BUTTON_SWITCHED:
            return ({ 
                ...state, 
                history: state.history.slice().reverse(), 
                reverseIsChecked: !state.reverseIsChecked 
            });   
        case GAME_BOARD_CLICKED:
            return { 
                ...state, 
                history: action.history.concat([{ squares: action.squares, }]), 
            };
        default: 
            return state;
    }
}

export default history;