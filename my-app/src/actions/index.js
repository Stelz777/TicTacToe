//Action Type

export const HISTORY_CHANGED = 'HISTORY_CHANGED';
export const HISTORY_CONCAT = 'HISTORY_CONCAT';
export const STEP_CHANGED = 'STEP_CHANGED';
export const STEP = 'STEP';
export const X_IS_NEXT_FLIP = 'X_IS_NEXT_FLIP';
export const X_IS_NEXT_BY_STEP = 'X_IS_NEXT_BY_STEP';
export const HIGHLIGHTS_CHANGED = 'HIGHLIGHTS_CHANGED';
export const CHECKED_FLIP = 'CHECKED_FLIP';
export const INIT = 'INIT';

//Action Creator
export function historyChanged()
{
    //Action
    return {
        type: HISTORY_CHANGED
    }
}

export function init()
{
    return {
        type: INIT
    }
}

export function historyConcat(history = null, squares = null)
{
    return {
        type: HISTORY_CONCAT,
        history,
        squares
    }
}

export function stepChanged(history = null)
{
    return {
        type: STEP_CHANGED,
        history
    }
}

export function step(stepInput = null)
{
    return {
        type: 'STEP',
        stepInput
    }
}

export function xIsNextFlip()
{
    return {
        type: X_IS_NEXT_FLIP
    }
}

export function xIsNextByStep(step = null)
{
    return {
        type: X_IS_NEXT_BY_STEP,
        step
    }
}

export function highlightsChanged(highlightsInput = null)
{
    return {
        type: HIGHLIGHTS_CHANGED,
        highlightsInput
    }
}

export function checkedFlip()
{
    return {
        type: CHECKED_FLIP
    }
}




