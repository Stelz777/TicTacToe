import * as commons from '../constants/commonConstants';

//Action Creator
export function allGamesReceived(games)
{
    //Action
    return {
        type: commons.ALL_GAMES_RECEIVED,
        games
    }
}

export function gameInit()
{
    return {
        type: commons.GAME_INIT   
    }
}

export function lobbyInit()
{
    return {
        type: commons.LOBBY_INIT
    }
}

export function playerNamesReceived(ticName, tacName)
{
    return {
        type: commons.PLAYER_NAMES_RECEIVED,
        ticName,
        tacName
    }
}

export function sideReceived(side)
{
    return {
        type: commons.SIDE_RECEIVED,
        side
    }
}

export function spectatorResolved()
{
    return {
        type: commons.SPECTATOR_RESOLVED   
    }
}