function GetCurrentItem(history, reverseIsChecked, stepNumber)
{
    if (history === null)
        {
            return null;
        }
        
        if (reverseIsChecked)
        {
            return history[stepNumber - 1];
        }
        else
        {
            if (history[stepNumber] !== undefined)
            {
                return history[stepNumber];
            }
            
            return history[stepNumber - 1];
        }
}

export default GetCurrentItem;