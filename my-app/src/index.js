import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';

function Square(props)
{
    return (
        <button
        className="square"
        onClick=
        {
            props.onClick
        }
        >
            {
                props.value
            }
        </button>
    );
}

function HighlightedSquare(props)
{
    return (
        <button
        className="highlightedSquare"
        onClick=
        {
            props.onClick
        }
        >
            {
                props.value
            }
        </button>
    );
}

class Board extends React.Component
{
    

    renderSquare(i, isHighlighted)
    {
        if (isHighlighted)
        {
            return (
                <HighlightedSquare 
                value=
                {
                    this.props.squares[i]
                }
                onClick=
                {
                    () => this.props.onClick(i)
    
                }
                />
                );
        }
        else
        {
            return (
            <Square
            value=
            {
                this.props.squares[i]
            }
            onClick=
            {
                () => this.props.onClick(i)

            }
            />
            );
        }
    }

    render()
    {
        

        return(
            <div>
                <div className="board-row">
                    {
                        this.renderSquare(0, this.props.highlights[0])
                    }
                    {
                        this.renderSquare(1, this.props.highlights[1])
                    }
                    {
                        this.renderSquare(2, this.props.highlights[2])
                    }
                </div>

                <div className="board-row">
                    {
                        this.renderSquare(3, this.props.highlights[3])
                    }
                    {
                        this.renderSquare(4, this.props.highlights[4])
                    }
                    {
                        this.renderSquare(5, this.props.highlights[5])
                    }
                </div>

                <div className="board-row">
                    {
                        this.renderSquare(6, this.props.highlights[6])
                    }
                    {
                        this.renderSquare(7, this.props.highlights[7])
                    }
                    {
                        this.renderSquare(8, this.props.highlights[8])
                    }
                </div>
            </div>
        );
    }
}

class Game extends React.Component
{
    constructor(props)
    {
        super(props);
        let highlights = Array(9).fill(false);
        this.state = 
        {
            history: 
            [
                {
                    squares: Array(9).fill(null),
                }
            ],
            stepNumber: 0,
            xIsNext: true,
            currentColumn: 1,
            currentRow: 1,
            highlights: highlights,
        }
        
    }

    handleClick(i)
    {
        const history = this.state.history.slice(0, this.state.stepNumber + 1);
        const current = history[history.length - 1];
        const squares = current.squares.slice();
       
        
        if (calculateWinner(squares) || squares[i])
        {
            return;
        }
        squares[i] = this.state.xIsNext ? 'X' : 'O';
        
        
        
        this.setState(
            {
                history: history.concat(
                    [
                        {
                            squares: squares,
                            
                        }
                    ]
                ),
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
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const winner = calculateWinner(current.squares);
        const squares = current.squares.slice();
        let previousStep = Array(9).fill(null);
        const moves = history.map((step, move) => 
        {
            let i = this.findDifferencesBetweenTwoArrays(step.squares, previousStep);
            const desc = move ? 'Перейти к ходу #' + move + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')' : 'К началу игры';
            previousStep = step.squares;
            return (
                <li
                key = 
                {
                    move
                }
                >
                    <button
                    onClick = 
                    {
                        () => this.jumpTo(move, i)

                    }
                    >
                        {
                            desc
                        }

                    </button>
                </li>
            );
        });

        let status;
        if (winner)
        {
            status = 'Выиграл ' + winner;
        }
        else
        {
            status = 'Следующий ход: ' + (this.state.xIsNext ? 'X' : 'O');
        }
        
        return(
            <div className="game">
                <div className="game-board">
                    <Board
                    highlights = 
                    {
                        this.state.highlights
                    }
                    squares = 
                    {
                        current.squares
                    }
                    onClick = 
                    {
                        (i) => this.handleClick(i)
                    }
                    />
                </div>
                <div className="game-info">
                    <div>
                        {
                            status
                        }
                    </div>
                    <ol>
                        {
                            moves
                        }
                    </ol>
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <Game />,
    document.getElementById('root')
);

function calculateWinner(squares)
{
    const lines = 
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],
    ];
    for (let i = 0; i < lines.length; i++)
    {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c])
        {
            return squares[a];
        }
    }
    return null;
}

