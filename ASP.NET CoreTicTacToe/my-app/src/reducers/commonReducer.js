import { ALL_GAMES_RECEIVED, GAME_INIT, LOBBY_INIT, PLAYER_NAMES_RECEIVED, SIDE_RECEIVED, SPECTATOR_RESOLVED } from '../actions/actions';

const initialState = {
    games: null,
    isDisabledNameInput: false,
    isInGame: false,
    isInLobby: true,
    isSpectator: false,
    side: 0,
    tacPlayerName: '',
    ticPlayerName: ''
}

function commonReducer(state = initialState, action)
{
    switch (action.type)
    {
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
        case LOBBY_INIT:
            return ({
                ...state,
                isDisabledNameInput: false,
                isInGame: false,
                isInLobby: true,
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

export default commonReducer;