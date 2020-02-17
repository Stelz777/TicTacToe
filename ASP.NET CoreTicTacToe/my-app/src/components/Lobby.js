import React from 'react';
import { connect } from 'react-redux';
import { allGamesReceived, gameInit, botXButtonSwitched, botOButtonSwitched } from '../actions/actions';
import Name from '../components/Name'
import Switch from "react-switch";

const mapStateToProps = (state) =>
{
    return {
        games: state.games,
        lobbyPlayerName: state.lobbyPlayerName,
        botXIsChecked: state.botXIsChecked,
        botOIsChecked: state.botOIsChecked
    };
}

const mapDispatchToProps =
{
    allGamesReceived,
    gameInit,
    botXButtonSwitched,
    botOButtonSwitched
}

class Lobby extends React.Component
{
    componentDidMount()
    {
        this.getGames();
    }

    printGamesList()
    {
        const games = this.props.games;
        console.log("printgamelist games: ", games);
        if (!games)
        {
            return [];
        }
        
        return games.map((game, iterator) => {
            const description = this.generateDescription(game.id, game.ticPlayer, game.tacPlayer);
            return (
                <li key={game.id} >
                    <button onClick={() => this.showGame(game.id)}>{description}</button>
                </li>
            );
        })
    }

    generateDescription(id, ticPlayer, tacPlayer)
    {
        console.log("generateDescription ticPlayer: ", ticPlayer);
        return `"id: ${id} X: ${this.generatePlayerDescription(ticPlayer)}
                           O: ${this.generatePlayerDescription(tacPlayer)}`;
    }

    generatePlayerDescription(player)
    {
        return `${this.getPlayerName(player)} ${this.getPlayerBot(player)}`;
    }

    getPlayerName(player)
    {
        return player.name || "свободно";
    }

    getPlayerBot(player)
    {
        return player.isBot ? "(бот)" : "";
    }

    showGame(gameIndex)
    {
        this.props.gameInit();
        console.log("showGame this.props.lobbyPlayerName: ", this.props.lobbyPlayerName);
        const urlData = {'name': this.props.lobbyPlayerName, 'id': gameIndex, 'bot': this.shouldWeIncludeBot() ? this.determineBotSymbol() : null};
        this.replaceState(urlData);
    }

    

    getGames()
    {
        console.log("inside getGames()");
        fetch(`/api/lobby/allgames/`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   
                console.log("getGames data: ", data);
                this.props.allGamesReceived(data);
            });
    }

    createNewGame()
    {
        this.props.gameInit();
        const urlData = {'name': this.props.lobbyPlayerName, 'bot': this.shouldWeIncludeBot() ? this.determineBotSymbol() : null};
        this.replaceState(urlData);
    }

    replaceState(data)
    {
        let url = `?`;
        const params = [];
        for (let dataItem in data)
        {
            params.push(encodeURIComponent(dataItem) + '=' + encodeURIComponent(data[dataItem]));
        }
        url += params.join('&');
        console.log("replaceState url: ", url);
        window.history.replaceState(null, null, url);
    }

    determineBotSymbol()
    {
        if (this.props.botXIsChecked && this.props.botOIsChecked)
        {
            return "XO";
        }
        if (this.props.botXIsChecked)
        {
            return "X";
        }
        if (this.props.botOIsChecked)
        {
            return "O";
        }
    }

    shouldWeIncludeBot()
    {
        return this.props.botXIsChecked || this.props.botOIsChecked;
    }

    render()
    {
        const games = this.printGamesList();

        return (
            <div>
                <Name/>
                Бот-X
                <Switch
                    onChange={this.props.botXButtonSwitched}
                    checked = { this.props.botXIsChecked }
                />
                Бот-O
                <Switch
                    onChange = { this.props.botOButtonSwitched }
                    checked = { this.props.botOIsChecked }
                />
                <button onClick = { () => this.createNewGame() }>Новая игра</button>
                <ol> { games } </ol>
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Lobby);