import { HISTORY_BUTTON_SWITCHED, HISTORY_REQUESTED } from '../actions/actions';
import { GAME_BOARD_CLICKED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';
import { BOARD_REQUESTED } from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';

const initialState = {
    history: [{ squares: Array(9).fill(null), }],
    reverseIsChecked: false,
    status: {
        xIsNext: true,
        stepNumber: 0
    },
    highlights: Array(9).fill(false), 
    board: Array(9).fill(null)
}

function rootReducer(state = initialState, action)
{
    switch (action.type)
    {
        case HISTORY_REQUESTED:
            console.log("history in reducer: ", action.history.turns);
            /*var newHistory;
            for (var i = 0; i < action.history.turns.length; i++)
            {
                newHistory[i] = action.history.turns[i].squares;
            }*/
            return ({
                ...state,
                history: action.history.turns
            })

        case BOARD_REQUESTED:
            console.log("board in reducer: ", action.board);
            return ({
                ...state,
                board: action.board
            });

        case HISTORY_ITEM_CLICKED:
            let highlights = Array(9).fill(false);
            highlights[action.squareIndex] = true; 
            return ({ 
                ...state, 
                highlights: highlights,
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
               
            if (CalculateWinner(squares) || squares[action.squareIndex])
            {
                return state;
            }
            squares[action.squareIndex] = state.status.xIsNext ? 'X' : 'O';
            
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
