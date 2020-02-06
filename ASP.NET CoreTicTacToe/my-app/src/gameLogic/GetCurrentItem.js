function GetCurrentItem(history, reverseIsChecked, stepNumber)
{
    if (history === null)
        {
            return null;
        }
        console.log("getCurrentHistoryItem this.props.stepNumber: ", stepNumber);
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