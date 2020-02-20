import * as bots from '../constants/botConstants';
import * as commons from '../constants/commonConstants';
import * as histories from '../constants/historyConstants';
import * as names from '../constants/nameConstants';

//Action Creator
export function allGamesReceived(games)
{
    //Action
    return {
        type: commons.ALL_GAMES_RECEIVED,
        games
    }
}

export function botOButtonSwitched()
{
    return {
        type: bots.BOT_O_BUTTON_SWITCHED
    }
}

export function botSet(bot)
{
    return {
        type: bots.BOT_SET,
        bot
    }
}

export function botXButtonSwitched()
{
    return {
        type: bots.BOT_X_BUTTON_SWITCHED
    }
}

export function gameInit()
{
    return {
        type: commons.GAME_INIT   
    }
}

export function historyButtonSwitched()
{
    return {
        type: histories.HISTORY_BUTTON_SWITCHED  
    }
}

export function historyInit()
{
    return {
        type: histories.HISTORY_INIT
    }
}

export function historyItemAdded(squareIndex, side)
{
    return {
        type: histories.HISTORY_ITEM_ADDED,
        squareIndex,
        side
    }
}

export function historyItemClicked(stepInput, squareIndex)
{
    return {
        type: histories.HISTORY_ITEM_CLICKED,
        stepInput,
        squareIndex
    }
}

export function historyRequested(boards)
{
    return {
        type: histories.HISTORY_REQUESTED,
        boards
    }
}

export function lobbyInit()
{
    return {
        type: commons.LOBBY_INIT
    }
}

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
























