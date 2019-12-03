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

    renderTable()
    {
        let rows = [];
        for (var i = 0; i < 3; i++)
        {
            let squares = [];
            for (var j = 0; j < 3; j++)
            {
                squares.push(this.renderSquare(i * 3 + j, this.props.highlights[i * 3 + j]));
            }
            rows.push(<div className="board-row"> { squares } </div>);
        } 
        return rows;
    }

    render()
    {
        return (
            <div> { this.renderTable() } </div>
        );
    }
}

export default Board;