import { BOT_O_BUTTON_SWITCHED, BOT_SET, BOT_X_BUTTON_SWITCHED } from '../constants/botConstants';

const initialState = {
    bot: '',
    botOIsChecked: false,
    botXIsChecked: false,    
}

function botReducer(state = initialState, action)
{
    switch (action.type)
    {
        case BOT_O_BUTTON_SWITCHED:
            return ({
                ...state,
                botOIsChecked: !state.botOIsChecked
            });
        case BOT_SET:
            return ({
                ...state,
                bot: action.bot
            });
        case BOT_X_BUTTON_SWITCHED:
            return ({
                ...state,
                botXIsChecked: !state.botXIsChecked
            });
        default:
            return state;
    }
}

export default botReducer;