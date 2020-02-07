import { HISTORY_BUTTON_SWITCHED, HISTORY_REQUESTED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';
import { BOARD_REQUESTED } from '../actions/actions';
import { SIDE_RECEIVED } from '../actions/actions';
import { BOT_SET , BOT_IS_X} from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';
import ArrayNotNullOrEmpty from '../utility/utils';

const initialState = {
    history: null,
    reverseIsChecked: false,
    status: {
        xIsNext: true,
        stepNumber: 0
    },
    highlights: Array(9).fill(false), 
    board: Array(9).fill(null),
    side: 0,
    playerName: '',
    bot: '',
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



function rootReducer(state = initialState, action)
{
    
    switch (action.type)
    {
        case BOT_IS_X:
            return ({
                ...state,
                status: { 
                    stepNumber: state.status.stepNumber + 1
                }
            });
        case BOT_SET:
            return ({
                ...state,
                bot: action.bot
            });

        case SIDE_RECEIVED:
            return ({
                ...state,
                side: action.side,
                playerName: action.playerName
            });

        case HISTORY_REQUESTED:
            return ({
                ...state,
                history: action.boards
            });

        case BOARD_REQUESTED:
            return ({
                ...state,
                board: action.board
            });

        case HISTORY_ITEM_CLICKED:
            let highlights = Array(9).fill(false);
            highlights[action.squareIndex] = true; 
            console.log("HISTORY_ITEM_CLICKED action.stepInput: ", action.stepInput);
            let historyItemClickedStepNumber = state.reverseIsChecked ? state.history.length - action.stepInput - 1 : action.stepInput;
            return ({ 
                ...state, 
                highlights: highlights,
                status: { 
                    xIsNext: (action.stepInput % 2) !== 0, 
                    stepNumber: historyItemClickedStepNumber
                }
            });


        case GAME_BOARD_CLICKED:
            
            let history = getHistorySlice(state);
            let current = ArrayNotNullOrEmpty(history) ? getLastHistoryItem(state, history) : state.board;
            let squares = ArrayNotNullOrEmpty(history) ? current.squares.slice() : current.slice();
               
            if (CalculateWinner(squares) || squares[action.squareIndex])
            {
                return state;
            }
            squares[action.squareIndex] = action.side === 0 ? 'X' : 'O';
            
            return { 
                ...state, 
                history: state.reverseIsChecked 
                ? history.reverse().concat([{ squares: squares, }]).reverse() 
                : history.concat([{ squares: squares, }]),
                status: { 
                    xIsNext: !state.status.xIsNext,
                    stepNumber: history.length, 
                },
                board: squares,
            };
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
        default: 
            return state;
    }
}

export default rootReducer;
