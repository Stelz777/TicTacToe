export function configureFakeBackend()
{
    let users = [{ id: 1, username: 'test', password: 'test', firstName: 'Test', lastName: 'User' }];
    let realFetch = window.fetch;
    window.fetch = function(url, options)
    {
        return new this.Promise((resolve, reject) => {
            this.setTimeout(() => {
                if (url.endsWith('/users/authenticate') && options.method === 'POST')
                {
                    let params = JSON.parse(options.body);
                    let filteredUsers = users.filter(user => {
                        return user.username === params.username && user.password === params.password;
                    });
                    if (filteredUsers.length)
                    {
                        let user = filteredUsers[0];
                        let responseJson = {
                            id: user.id,
                            username: user.username,
                            firstName: user.firstName,
                            lastName: user.lastName,
                            token: 'fake-jwt-token'
                        };
                        resolve({ ok: true, text: () => this.Promise.resolve(JSON.stringify(responseJson)) });
                    }
                    else
                    {
                        reject('Username or password is incorrect');
                    }
                    return;
                }
                if (url.endsWith('/users') && options.method === 'GET')
                {
                    if (options.headers && options.headers.Authorization === 'Bearer fake-jwt-token')
                    {
                        resolve({ ok: true, text: () => this.Promise.resolve(JSON.stringify(users)) });
                    }
                    else
                    {
                        reject('Unathorised');
                    }
                    return;
                }
                realFetch(url, options).then(response => resolve(response));
            }, 500);
        });
    }
}