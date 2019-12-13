import { HISTORY_BUTTON_SWITCHED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';

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
            let history;
            if (state.reverseIsChecked)
            {
                history = state.history.slice(state.history.length - state.status.stepNumber - 1, state.history.length);
            }
            else
            {
                history = state.history.slice(0, state.status.stepNumber + 1);
            }
            let current;
            if (state.reverseIsChecked)
            {
                current = history[0];
            }
            else
            {
                current = history[history.length - 1];
            }
            const squares = current.squares.slice();
               
            if (CalculateWinner(squares) || squares[action.i])
            {
                return;
            }
            squares[action.i] = state.status.xIsNext ? 'X' : 'O';
            return { 
                ...state, 
                history: state.reverseIsChecked 
                ? history.reverse().concat([{ squares: squares, }]).reverse() 
                : history.concat([{ squares: squares, }]),
                status: { 
                    xIsNext: !state.status.xIsNext,
                    stepNumber: history.length, 
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
