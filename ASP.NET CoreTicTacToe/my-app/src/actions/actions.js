//Action Type

export const HISTORY_BUTTON_SWITCHED = 'HISTORY_BUTTON_SWITCHED'
export const HISTORY_ITEM_CLICKED = 'HISTORY_ITEM_CLICKED';
export const GAME_BOARD_CLICKED = 'GAME_BOARD_CLICKED';
export const BOT_TURN_REQUESTED = 'BOT_TURN_REQUESTED';
export const BOT_TURN_RESPONDED = 'BOT_TURN_RESPONDED';
export const BOARD_REQUESTED = 'BOARD_REQUESTED';
export const HISTORY_REQUESTED = 'HISTORY_REQUESTED';
export const SIDE_RECEIVED = 'SIDE_RECEIVED';
export const BOT_SET = 'BOT_SET';
export const BOT_IS_X = 'BOT_IS_X';
export const PLAYER_NAMES_RECEIVED = 'PLAYER_NAMES_RECEIVED';
export const SPECTATOR_RESOLVED = 'SPECTATOR_RESOLVED';
export const GAME_RENDERED = 'GAME_RENDERED';
export const ALL_GAMES_RECEIVED = 'ALL_GAMES_RECEIVED'
export const LOBBY_RENDERED = 'LOBBY_RENDERED';

export function lobbyRendered()
{
    return {
        type: LOBBY_RENDERED
    }
}

export function allGamesReceived(ids, ticPlayers, tacPlayers)
{
    return {
        type: ALL_GAMES_RECEIVED,
        ids,
        ticPlayers,
        tacPlayers
    }
}

export function gameRendered()
{
    return {
        type: GAME_RENDERED
    }
}

export function spectatorResolved()
{
    return {
        type: SPECTATOR_RESOLVED
    }
}

export function playerNamesReceived(ticName, tacName)
{
    console.log("playerNamesReceived ticName: ", ticName);
    return {
        type: PLAYER_NAMES_RECEIVED,
        ticName,
        tacName
    }
}

export function botIsX()
{
    return {
        type: BOT_IS_X
    }
}

export function botSet(bot)
{
    return {
        type: BOT_SET,
        bot
    }
}

export function sideReceived(side, playerName)
{
    return {
        type: SIDE_RECEIVED,
        side,
        playerName
    }
}

export function historyRequested(boards)
{
    return {
        type: HISTORY_REQUESTED,
        boards
    }
}

export function boardRequested(board)
{
    return {
        type: BOARD_REQUESTED,
        board
    }
}

//Action Creator
export function historyButtonSwitched()
{
    //Action
    return {
        type: HISTORY_BUTTON_SWITCHED
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

export function gameBoardClicked(squareIndex, side)
{
    
    return {
        type: GAME_BOARD_CLICKED,
        squareIndex,
        side
    }
}










