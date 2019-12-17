import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { connect } from 'react-redux';
import { gameBoardClicked } from '../actions/actions'


const mapStateToProps = (state) =>
{
    if (state == undefined)
    {
        return {
            history: [{ squares: Array(9).fill(null), }],
            reverseIsChecked: false,
            xIsNext: true,
            stepNumber: 0,
            highlights: Array(9).fill(false),
        }
    }
    else
    {
        return {
            history: state.history,
            reverseIsChecked: state.reverseIsChecked,
            stepNumber: state.status.stepNumber,
            xIsNext: state.status.xIsNext,
            highlights: state.highlights
        };
    }
}

const mapDispatchToProps =
{
    gameBoardClicked
}

class Board extends React.Component
{
    /*componentDidMount() 
    {
        this.ensureDataFetched();
    }

    componentDidUpdate() 
    {
        this.ensureDataFetched();
    }

    ensureDataFetched() 
    {
        this.props.requestBoard();
    }*/

    renderSquare(i, isHighlighted)
    {
        const history = this.props.history;
        let current;
        if (this.props.reverseIsChecked)
        {
            current = history[history.length - this.props.stepNumber - 1];
        }
        else
        {
            current = history[this.props.stepNumber];
        }
        const squares = current.squares;
        if (isHighlighted)
        {
            return (
                <HighlightedSquare 
                    value= { squares[i] }
                    onClick= { () => this.handleClick(i) }
                />
            );
        }
        else
        {
            return (
                <Square
                    value= { squares[i] }
                    onClick= { () => this.handleClick(i) }
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

    handleGetJson()
    {
        console.log("inside handleGetJson");
        fetch(`Board`, {
            headers : { 
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        })
        .then((response) => response.json())
        .then((messages) => {
            console.log(messages.cellNumber);
            this.props.gameBoardClicked(messages.cellNumber)});
    }

    handleClick(i)
    {

        this.props.gameBoardClicked(i);
        this.handleGetJson();
        /*let botTurn = fetch(`board`)
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.error('Unable to get board.', error));*/
            //then(data => console.log(data));
        //console.log("fetched ", botTurn);

    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);