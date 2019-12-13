import React from 'react';
import Board from './Board.js';
import Info from './Info.js';

class Game extends React.Component
{
    render()
    {
        return (
            <div className="game">  
                <Board/>
                <Info/>     
            </div> 
        );
    }
}

export default Game;

