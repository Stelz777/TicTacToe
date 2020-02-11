import React from 'react';

export function ArrayNotNullOrEmpty(history)
{
    return history && Array.isArray(history) && history.length;
}

export function SplitLineToParagraphs(text)
{
    let newText = text.split('\n').map((item, i) => 
    {
        return <p key = {i}>{item}</p>;
    });
    return newText;
}

export function CreateUrlParams()
    {
        return new URLSearchParams(window.location.search);
    }

export function GetIdFromUrlParams(urlParams)
    {
        return urlParams.get('id');
    }

export function GetBotFromUrlParams(urlParams)
    {
        return urlParams.get('bot');
    }

export default { ArrayNotNullOrEmpty, SplitLineToParagraphs, CreateUrlParams, GetIdFromUrlParams, GetBotFromUrlParams };

