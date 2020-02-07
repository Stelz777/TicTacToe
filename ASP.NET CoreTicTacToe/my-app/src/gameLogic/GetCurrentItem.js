function GetCurrentItem(history, reverseIsChecked, stepNumber)
{
    if (history === null)
    {
        return null;
    }
    
    if (reverseIsChecked)
    {
        return history[stepNumber];
    }
    else
    {
        return history[Math.min(stepNumber, history.length - 1)]   
    }
}

export default GetCurrentItem;