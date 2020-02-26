import { authHeader } from '../helpers/authHeader';

export function login(username, password)
{
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Name: username, Password: password })
    };

    return fetch(`/api/user/authenticate/`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        });

}

function handleResponse(response)
{
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok)
        {
            if (response.status === 401)
            {
                logout();
                window.location.reload(true);
            }
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        return data;
    });
}

export function logout()
{
    localStorage.removeItem('user');
}

export function getAll()
{
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`/api/user/all/`, requestOptions).then(handleResponse);
}

export function register(username, password)
{
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Name: username, Password: password })
    };

    return fetch(`/api/user/register/`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        });

}