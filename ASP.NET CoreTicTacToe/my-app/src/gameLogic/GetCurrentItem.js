function GetCurrentItem(history, stepNumber)
{
    if (!history)
    {
        return null;
    }
    
    return history[stepNumber];
}

export default GetCurrentItem;