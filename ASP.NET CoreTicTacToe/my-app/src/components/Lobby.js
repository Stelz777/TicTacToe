import React from 'react';
import { connect } from 'react-redux';
import { allGamesReceived, gameInit } from '../actions/actions';

const mapStateToProps = (state) =>
{
    return {
        games: state.games
    };
}

const mapDispatchToProps =
{
    allGamesReceived,
    gameInit
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
        console.log("getPlayerName player: ", player);
        return player.name || "свободно";
    }

    getPlayerBot(player)
    {
        return player.isBot ? "(бот)" : "";
    }

    showGame(gameIndex)
    {
        this.props.gameInit();
        window.history.replaceState(null, null, `?id=${gameIndex}`);
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
    }

    render()
    {
        const games = this.printGamesList();

        return (
            <div>
                <button onClick = { () => this.createNewGame() }>Новая игра</button>
                <ol> { games } </ol>
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Lobby);