import * as histories from '../constants/historyConstants';

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