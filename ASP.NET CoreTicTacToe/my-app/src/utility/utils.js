function ArrayNotNullOrEmpty(history)
{
    return history && Array.isArray(history) && history.length;
}

export default ArrayNotNullOrEmpty;