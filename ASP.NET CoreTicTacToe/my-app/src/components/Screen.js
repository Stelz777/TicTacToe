import React from 'react';
import Game from '../components/Game';
import Lobby from '../components/Lobby';
import { connect } from 'react-redux';
import utils from '../utility/utils';
import { gameInit } from '../actions/actions';

const mapStateToProps = (state) =>
{
    return {
        isInGame: state.isInGame,
        isInLobby: state.isInLobby,
        playerName: state.playerName
    };
}

const mapDispatchToProps =
{
    gameInit
}

class Screen extends React.Component
{
    render()
    {
        const id = utils.GetAllUrlParams().id;
        console.log("screen render id: ", id);
        if (id)
        {
            this.props.gameInit();
        }

        if (this.props.isInLobby)
        {
            return (
                <div>
                    <Lobby />
                </div>
            )
        }
        if (this.props.isInGame)
        {
            console.log("screen render this.props.playerName: ", this.props.playerName)
            return (
                <div>  
                    <Game />     
                </div> 
            );
        }
        else
        {
            return null;
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Screen);