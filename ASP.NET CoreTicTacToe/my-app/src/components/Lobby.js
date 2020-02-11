import React from 'react';
import { connect } from 'react-redux';
import { allGamesReceived, gameRendered } from '../actions/actions';

const mapStateToProps = (state) =>
{
    if (!state)
    {
        return {
            gameIds: null,
            ticPlayers: null,
            tacPlayers: null
        }
    }

    return {
        gameIds: state.gameIds,
        ticPlayers: state.ticPlayers,
        tacPlayers: state.tacPlayers
    };
    
}

const mapDispatchToProps =
{
    allGamesReceived,
    gameRendered
}

class Lobby extends React.Component
{
    printGamesList()
    {
        setTimeout(() => { this.getGames() }, 500);
        const ids = this.props.gameIds;
        const ticPlayers = this.props.ticPlayers;
        const tacPlayers = this.props.tacPlayers;
        console.log(ids);
        console.log(ticPlayers);
        console.log(tacPlayers);
        if (ids && ticPlayers && tacPlayers)
        {
            let games = Array(ids.length);
            
            for (let i = 0; i < ids.length; i++)
            {
                
                let description = this.generateDescription(ids[i], ticPlayers[i], tacPlayers[i]);
                games[i] = (
                    <li key = { ids[i] } >
                        <button onClick = { () => this.showGame(ids[i]) }> { description} </button>
                    </li>
                );
            }
            return games;
        }
        return null;
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
        if (player.name)
        {
            return player.name;
        }
        else
        {
            return "свободно";
        }
    }

    getPlayerBot(player)
    {
        if (player.bot)
        {
            return "(бот)";
        }
        else
        {
            return "";
        }
    }

    showGame(gameIndex)
    {
        this.props.gameRendered();
        window.history.replaceState(null, null, `?id=${gameIndex}`);
    }

    

    getGames()
    {
        fetch(`/api/lobby/getallgames/`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   
                this.props.allGamesReceived(data.ids, data.ticPlayers, data.tacPlayers);
            });
    }

    createNewGame()
    {
        this.props.gameRendered();
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