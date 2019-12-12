import { GAME_BOARD_CLICKED, HISTORY_ITEM_CLICKED } from '../actions/actions';

const initialState = {
    status: {
        xIsNext: true,
        stepNumber: 0
    },
    highlights: Array(9).fill(false), 
}

function game(state = initialState, action)
{
    switch (action.type)
    {
        case HISTORY_ITEM_CLICKED:
            return ({ 
                ...state, 
                highlights: action.highlightsInput,
                status: { 
                    xIsNext: (action.stepInput % 2) === 0, 
                    stepNumber: action.stepInput
                }
             });


        case GAME_BOARD_CLICKED:
            return { 
                ...state, 
                status: { 
                    xIsNext: !state.status.xIsNext,
                    stepNumber: action.history.length
                }
            };
                
        default: 
            return state;
    }
}

export default game;