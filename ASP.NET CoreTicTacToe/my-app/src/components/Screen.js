import React from 'react';
import Game from '../components/Game';
import Lobby from '../components/Lobby';
import { connect } from 'react-redux';
import utils from '../utility/utils';
import { gameRendered } from '../actions/actions';

const mapStateToProps = (state) =>
{
    if (!state)
    {
        return {
            isInGame: false
        }
    }

    return {
        isInGame: state.isInGame
    };
    
}

const mapDispatchToProps =
{
    gameRendered
}

class Screen extends React.Component
{
    render()
    {
        const urlParams = utils.CreateUrlParams();
        const id = utils.GetIdFromUrlParams(urlParams);
        if (id)
        {
            this.props.gameRendered();
        }

        if (!this.props.isInGame)
        {
            return (
                <div>
                    <Lobby />
                </div>
            )
        }
        return (
            <div>  
                <Game />     
            </div> 
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Screen);