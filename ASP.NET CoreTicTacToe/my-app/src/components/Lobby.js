import React from 'react';
import { connect } from 'react-redux';
import Switch from "react-switch";
import { botOButtonSwitched, botXButtonSwitched } from '../actions/botActions';
import { allGamesReceived, gameInit } from '../actions/commonActions';
import { historyInit } from '../actions/historyActions';
import Login from './Login';
import Registration from './Registration';
import utils from '../utility/utils';

const mapStateToProps = (state) =>
{
    return {
        botOIsChecked: state.botReducer.botOIsChecked,
        botXIsChecked: state.botReducer.botXIsChecked,
        games: state.commonReducer.games,
        isRegistering: state.registerReducer.isRegistering,
        lobbyPlayerFirstName: state.authenticationReducer.lobbyPlayerFirstName,
        lobbyPlayerLastName: state.authenticationReducer.lobbyPlayerLastName,
        lobbyPlayerName: state.authenticationReducer.lobbyPlayerName
    }
}

const mapDispatchToProps = 
{
    allGamesReceived,
    botOButtonSwitched,
    botXButtonSwitched,
    gameInit,
    historyInit
}

class Lobby extends React.Component
{
    componentDidMount()
    {
        this.getGames();
    }

    getGames()
    {
        const requestOptions = {
            method: 'GET'
        };
        fetch(`/api/lobby/allgames/`, requestOptions)
            .then(result => result.json())
            .then(data => {   
                this.props.allGamesReceived(data);
            });
    }

    render()
    {
        const games = this.printGamesList();
        console.log("lobby render this.props.lobbyPlayerName: ", this.props.lobbyPlayerName);
        console.log("Lobby render this.props.isRegistering", this.props.isRegistering);
        let greetings = `Здравствуйте, ${this.props.lobbyPlayerFirstName} ${this.props.lobbyPlayerLastName}`; 
        return (
            
            <div>
                { this.props.lobbyPlayerName ? greetings : null }
                { this.props.lobbyPlayerName || this.props.isRegistering ? null : <Login /> }
                { this.props.isRegistering ? <Registration /> : null }
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
        return `id: ${id} X: ${this.generatePlayerDescription(ticPlayer)}
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
        this.props.historyInit();
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
        this.props.historyInit();
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