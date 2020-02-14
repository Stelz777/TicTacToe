import React from 'react';
import Board from './Board';
import Info from './Info';

class Game extends React.Component
{
    render()
    {
        console.log("game render!");
        return (
            <div className="game">  
                <Board/>
                <Info/>     
            </div> 
        );
    }
}

export default Game;

