function ActionLink()
{
    function handleClick(e)
    {
        e.preventDefault();
        console.log('По ссылке кликнули.');
    }
    return (
        <a href="#" onClick={handleClick}>
            Нажми на меня
        </a>
    );
}

function FormattedDate(props)
{
    return <h2>Сейчас { props.date.toLocaleTimeString() }.</h2>
}

function Clock(props)
{
    return (
        <div>
            <h1>Привет, мир!</h1>
            <h2>Сейчас { props.date.toLocaleTimeString() }.</h2>
        </div>
    );
}

function withdraw(account, amount)
{
    account.total -= amount;
}

function sum(a, b)
{
    return a + b
}

function UserInfo(props)
{
    return (
        <div className="UserInfo">
            <Avatar user={props.user} />
            <div className="UserInfo-name">
                { props.user.name }
            </div>
        </div>
    );
}

function Avatar(props)
{
    return (
        <img className="Avatar"
            src= { props.user.avatarUrl }
            alt= { props.user.name }
        />
    );
}

function Comment(props)
{
    return (
        <div className="Comment">
            <UserInfo user={props.author} />
            <div className="Comment-text">
                { props.text }
            </div>
            <div className="Comment-date">
                { formatDate(props.date) }
            </div>
        </div>
    );
}

function App()
{
    return (
        <div>
            <Clock />
            <Clock />
            <Clock />
        </div>
    );
}

function Welcome(props)
{
    return <h1>Привет, { props.name } </h1>;
}

function tick()
{
    ReactDOM.render(
        <Clock />,
        document.getElementById('root')
    );
}

setInterval(tick, 1000);

function formatName(user)
{
    return user.firstName + ' ' + user.lastName;
}

function getGreeting(user)
{
    if (user)
    {
        return <h1>Здравствуй, { formatName(user) }!</h1>;
    }
    return <h1>Здравствуй, незнакомец.</h1>
}

const title = response.potentiallyMaliciousInput;

const user = 
{
    firstName: 'Марья',
    lastName: 'Моревна'
};

const name = 'Иван-Царевич';
const element = <Welcome name="Алиса" />


ReactDOM.render (
    <App />,
    document.getElementById('root')
);