import { HISTORY_BUTTON_SWITCHED, HISTORY_INIT, HISTORY_ITEM_ADDED, HISTORY_ITEM_CLICKED, HISTORY_REQUESTED } from '../constants/historyConstants';
import CalculateWinner from '../gameLogic/CalculateWinner';
import utils from '../utility/utils';

const initialState = {
    board: Array(9).fill(null),
    highlights: Array(9).fill(false),
    history: null,
    reverseIsChecked: false,
    status: {
        stepNumber: 0,
        xIsNext: true
    },
}

function historyReducer(state = initialState, action)
{
    switch (action.type)
    {
        case HISTORY_BUTTON_SWITCHED:
            let currentStepNumber = state.history.length - state.status.stepNumber - 1;
                
            let currentHistory = state.history.slice().reverse();
    
            return ({ 
                ...state, 
                history: currentHistory, 
                reverseIsChecked: !state.reverseIsChecked,
                status: {
                    stepNumber: currentStepNumber 
                }
            }); 
        case HISTORY_INIT:
            return {
                ...initialState
            };
        case HISTORY_ITEM_ADDED:
            let history = getHistorySlice(state);
            let current = utils.ArrayNotNullOrEmpty(history) ? getLastHistoryItem(state, history) : state.board;
            let squares = utils.ArrayNotNullOrEmpty(history) ? current.squares.slice() : current.slice();
                       
            if (CalculateWinner(squares) || squares[action.squareIndex])
            {
                return state;
            }
            squares[action.squareIndex] = action.side === 0 ? 'X' : 'O';
                    
            return { 
                ...state, 
                board: squares,
                history: state.reverseIsChecked 
                ? history.reverse().concat([{ squares: squares, }]).reverse() 
                : history.concat([{ squares: squares, }]),
                status: { 
                    xIsNext: !state.status.xIsNext,
                    stepNumber: history.length, 
                }
            };
        case HISTORY_ITEM_CLICKED:
            let highlights = Array(9).fill(false);
            highlights[action.squareIndex] = true; 
            
            let historyItemClickedStepNumber = state.reverseIsChecked ? state.history.length - action.stepInput - 1 : action.stepInput;
            return ({ 
                ...state, 
                highlights: highlights,
                status: {
                    stepNumber: historyItemClickedStepNumber, 
                    xIsNext: (action.stepInput % 2) !== 0
                }
            });
        case HISTORY_REQUESTED:
            return ({
                ...state,
                history: action.boards
            });
        default:
            return state;
    }
}

function getHistorySlice(state)
{       
    return state.history.slice(0, state.status.stepNumber + 1);
}

function getLastHistoryItem(state, history)
{
    if (state.reverseIsChecked)
    {
        return history[0];
    }
    else
    {
        return history[history.length - 1];
    }
}

export default historyReducer;

