import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { connect } from 'react-redux';
import { gameBoardClicked, boardRequested, historyRequested, sideReceived, botSet, botIsX } from '../actions/actions';
import ValidateArray from '../validation/validator';

const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            history: null,
            reverseIsChecked: false,
            xIsNext: true,
            stepNumber: 0,
            highlights: Array(9).fill(false),
            side: 0,
            playerName: '',
            bot: '',
            board: Array(9).fill(null)
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
            playerName: state.playerName,
            bot: state.bot,
            board: state.board
        };
    }
}

const mapDispatchToProps =
{
    gameBoardClicked,
    boardRequested,
    historyRequested,
    sideReceived,
    botSet,
    botIsX
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

    getBotFromUrlParams(urlParams)
    {
        return urlParams.get('bot');
    }

    componentDidMount()
    {
        this.getGame();
        
    }

    getGame()
    {
        const urlParams = this.createUrlParams();
        const id = this.getIdFromUrlParams(urlParams);
        const bot = this.getBotFromUrlParams(urlParams);
        
        this.props.botSet(bot);
        fetch(`/api/farm/getgame/${id === null ? '' : id}?bot=${bot == null ? '' : bot}`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   
                
                this.fillSquares(data); 
                this.props.historyRequested(data.boards);
                window.history.replaceState(null, null, `?id=${data.id}`);
                if (bot === "X")
                {
                    this.props.botIsX();
                }
                if (bot !== "XO")
                {
                    this.refreshBoard(null); 
                }
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
        if (history === null)
        {
            return null;
        }
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

    renderHighlightedSquare(squares, index)
    {
        return (
            <HighlightedSquare 
                value= { squares[index] }
                onClick= { () => this.handleClick(index) }
            />
        );
    }

    renderSimpleSquare(squares, index)
    {
        return (
            <Square
                value= { squares[index] }
                onClick= { () => this.handleClick(index) }
            />
        );
    }

    renderSquare(index, isHighlighted)
    {
        const history = this.props.history;
        console.log("renderSquare history: ", history);
        let current = ValidateArray(history) ? this.getCurrentHistoryItem(history) : this.props.board;
        console.log("renderSquare current: ", current);
        let square;
        let squares = ValidateArray(history) ? current.squares : current;
        console.log("renderSquare squares: ", squares);
        if (isHighlighted)
        {
            square = this.renderHighlightedSquare(squares, index);
        }
        else
        {
            square = this.renderSimpleSquare(squares, index);
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
        this.updates(id, squareIndex);
    }

    

    updates(id, squareIndex)
    {
        
        let currentTurn = this.props.history.length;
        
        fetch(`/api/game/updates/${id}?currentTurn=${currentTurn}`, { 
            method: 'GET'
        })
        .then((response) => response.json())
        .then((messages) => {
            
            for (var i = 0; i < messages.length; i++)
            {
                let receivedCell = messages[i].cellNumber;
                if (receivedCell >= 0)
                {
                    this.props.gameBoardClicked(receivedCell, messages[i].side);
                    squareIndex = receivedCell;
                }
            }
            
            setTimeout(() => { this.refreshBoard(squareIndex) }, 10000);
        });
    }

    sendTurn(squareIndex, side)
    {
        
        const urlParams = this.createUrlParams();
        const id = this.getIdFromUrlParams(urlParams);
        fetch(`/api/game/maketurn/${id}`, {
            method: 'POST',
            body: JSON.stringify({ CellNumber: squareIndex, Side: side }),
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
                
            }
        })
    }

    handleClick(i)
    {
        
        if (this.props.playerName !== '')
        {
            this.sendTurn(i, this.props.side);
        }
    }

    render()
    {
        return (
            <div className="game-board"> { this.renderTable() } </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);

