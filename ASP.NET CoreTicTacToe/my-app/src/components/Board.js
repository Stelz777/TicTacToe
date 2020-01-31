import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { connect } from 'react-redux';
import { gameBoardClicked, boardRequested, historyRequested } from '../actions/actions'

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
            side: 0,
            playerName: ''
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
            side: state.side,
            playerName: state.playerName
        };
    }
}

const mapDispatchToProps =
{
    gameBoardClicked,
    boardRequested,
    historyRequested
}

class Board extends React.Component
{
    createUrlParams()
    {
        return new URLSearchParams(window.location.search);
    }

    getIdFromUrlParams(urlParams)
    {
        return urlParams.get('id');
    }

    componentDidMount()
    {
        this.getGame();
        
    }

    getGame()
    {
        const urlParams = this.createUrlParams();
        const id = this.getIdFromUrlParams(urlParams);
        fetch(`/api/farm/getgame/${id === null ? '' : id}`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   

                this.fillSquares(data); 
                this.props.historyRequested(data.boards);
                window.history.replaceState(null, null, `?id=${data.id}`);
                this.refreshBoard(-1); 
            });
    }

    fillSquares(data)
    {
        for (var i = 0; i < data.boards.length; i++)
        {
            data.boards[i].squares = data.boards[i].squares.map(cell => cell === 0 ? 'X' : cell === 1 ? 'O' : null);
        }
    }

    getCurrentHistoryItem(history)
    {
        if (this.props.reverseIsChecked)
        {
            return history[this.props.stepNumber];
        }
        else
        {
            if (history[this.props.stepNumber + 1] !== undefined)
            {
                return history[this.props.stepNumber + 1];
            }
            
            return history[this.props.stepNumber];
            
            
        }
    }

    renderHighlightedSquare(current, index)
    {
        return (
            <HighlightedSquare 
                value= { current.squares[index] }
                onClick= { () => this.handleClick(index) }
            />
        );
    }

    renderSimpleSquare(current, index)
    {
        return (
            <Square
                value= { current.squares[index] }
                onClick= { () => this.handleClick(index) }
            />
        );
    }

    renderSquare(index, isHighlighted)
    {
        const history = this.props.history;
        let current = this.getCurrentHistoryItem(history);
        let square;
        if (isHighlighted)
        {
            square = this.renderHighlightedSquare(current, index);
        }
        else
        {
            square = this.renderSimpleSquare(current, index);
        }
        return square;
    }

    addSquareToRow(squares, rowIndex, columnIndex)
    {
        if (this.props.highlights !== undefined)
        {
            squares.push(this.renderSquare(rowIndex * 3 + columnIndex, this.props.highlights[rowIndex * 3 + columnIndex]));
        }
        return squares;
    }

    renderRow(index)
    {
        let squares = [];
        for (var j = 0; j < 3; j++)
        {
            squares = this.addSquareToRow(squares, index, j);
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

    refreshBoard(squareIndex)
    {
        const urlParams = this.createUrlParams();
        const id = this.getIdFromUrlParams(urlParams);
        fetch(`/api/game/updates/${id}`, { 
            method: 'POST',
            body: JSON.stringify({ Name: this.props.playerName }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then((response) => response.json())
        .then((messages) => {
            console.log("refreshboard messages: ", messages); 
            if (messages.lastTurn !== undefined)
            {
                let receivedCell = messages.lastTurn.cellNumber;
                if (receivedCell >= 0 && receivedCell !== squareIndex)
                {
                    this.props.gameBoardClicked(receivedCell, messages.lastTurn.whichTurn);
                    squareIndex = receivedCell;
                }
            }
            setTimeout(() => { this.refreshBoard(squareIndex) }, 10000);
        });
    }

    sendTurn(squareIndex, whichTurn)
    {
        const urlParams = this.createUrlParams();
        const id = this.getIdFromUrlParams(urlParams);
        fetch(`/api/game/maketurn/${id}`, {
            method: 'POST',
            body: JSON.stringify({ CellNumber: squareIndex, WhichTurn: whichTurn }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data)
            {
                this.props.gameBoardClicked(squareIndex, this.props.side);
                this.refreshBoard(squareIndex);
            }
        })
    }

    handleClick(i)
    {
        console.log("handleClick side: ", this.props.side);
        this.sendTurn(i, this.props.side);
    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);

