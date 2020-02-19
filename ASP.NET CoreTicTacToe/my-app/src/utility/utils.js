import React from 'react';

export function ArrayNotNullOrEmpty(history)
{
    return history && Array.isArray(history) && history.length;
}

export function BuildUrlParams(data)
{
    return Object.keys(data).map(key => `${key}=${encodeURIComponent(data[key])}`).join('&')
}

export function GetAllUrlParams(url)
{
    var queryString = url ? url.split('?')[1] : window.location.search.slice(1);
    var params = {};
    if (queryString)
    {
        queryString = queryString.split('#')[0];
        var componentParts = queryString.split('&');
        for (var i = 0; i < componentParts.length; i++)
        {
            var keysAndValues = componentParts[i].split('=');
            var paramName = keysAndValues[0];
            var paramValue = typeof(keysAndValues[1]) === 'undefined' ? true : keysAndValues[1];
            paramName = paramName.toLowerCase();
            if (typeof paramValue === 'string')
            {
                paramValue = paramValue.toLowerCase();
            }
            if (paramName.match(/\[(\d+)?\]$/))
            {
                var key = paramName.replace(/\[(\d+)?\]/, '');
                if (!params[key])
                {
                    params[key] = [];
                }
                if (paramName.match(/\[\d+\]$/))
                {
                    var index = /\[(\d+)\]/.exec(paramName)[1];
                    params[key][index] = paramValue;
                }
                else
                {
                    params[key].push(paramValue);
                }
            }
            else
            {
                if (!params[paramName])
                {
                    params[paramName] = paramValue;
                }
                else if (params[paramName] && typeof params[paramName] === 'string')
                {
                    params[paramName] = [params[paramName]];
                    params[paramName].push(paramValue);
                }
                else
                {
                    params[paramName].push(paramValue);
                }
            }
        }
    }
    return params;
}

export function SplitLineToParagraphs(text)
{
    let newText = text.split('\n').map((item, i) => 
    {
        return <p key = {i}>{item}</p>;
    });
    return newText;
}

export default { ArrayNotNullOrEmpty, BuildUrlParams, GetAllUrlParams, SplitLineToParagraphs };

