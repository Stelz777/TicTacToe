import React from 'react';
import Board from './Board.js';
import Info from './Info.js';
import { connect } from 'react-redux';
import { gameBoardClicked } from '../actions/actions'
import CalculateWinner from '../gameLogic/CalculateWinner';


const mapStateToProps = (state) =>
{
    return {
        history: state.history.history,
        stepNumber: state.game.status.stepNumber,
        xIsNext: state.game.status.xIsNext,
        highlights: state.game.highlights
    };
}

const mapDispatchToProps =
{
    gameBoardClicked
}

class Game extends React.Component
{
    handleClick(i)
    {
        const history = this.props.history.slice(0, this.props.stepNumber + 1);
        const current = history[history.length - 1];
        const squares = current.squares.slice();
       
        if (CalculateWinner(squares) || squares[i])
        {
            return;
        }
        squares[i] = this.props.xIsNext ? 'X' : 'O';
        this.props.gameBoardClicked(history, squares);
    }

    render()
    {
        const history = this.props.history;
        const current = history[this.props.stepNumber];

        return (
                <div className="game">
                    
                    <Board
                        highlights = { this.props.highlights }
                        squares = { current.squares }
                        onClick = { (i) => this.handleClick(i) }
                    />
                    <Info/>     
                </div> 
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Game);

