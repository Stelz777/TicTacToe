import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { connect } from 'react-redux';
import { gameBoardClicked, boardRequested } from '../actions/actions'


const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            history: [{ squares: Array(9).fill(null), }],
            reverseIsChecked: false,
            xIsNext: true,
            stepNumber: 0,
            highlights: Array(9).fill(false),
            //squares: Array(9).fill(0)
        }
    }
    else
    {
        return {
            history: state.history,
            reverseIsChecked: state.reverseIsChecked,
            stepNumber: state.status.stepNumber,
            xIsNext: state.status.xIsNext,
            highlights: state.highlights,
            board: state.board
        };
    }
}

const mapDispatchToProps =
{
    gameBoardClicked,
    boardRequested
}



class Board extends React.Component
{
    fetcher()
    {
        
    }

    componentDidMount()
    {
        console.log("mounted!");
        //window.history.replaceState(null, null, "/../../..");
        const urlParams = new URLSearchParams(window.location.search);
        const id = urlParams.get('id');
        fetch(`/api/farm/getboard/${id}`, { method: 'GET' })
            .then(result => result.json())
            .then(data => { 
                console.log(data.squares);  
                this.props.boardRequested(data.squares) 
            });
        
    }

    renderSquare(i, isHighlighted)
    {
        //boardRequested();
        console.log('inside rendersquare');
        
        /*async function getBoard()
        {
            await fetch(`api/board/getboard`, { method: 'POST'}).then(result => result.json()).then(data => squares = data.squares );
            //await promise;
            return squares;
        }*/
        //squares = this.componentDidMount();
        console.log("renderSquare squares: ", this.props.board);
        let symbol = "";
        if (this.props.board[i] === 0)
        {
            symbol = 'X';
        }
        else if (this.props.board[i] === 1)
        {
            symbol = 'O';
        }
        
        if (isHighlighted)
        {
            return (
                <HighlightedSquare 
                    value= { symbol }
                    onClick= { () => this.handleClick(i, this.props.board) }
                />
            );
        }
        else
        {
            return (
                <Square
                    value= { symbol }
                    onClick= { () => this.handleClick(i, this.props.board) }
                />
            );
        }
    }

    renderRow(i)
    {
        let squares = [];
        for (var j = 0; j < 3; j++)
        {
            if (this.props.highlights !== undefined)
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

    refreshBoard()
    {
        console.log("inside makeBotTurn");
        fetch(`/api/board/nextturn`, { method: 'POST' })
            .then((response) => response.json())
            .then((messages) => {
                console.log("bot turn: ", messages.cellNumber);
                if (messages.cellNumber >= 0)
                {
                    this.props.gameBoardClicked(messages.cellNumber, false)
                }
            });
    }

    handleClick(i, squares)
    {
        console.log("squares in handleclick: ", squares);
        this.props.gameBoardClicked(i, true);
        this.refreshBoard();
    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);