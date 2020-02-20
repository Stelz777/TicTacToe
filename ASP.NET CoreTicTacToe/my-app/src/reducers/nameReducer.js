import { NAME_SET, NAME_SET_IN_LOBBY } from '../actions/actions';

const initialState = {
    clientPlayerName: '',
    lobbyPlayerName: ''
}

function nameReducer(state = initialState, action)
{
    switch (action.type)
    {
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
        default:
            return state;
    }
}

export default nameReducer;