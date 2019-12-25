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
        const urlParams = new URLSearchParams(window.location.search);
        const id = urlParams.get('id');
        fetch(`/api/farm/getgame/${id === null ? '' : id}`, { method: 'GET' })
            .then(result => result.json())
            .then(data => { 
                console.log(data.squares);  

                this.props.boardRequested(data.board.squares.map(cell => cell === 0 ? 'X' : cell === 1 ? 'O' : null));
                window.history.replaceState(null, null, `?id=${data.id}`) 
            });
        
    }

    renderSquare(i, isHighlighted)
    {
        console.log('inside rendersquare');
        console.log("renderSquare squares: ", this.props.board);
        
        
        if (isHighlighted)
        {
            return (
                <HighlightedSquare 
                    value= { this.props.board[i] }
                    onClick= { () => this.handleClick(i, this.props.board) }
                />
            );
        }
        else
        {
            return (
                <Square
                    value= { this.props.board[i] }
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
        const urlParams = new URLSearchParams(window.location.search);
        const id = urlParams.get('id');
        fetch(`/api/board/nextturn/${id}`, { method: 'POST' })
            .then((response) => response.json())
            .then((messages) => {
                console.log("bot turn: ", messages.cellNumber);
                if (messages.cellNumber >= 0)
                {
                    this.props.gameBoardClicked(messages.cellNumber, false)
                }
            });
    }

    sendTurn(squareIndex, ticTurn)
    {
        console.log("gameBoardClicked squareIndex: ", squareIndex);
        console.log("gameBoardClicked ticTurn: ", ticTurn);
        const urlParams = new URLSearchParams(window.location.search);
        const id = urlParams.get('id');
        fetch(`/api/farm/setboard/${id}`, {
            method: 'POST',
            body: JSON.stringify({ CellNumber: squareIndex, TicTurn: ticTurn }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log("DATA: ", data);
            if (data)
            {
                this.props.gameBoardClicked(squareIndex, true);
                this.refreshBoard();
            }
        })
    }

    handleClick(i, squares)
    {
        this.sendTurn(i, true);
        //console.log("squares in handleclick: ", squares);
        
    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);