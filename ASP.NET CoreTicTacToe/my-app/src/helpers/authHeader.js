export function authHeader()
{
    let user = JSON.parse(localStorage.getItem('user'));
    if (user && user.token)
    {
        return { 'Authorization': 'Bearer ' + user.token };
    }
    else
    {
        return {};
    }
}

export function authHeaderJSON()
{
    let user = JSON.parse(localStorage.getItem('user'));
    if (user && user.token)
    {
        return {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + user.token,
            'Content-Type': 'application/json' 
        };
    }
    else
    {
        return {
            'Accept': 'application/json',
            'Content-Type': 'application/json' 
        };
    }
}