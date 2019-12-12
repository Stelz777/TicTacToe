//Action Type

export const HISTORY_BUTTON_SWITCHED = 'HISTORY_BUTTON_SWITCHED'
export const INIT = 'INIT';
export const HISTORY_ITEM_CLICKED = 'HISTORY_ITEM_CLICKED';
export const GAME_BOARD_CLICKED = 'GAME_BOARD_CLICKED';

//Action Creator
export function historyButtonSwitched()
{
    //Action
    return {
        type: HISTORY_BUTTON_SWITCHED
    }
}

export function historyItemClicked(stepInput = null, highlightsInput = null)
{
    return {
        type: HISTORY_ITEM_CLICKED,
        stepInput,
        highlightsInput
    }
}

export function init()
{
    return {
        type: INIT
    }
}

export function gameBoardClicked(history = null, squares = null)
{
    return {
        type: GAME_BOARD_CLICKED,
        history,
        squares
    }
}










