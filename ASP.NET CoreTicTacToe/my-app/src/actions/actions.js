//Action Type

export const HISTORY_BUTTON_SWITCHED = 'HISTORY_BUTTON_SWITCHED'
export const HISTORY_ITEM_CLICKED = 'HISTORY_ITEM_CLICKED';
export const GAME_BOARD_CLICKED = 'GAME_BOARD_CLICKED';
export const BOT_TURN_REQUESTED = 'BOT_TURN_REQUESTED';
export const BOT_TURN_RESPONDED = 'BOT_TURN_RESPONDED';
export const BOARD_REQUESTED = 'BOARD_REQUESTED';
export const HISTORY_REQUESTED = 'HISTORY_REQUESTED';
export const SIDE_RECEIVED = 'SIDE_RECEIVED';

export function sideReceived(side)
{
    return {
        type: SIDE_RECEIVED,
        side
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

export function gameBoardClicked(squareIndex)
{
    
    return {
        type: GAME_BOARD_CLICKED,
        squareIndex
    }
}










