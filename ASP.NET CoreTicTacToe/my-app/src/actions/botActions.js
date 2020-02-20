import * as bots from '../constants/botConstants';

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