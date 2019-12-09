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
            state => (
                { 
                    checked: !state.checked,
                    history: state.history.slice().reverse() 
                }
            )
        );

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
            if (!this.state.checked)
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
        const { enabled } = this.state;
        const history = this.state.history;
        const current = history[this.state.stepNumber];
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