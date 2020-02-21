import React from 'react';
import { connect } from 'react-redux';
import { gameInit } from '../actions/commonActions';
import { historyInit } from '../actions/historyActions';
import Game from '../components/Game';
import Lobby from '../components/Lobby';
import utils from '../utility/utils';


const mapStateToProps = (state) =>
{
    return {
        isInGame: state.commonReducer.isInGame,
        isInLobby: state.commonReducer.isInLobby,
    }
}

const mapDispatchToProps = 
{
    gameInit,
    historyInit
}

class Screen extends React.Component
{
    render()
    {
        const id = utils.GetAllUrlParams().id;
        
        if (id)
        {
            this.props.gameInit();
            this.props.historyInit();
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