import { HISTORY_BUTTON_SWITCHED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    reverseIsChecked: false,
    status: {
        xIsNext: true,
        stepNumber: 0
    },
    highlights: Array(9).fill(false), 
}

function rootReducer(state = initialState, action)
{
    switch (action.type)
    {
        case HISTORY_ITEM_CLICKED:
            return ({ 
                ...state, 
                highlights: action.highlightsInput,
                status: { 
                    xIsNext: (action.stepInput % 2) === 0, 
                    stepNumber: action.stepInput
                }
             });


        case GAME_BOARD_CLICKED:
            return { 
                ...state, 
                history: state.reverseIsChecked ? action.history.reverse().concat([{ squares: action.squares, }]).reverse() : action.history.concat([{ squares: action.squares, }]),
                status: { 
                    xIsNext: !state.status.xIsNext,
                    stepNumber: action.history.length, 
                }
            };
        case HISTORY_BUTTON_SWITCHED:
            return ({ 
                ...state, 
                history: state.history.slice().reverse(), 
                reverseIsChecked: !state.reverseIsChecked 
            });    
        default: 
            return state;
    }
}

export default rootReducer;
