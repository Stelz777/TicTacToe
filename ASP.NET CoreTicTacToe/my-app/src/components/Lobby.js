import React from 'react';
import { connect } from 'react-redux';
import Switch from "react-switch";
import { allGamesReceived, botOButtonSwitched, botXButtonSwitched, gameInit } from '../actions/actions';
import Name from '../components/Name'
import utils from '../utility/utils';

const mapStateToProps = (state) =>
{
    return {
        botOIsChecked: state.botOIsChecked,
        botXIsChecked: state.botXIsChecked,
        games: state.games,
        lobbyPlayerName: state.lobbyPlayerName
    };
}

const mapDispatchToProps =
{
    allGamesReceived,
    botOButtonSwitched,
    botXButtonSwitched,
    gameInit
}

class Lobby extends React.Component
{
    componentDidMount()
    {
        this.getGames();
    }

    getGames()
    {
        fetch(`/api/lobby/allgames/`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   
                this.props.allGamesReceived(data);
            });
    }

    render()
    {
        const games = this.printGamesList();

        return (
            <div>
                <Name/>
                <div>
                    Бот-X
                    <Switch
                        onChange={this.props.botXButtonSwitched}
                        checked = { this.props.botXIsChecked }
                    />
                </div>
                <div>
                    Бот-O
                    <Switch
                        onChange = { this.props.botOButtonSwitched }
                        checked = { this.props.botOIsChecked }
                    />
                </div>    
                <button onClick = { () => this.createNewGame() }>Новая игра</button>
                <ol> { games } </ol>
            </div>
        );
    }

    printGamesList()
    {
        const games = this.props.games;
        
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
        const urlData = {'name': this.props.lobbyPlayerName, 'id': gameIndex, 'bot': this.shouldWeIncludeBot() ? this.determineBotSymbol() : null};
        this.replaceState(urlData);
    }

    shouldWeIncludeBot()
    {
        return this.props.botXIsChecked || this.props.botOIsChecked;
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

    createNewGame()
    {
        this.props.gameInit();
        const urlData = {'name': this.props.lobbyPlayerName, 'bot': this.shouldWeIncludeBot() ? this.determineBotSymbol() : null};
        this.replaceState(urlData);
    }

    replaceState(data)
    {
        let url = `?`;
        url += utils.BuildUrlParams(data);
        window.history.replaceState(null, null, url);
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Lobby);