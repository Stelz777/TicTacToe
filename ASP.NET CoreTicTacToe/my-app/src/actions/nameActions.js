
import * as names from '../constants/nameConstants';

export function nameSet(clientPlayerName)
{
    return {
        type: names.NAME_SET,
        clientPlayerName  
    }
}

export function nameSetInLobby(playerNameInLobby)
{
    return {
        type: names.NAME_SET_IN_LOBBY,
        playerNameInLobby
    }
}


























