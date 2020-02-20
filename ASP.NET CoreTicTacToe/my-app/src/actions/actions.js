//Action Type
export const ALL_GAMES_RECEIVED = 'ALL_GAMES_RECEIVED'
export const BOT_O_BUTTON_SWITCHED = 'BOT_O_BUTTON_SWITCHED';
export const BOT_SET = 'BOT_SET';
export const BOT_X_BUTTON_SWITCHED = 'BOT_X_BUTTON_SWITCHED';
export const GAME_INIT = 'GAME_INIT';
export const HISTORY_BUTTON_SWITCHED = 'HISTORY_BUTTON_SWITCHED';
export const HISTORY_INIT = 'HISTORY_INIT';
export const HISTORY_ITEM_ADDED = 'HISTORY_ITEM_ADDED';
export const HISTORY_ITEM_CLICKED = 'HISTORY_ITEM_CLICKED';
export const HISTORY_REQUESTED = 'HISTORY_REQUESTED';
export const LOBBY_INIT = 'LOBBY_INIT';
export const NAME_SET = 'NAME_SET';
export const NAME_SET_IN_LOBBY = 'NAME_SET_IN_LOBBY';
export const PLAYER_NAMES_RECEIVED = 'PLAYER_NAMES_RECEIVED';
export const SIDE_RECEIVED = 'SIDE_RECEIVED';
export const SPECTATOR_RESOLVED = 'SPECTATOR_RESOLVED';


//Action Creator
export function allGamesReceived(games)
{
    //Action
    return {
        type: ALL_GAMES_RECEIVED,
        games
    }
}

export function botOButtonSwitched()
{
    return {
        type: BOT_O_BUTTON_SWITCHED
    }
}

export function botSet(bot)
{
    return {
        type: BOT_SET,
        bot
    }
}

export function botXButtonSwitched()
{
    return {
        type: BOT_X_BUTTON_SWITCHED
    }
}

export function gameInit()
{
    return {
        type: GAME_INIT   
    }
}

export function historyButtonSwitched()
{
    return {
        type: HISTORY_BUTTON_SWITCHED  
    }
}

export function historyInit()
{
    return {
        type: HISTORY_INIT
    }
}

export function historyItemAdded(squareIndex, side)
{
    return {
        type: HISTORY_ITEM_ADDED,
        squareIndex,
        side
    }
}

export function historyItemClicked(stepInput, squareIndex)
{
    return {
        type: HISTORY_ITEM_CLICKED,
        stepInput,
        squareIndex
    }
}

export function historyRequested(boards)
{
    return {
        type: HISTORY_REQUESTED,
        boards
    }
}

export function lobbyInit()
{
    return {
        type: LOBBY_INIT
    }
}

export function nameSet(clientPlayerName)
{
    return {
        type: NAME_SET,
        clientPlayerName  
    }
}

export function nameSetInLobby(playerNameInLobby)
{
    return {
        type: NAME_SET_IN_LOBBY,
        playerNameInLobby
    }
}

export function playerNamesReceived(ticName, tacName)
{
    return {
        type: PLAYER_NAMES_RECEIVED,
        ticName,
        tacName
    }
}

export function sideReceived(side)
{
    return {
        type: SIDE_RECEIVED,
        side
    }
}

export function spectatorResolved()
{
    return {
        type: SPECTATOR_RESOLVED   
    }
}
























