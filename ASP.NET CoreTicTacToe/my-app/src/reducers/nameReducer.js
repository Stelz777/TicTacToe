import { NAME_SET, NAME_SET_IN_LOBBY } from '../constants/nameConstants';

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
        default:
            return state;
    }
}

export default nameReducer;