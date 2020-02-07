import React from 'react';

export function ArrayNotNullOrEmpty(history)
{
    return history && Array.isArray(history) && history.length;
}

export function FixLineSeparationForReact(text)
{
    let newText = text.split('\n').map((item, i) => 
    {
        return <p key = {i}>{item}</p>;
    });
    return newText;
}

export default { ArrayNotNullOrEmpty, FixLineSeparationForReact };

