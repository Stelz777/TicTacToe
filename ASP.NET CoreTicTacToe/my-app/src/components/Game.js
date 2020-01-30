import React from 'react';
import Board from './Board';
import Info from './Info';
import Name from './Name';

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

