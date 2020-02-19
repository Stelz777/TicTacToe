import { ALL_GAMES_RECEIVED } from '../actions/actions';
import { BOARD_REQUESTED } from '../actions/actions';
import { BOT_IS_X, BOT_O_BUTTON_SWITCHED, BOT_SET, BOT_X_BUTTON_SWITCHED } from '../actions/actions';
import { GAME_BOARD_CLICKED, GAME_INIT } from '../actions/actions';
import { HISTORY_BUTTON_SWITCHED,  HISTORY_ITEM_CLICKED, HISTORY_REQUESTED } from '../actions/actions';
import { LOBBY_INIT } from '../actions/actions';
import { NAME_SET, NAME_SET_IN_LOBBY } from '../actions/actions';
import { PLAYER_NAMES_RECEIVED } from '../actions/actions';
import { SIDE_RECEIVED } from '../actions/actions';
import { SPECTATOR_RESOLVED } from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';
import utils from '../utility/utils';

const initialState = {
    board: Array(9).fill(null),
    bot: '',
    botOIsChecked: false,
    botXIsChecked: false,
    clientPlayerName: '',
    games: null,
    highlights: Array(9).fill(false),
    history: null,
    isDisabledNameInput: false,
    isInGame: false,
    isInLobby: true,
    isSpectator: false,
    lobbyPlayerName: '',
    reverseIsChecked: false,
    side: 0,
    status: {
        xIsNext: true,
        stepNumber: 0
    },
    tacPlayerName: '',
    ticPlayerName: ''
}


function rootReducer(state = initialState, action)
{
    
    switch (action.type)
    {
        case ALL_GAMES_RECEIVED:
            return ({
                ...state,
                games: action.games
            });
        case BOARD_REQUESTED:
            return ({
                ...state,
                board: action.board
            });
        case BOT_IS_X:
            return ({
                ...state,
                status: { 
                    stepNumber: state.status.stepNumber + 1
                }
            });
        case BOT_O_BUTTON_SWITCHED:
            return ({
                ...state,
                botOIsChecked: !state.botOIsChecked
            });
        case BOT_SET:
            return ({
                ...state,
                bot: action.bot
            });
        case BOT_X_BUTTON_SWITCHED:
            return ({
                ...state,
                botXIsChecked: !state.botXIsChecked
            });
        case GAME_BOARD_CLICKED:
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
        case GAME_INIT:
            return ({
                ...initialState,
                isInGame: true,
                isInLobby: false
            });
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
        case LOBBY_INIT:
            return ({
                ...state,
                isDisabledNameInput: false,
                isInGame: false,
                isInLobby: true,
            });
        case NAME_SET:
            return ({
                ...state,
                clientPlayerName: action.clientPlayerName
            });    
        case NAME_SET_IN_LOBBY:
            return ({
                ...state,
                lobbyPlayerName: action.playerNameInLobby
            });
        case PLAYER_NAMES_RECEIVED:
            return ({
                ...state,
                tacPlayerName: action.tacName,
                ticPlayerName: action.ticName
            });
        case SIDE_RECEIVED:
            return ({
                ...state,
                side: action.side
            });
        case SPECTATOR_RESOLVED:
            return ({
                ...state,
                isSpectator: true
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

export default rootReducer;
