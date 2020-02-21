function GetCurrentItem(history, stepNumber)
{
    if (history === null)
    {
        return null;
    }
    
    return history[stepNumber];
}

export default GetCurrentItem;