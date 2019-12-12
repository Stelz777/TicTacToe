//Action Type

export const HISTORY_BUTTON_SWITCHED = 'HISTORY_BUTTON_SWITCHED'
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

export function historyItemClicked(stepInput, highlightsInput)
{
    return {
        type: HISTORY_ITEM_CLICKED,
        stepInput,
        highlightsInput
    }
}

export function gameBoardClicked(history, squares)
{
    return {
        type: GAME_BOARD_CLICKED,
        history,
        squares
    }
}










