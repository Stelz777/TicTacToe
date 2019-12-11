import React from 'react';
import CalculateWinner from './CalculateWinner.js';
import Board from './Board.js';
import Switch from "react-switch";
import store from "../store/configureStore"
import { connect } from 'react-redux';
import { init, checked, historyChanged, historyConcat, stepChanged, step, xIsNext, xIsNextByStep, highlightsChanged } from '../actions/index'

const mapStateToProps = state =>
{
    return {
        history: store.history,
        stepNumber: store.stepNumber,
        xIsNext: store.xIsNext,
        highlights: store.highlights,
        checked: store.checked
    };
}

function mapDispatchToProps(dispatch)
{
    return {
        init: () => dispatch(init()),
        checked: () => dispatch(checked()),
        historyChanged: () => dispatch(historyChanged()),
        historyConcat: (history, squares) => dispatch(historyConcat(history, squares)),
        stepChanged: history => dispatch(stepChanged(history)),
        step: stepInput => dispatch(step(stepInput)),
        xIsNext: () => dispatch(xIsNext()),
        xIsNextByStep: step => dispatch(xIsNextByStep(step)),
        highlightsChanged: highlightsInput => dispatch(highlightsChanged(highlightsInput))

    };
}

class Game extends React.Component
{
    constructor(props)
    {
        super(props);
        this.props.init();
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(checked)
    {
        this.props.checked();   
        this.props.historyChanged();
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
        this.props.xIsNext();
    }

    jumpTo(step, i)
    {
        let highlights = Array(9).fill(false);
        highlights[i] = true;    
        this.props.step(step);
        this.props.xIsNextByStep(step);
        this.props.highlightsChanged(highlights);      
    }

    findDifferencesBetweenTwoArrays(step, previous)
    {
        if (step != undefined)
        {
            for (let i = 0; i < step.length; i++)
            {
                if (step[i] != previous[i])
                {
                    return i;
                }
            }
        }
    }

    getRow(i)
    {
        return Math.floor(i / 3) + 1;
    }

    getColumn(i)
    {
        return i % 3 + 1;
    }

    generateDescription(desc, condition, move, i)
    {
        desc = condition ? 'Перейти к ходу #' + move + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')' : 'К началу игры';
        return desc;
    }

    printMoveList(history, moves, previousStep)
    {
        moves = history.map((step, move) => 
        {
            let i;
            let desc;
            if (!this.props.checked)
            {
                i = this.findDifferencesBetweenTwoArrays(step.squares, previousStep);
                desc = this.generateDescription(desc, move, move, i);
            }
            else
            {
                if (history[move + 1] != undefined)
                {
                    i = this.findDifferencesBetweenTwoArrays(step.squares, history[move + 1].squares);
                }
                else
                {
                    i = 0;
                }
                let reverseMove = history.length - move - 1;
                desc = this.generateDescription(desc, ((reverseMove) !== 0), reverseMove, i);
            }
            previousStep = step.squares;
            return (
                <li key = { move } >
                    <button onClick = { () => this.jumpTo(move, i) }> { desc } </button>
                </li>
            );
        });
        return moves;
    }

    render()
    {
        //const { enabled } = this.state;
        const history = this.props.history;
        const current = history[this.props.stepNumber];
        const winner = CalculateWinner(current.squares);
        const squares = current.squares.slice();
        let nextStep = 1;
        let previousStep;
        previousStep = Array(9).fill(null);
        let moves;
        
        moves = this.printMoveList(history, moves, previousStep)
        
        let status;
        if (winner)
        {
            status = 'Выиграл ' + winner;
        }
        else
        {
            status = 'Следующий ход: ' + (this.props.xIsNext ? 'X' : 'O');
        }
        
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
                        <div> { status } </div>
                        <ol> { moves } </ol>
                    </div>
                    <Switch
                        onChange = { this.handleChange }
                        checked = { this.props.checked }
                    />    
                </div> 
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Game);

