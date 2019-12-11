import React from 'react';
import Board from './Board.js';
import Info from './Info.js';
import { connect } from 'react-redux';
import { init, historyConcat, stepChanged, xIsNextFlip } from '../actions/index'
import CalculateWinner from '../gameLogic/CalculateWinner';


const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        stepNumber: state.stepNumber,
        xIsNext: state.xIsNext,
        highlights: state.highlights
    };
}

function mapDispatchToProps(dispatch)
{
    return {
        init: () => dispatch(init()),
        historyConcat: (history, squares) => dispatch(historyConcat(history, squares)),
        stepChanged: history => dispatch(stepChanged(history)),
        xIsNextFlip: () => dispatch(xIsNextFlip()),
    };
}

class Game extends React.Component
{
    constructor(props)
    {
        super(props);
        this.props.init();
    }

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
        this.props.historyConcat(history, squares);
        this.props.stepChanged(history);
        this.props.xIsNextFlip();
    }

    render()
    {
        const history = this.props.history;
        const current = history[this.props.stepNumber];
        
        return (
            
                <div className="game">
                    <div className="game-board">
                        <Board
                            highlights = { this.props.highlights }
                            squares = { current.squares }
                            onClick = { (i) => this.handleClick(i) }
                        />
                    </div>
                    <div className="game-info">
                        <Info></Info>
                    </div>
                        
                </div> 
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Game);

