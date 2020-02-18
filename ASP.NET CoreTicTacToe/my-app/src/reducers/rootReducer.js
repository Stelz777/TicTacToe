import { HISTORY_BUTTON_SWITCHED, HISTORY_REQUESTED, PLAYER_NAMES_RECEIVED, SPECTATOR_RESOLVED, GAME_INIT, ALL_GAMES_RECEIVED, LOBBY_INIT, SIDE_RECEIVED } from '../actions/actions';
import { GAME_BOARD_CLICKED, NAME_SET_IN_LOBBY, BOT_X_BUTTON_SWITCHED, BOT_O_BUTTON_SWITCHED } from '../actions/actions';
import { HISTORY_ITEM_CLICKED } from '../actions/actions';
import { BOARD_REQUESTED } from '../actions/actions';
import { NAME_SET } from '../actions/actions';
import { BOT_SET , BOT_IS_X} from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';
import utils from '../utility/utils';
import { bindActionCreators } from 'redux';

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
    clientPlayerName: '',
    bot: '',
    ticPlayerName: '',
    tacPlayerName: '',
    isSpectator: false,
    isInGame: false,
    games: null,
    isInLobby: true,
    testValue: false,
    lobbyPlayerName: '',
    botXIsChecked: false,
    botOIsChecked: false,
    isDisabledNameInput: false
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
        case BOT_X_BUTTON_SWITCHED:
            return ({
                ...state,
                botXIsChecked: !state.botXIsChecked
            });
        case BOT_O_BUTTON_SWITCHED:
            return ({
                ...state,
                botOIsChecked: !state.botOIsChecked
            });
        case NAME_SET_IN_LOBBY:
            return ({
                ...state,
                lobbyPlayerName: action.playerNameInLobby
            });
        case NAME_SET:
            console.log("reducer: ", action.clientPlayerName);
            return ({
                ...state,
                clientPlayerName: action.clientPlayerName
            });
        case SIDE_RECEIVED:
            return ({
                ...state,
                side: action.side
            });
        case LOBBY_INIT:
            return ({
                ...state,
                isInGame: false,
                isInLobby: true,
                isDisabledNameInput: false
            });
        case ALL_GAMES_RECEIVED:
            return ({
                ...state,
                games: action.games
            });
        case GAME_INIT:
            return ({
                ...initialState,
                isInGame: true,
                isInLobby: false
            });
        case SPECTATOR_RESOLVED:
            return ({
                ...state,
                isSpectator: true
            });
        case PLAYER_NAMES_RECEIVED:
            return ({
                ...state,
                ticPlayerName: action.ticName,
                tacPlayerName: action.tacName
            });

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
            let current = utils.ArrayNotNullOrEmpty(history) ? getLastHistoryItem(state, history) : state.board;
            let squares = utils.ArrayNotNullOrEmpty(history) ? current.squares.slice() : current.slice();
               
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
