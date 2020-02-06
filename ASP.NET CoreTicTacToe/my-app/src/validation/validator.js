function ValidateArray(history)
{
    if (history === null)
    {
        return false;
    }
    else
    {
        if (history.length === 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

export default ValidateArray;