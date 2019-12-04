import React from 'react';
import CalculateWinner from './CalculateWinner.js';
import Board from './Board.js';
import Switch from "react-switch";

class Game extends React.Component
{
    constructor(props)
    {
        super(props);
        let highlights = Array(9).fill(false);
        this.state = 
        {
            history: [{ squares: Array(9).fill(null), }],
            stepNumber: 0,
            xIsNext: true,
            currentColumn: 1,
            currentRow: 1,
            highlights: highlights,
            checked: false,
        }
        this.handleChange = this.handleChange.bind(this);
        
    }

    handleChange(checked)
    {
        this.setState(
            { 
                checked: !this.state.checked,
                history: this.state.history.reverse() 
            });

    }

    handleClick(i)
    {
        const history = this.state.history.slice(0, this.state.stepNumber + 1);
        const current = history[history.length - 1];
        const squares = current.squares.slice();
       
        if (CalculateWinner(squares) || squares[i])
        {
            return;
        }
        squares[i] = this.state.xIsNext ? 'X' : 'O';
        
        this.setState(
            {
                history: history.concat([{ squares: squares, }]),
                stepNumber: history.length,
                xIsNext: !this.state.xIsNext,
            }
        );
    }

    jumpTo(step, i)
    {
        let highlights = Array(9).fill(false);
        highlights[i] = true;          
        this.setState(
            {
                stepNumber: step,
                xIsNext: (step % 2) === 0,
                highlights: highlights,
            }
        );
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

    render()
    {
        const { enabled } = this.state;
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const winner = CalculateWinner(current.squares);
        const squares = current.squares.slice();
        let nextStep = 1;
        let previousStep;
        previousStep = Array(9).fill(null);
        let moves;
        if (!this.state.checked)
        {
            moves = history.map((step, move) => 
            {
                let i = this.findDifferencesBetweenTwoArrays(step.squares, previousStep);
                let desc;
                desc = move ? 'Перейти к ходу #' + move + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')' : 'К началу игры';
                previousStep = step.squares;
                
                return (
                    <li key = { move } >
                        <button onClick = { () => this.jumpTo(move, i) }> { desc } </button>
                    </li>
                );
            });
        }
        else
        {
            moves = history.slice().map((step, move) => 
            {
                let i;
                if (history[move + 1] != undefined)
                {
                    i = this.findDifferencesBetweenTwoArrays(step.squares, history[move + 1].squares);
                }
                else 
                {
                    i = 0;
                }
                let desc;
                desc = ((history.length - move - 1) !== 0) ? 'Перейти к ходу #' + (history.length - move - 1) + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')' : 'К началу игры';
                previousStep = step.squares;
                return (
                    <li key = { move } >
                        <button onClick = { () => this.jumpTo(move, i) }> { desc } </button>
                    </li>
                );
            });
        }
        
        let status;
        if (winner)
        {
            status = 'Выиграл ' + winner;
        }
        else
        {
            status = 'Следующий ход: ' + (this.state.xIsNext ? 'X' : 'O');
        }
        
        return (
            
                <div className="game">
                    <div className="game-board">
                        <Board
                            highlights = { this.state.highlights }
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
                        checked = { this.state.checked }
                    />    
                </div> 
        );
    }
}

export default Game;