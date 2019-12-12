import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';

class Board extends React.Component
{
    renderSquare(i, isHighlighted)
    {
        //alert(i, isHighlighted);
        if (isHighlighted)
        {
            return (
                <HighlightedSquare 
                    value= { this.props.squares[i] }
                    onClick= { () => this.props.onClick(i) }
                />
            );
        }
        else
        {
            return (
                <Square
                    value= { this.props.squares[i] }
                    onClick= { () => this.props.onClick(i) }
                />
            );
        }
    }

    renderRow(i)
    {
        let squares = [];
        for (var j = 0; j < 3; j++)
        {
            if (this.props.highlights != undefined)
            {
                squares.push(this.renderSquare(i * 3 + j, this.props.highlights[i * 3 + j]));
            }
        }
        return squares;
    }

    renderTable()
    {
        let rows = [];
        for (var i = 0; i < 3; i++)
        {
            rows.push(<div className="board-row"> { this.renderRow(i) } </div>);
        } 
        return rows;
    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default Board;