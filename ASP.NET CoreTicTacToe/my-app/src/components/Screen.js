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
        isInLobby: state.isInLobby
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
        const urlParams = utils.CreateUrlParams();
        const id = utils.GetIdFromUrlParams(urlParams);
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