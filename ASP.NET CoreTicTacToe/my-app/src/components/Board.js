import React from 'react';
import { connect } from 'react-redux';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { botSet } from '../actions/botActions';
import { nameSet, playerNamesReceived, sideReceived } from '../actions/commonActions';
import { historyItemAdded, historyRequested } from '../actions/historyActions';
import GetCurrentItem from '../gameLogic/GetCurrentItem';
import { authHeader, authHeaderJSON } from '../helpers/authHeader';
import utils from '../utility/utils';

const mapStateToProps = (state) =>
{
    return {
        bot: state.botReducer.bot,
        board: state.historyReducer.board,
        clientPlayerName: state.commonReducer.clientPlayerName,
        highlights: state.historyReducer.highlights,
        history: state.historyReducer.history,
        reverseIsChecked: state.historyReducer.reverseIsChecked,
        side: state.commonReducer.side,
        stepNumber: state.historyReducer.status.stepNumber,
        xIsNext: state.historyReducer.status.xIsNext 
    }
};

const mapDispatchToProps = 
{
    botSet,
    historyItemAdded,
    historyRequested,
    nameSet,
    playerNamesReceived,
    sideReceived
}

class Board extends React.Component
{
    async componentDidMount()
    {
        await this.getGame();
    }

    getGame()
    {
        const id = utils.GetAllUrlParams().id;
        const bot = utils.GetAllUrlParams().bot;
        const difficulty = utils.GetAllUrlParams().difficulty;
        
        this.props.botSet(bot);
        const requestOptions = {
            method: 'GET',
            headers: authHeader()
        };
        fetch(`/api/lobby/game/${id || ''}?bot=${bot || ''}&difficulty=${difficulty || ''}`, requestOptions)
            .then(result => result.json())
            .then(data => {   
                this.fillSquares(data); 
                this.props.historyRequested(data.boards);

                const name = utils.GetAllUrlParams().name;
                if (name !== 'undefined')
                {
                    this.props.nameSet(name);
                }
                
                this.receiveSide(data.id, this.props.clientPlayerName);
                window.history.replaceState(null, null, `?id=${data.id}`);
                
                this.refreshBoard(null); 
            })
            .catch(error => {
                Promise.reject({
                    message: error
                })
            });
    }

    fillSquares(data)
    {
        for (var i = 0; i < data.boards.length; i++)
        {
            data.boards[i].squares = data.boards[i].squares.map(cell => cell === 0 ? 'X' : cell === 1 ? 'O' : null);
        }
    }

    receiveSide(id, name)
    {
        const requestOptions = {
            method: 'POST',
            headers: authHeader()
        }
        return fetch(`/api/game/setside/${id}?name=${name}`, requestOptions)
        .then(response => response.json())
        .then(data => {
                this.props.sideReceived(data);
        })
        .catch(error => {
            Promise.reject({
                message: error
            })
        });
    }

    refreshBoard(squareIndex)
    {
        const id = utils.GetAllUrlParams().id;   
        this.updates(id, squareIndex);
    }

    updates(id, squareIndex)
    {
        let currentTurn = this.props.history.length;
        this.callUpdatesAPI(id, squareIndex, currentTurn);
    }

    async callUpdatesAPI(id, squareIndex, currentTurn)
    {
        try
        {
            await this.updatesAPI(id, squareIndex, currentTurn);
        }
        catch (exception)
        {
            setTimeout(() => { this.refreshBoard(squareIndex) }, 500);
        }
    }

    async updatesAPI(id, squareIndex, currentTurn)
    {
        const requestOptions = {
            method: 'GET',
            headers: authHeader()
        };
        return fetch(`/api/game/updates/${id}?currentTurn=${currentTurn}`, requestOptions)
        .then((response) => response.json())
        .then((messages) => {
            let turns = messages.turns;
            for (var i = 0; i < turns.length; i++)
            {
                let receivedCell = turns[i].cellNumber;
                if (receivedCell >= 0)
                {
                    this.props.historyItemAdded(receivedCell, turns[i].side);
                    squareIndex = receivedCell;
                }
            }
            this.props.playerNamesReceived(messages.ticPlayerName, messages.tacPlayerName);
            if (messages.continueUpdating)
            {
                setTimeout(() => this.refreshBoard(squareIndex), 500);
            }
        });
    }

    render()
    {
        return (<div className="game-board"> { this.renderTable() } </div>);
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

    renderRow(index)
    {
        let squares = [];
        for (var j = 0; j < 3; j++)
        {
            squares = this.addSquareToRow(squares, index, j);
        }
        return squares;
    }

    addSquareToRow(squares, rowIndex, columnIndex)
    {
        if (this.props.highlights)
        {
            squares.push(this.renderSquare(rowIndex * 3 + columnIndex, this.props.highlights[rowIndex * 3 + columnIndex]));
        }
        return squares;
    }

    renderSquare(index, isHighlighted)
    {
        const history = this.props.history;
        
        let squares = utils.ArrayNotNullOrEmpty(history) ? GetCurrentItem(history, this.props.stepNumber).squares : this.props.board;
        
        if (isHighlighted)
        {
           return this.renderHighlightedSquare(squares, index);
        }
        return this.renderSimpleSquare(squares, index);
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

    handleClick(i)
    {
        
        if (this.props.clientPlayerName && this.isSideValid())
        {
            this.sendTurn(i, this.props.side);
        }
    }

    isSideValid()
    {
        let side = this.props.side;
        return side === 0 || side === 1;
    }

    sendTurn(squareIndex, side)
    {
        const id = utils.GetAllUrlParams().id;
        
        const requestOptions = {
            method: 'POST',
            headers: authHeaderJSON(),
            body: JSON.stringify({ CellNumber: squareIndex, Side: side }),
        };
        fetch(`/api/game/maketurn/${id}?name=${this.props.clientPlayerName}`, requestOptions)
        .then(response => response.json())
        .then(data => {
            if (data)
            {
                this.props.historyItemAdded(squareIndex, this.props.side);   
            }
        })
        .catch(error => {
            Promise.reject({
                message: error
            })
        })
        
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Board);

