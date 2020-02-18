import React from 'react';
import HighlightedSquare from './HighlightedSquare.js';
import Square from './Square.js';
import { connect } from 'react-redux';
import { gameBoardClicked, boardRequested, historyRequested, botSet, botIsX, playerNamesReceived, sideReceived, nameSet } from '../actions/actions';
import utils from '../utility/utils';
import GetCurrentItem from '../gameLogic/GetCurrentItem';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        reverseIsChecked: state.reverseIsChecked,
        stepNumber: state.status.stepNumber,
        xIsNext: state.status.xIsNext,
        highlights: state.highlights,
        side: state.side,
        clientPlayerName: state.clientPlayerName,
        bot: state.bot,
        board: state.board,
        testValue: state.testValue
    };
}

const mapDispatchToProps =
{
    gameBoardClicked,
    boardRequested,
    historyRequested,
    botSet,
    botIsX,
    playerNamesReceived,
    sideReceived,
    nameSet
}

class Board extends React.Component
{
    async componentDidMount()
    {
        console.log("Board componentdidmount!");
        await this.getGame();
    }

    receiveSide(id, name)
    {
        console.log("receiveSide name: ", name);
        return fetch(`/api/game/setside/${id}?name=${name}`, { 
            method: 'POST'
        })
        .then(
            response => response.json()
        )
        .then(
            data => {
                this.props.sideReceived(data);
            }
        )
    }

    getGame()
    {
        const id = utils.GetAllUrlParams().id;
        const bot = utils.GetAllUrlParams().bot;

        this.props.botSet(bot);
        console.log("getGame bot: ", bot);
        fetch(`/api/lobby/game/${id || ''}?bot=${bot || ''}`, { method: 'GET' })
            .then(result => result.json())
            .then(data => {   
                console.log("getGame data: ", data);
                this.fillSquares(data); 
                this.props.historyRequested(data.boards);

                const name = utils.GetAllUrlParams().name;
                this.props.nameSet(name);
                
                this.receiveSide(data.id, this.props.clientPlayerName);
                window.history.replaceState(null, null, `?id=${data.id}`);
                
                this.refreshBoard(null); 
            });
    }

    fillSquares(data)
    {
        for (var i = 0; i < data.boards.length; i++)
        {
            data.boards[i].squares = data.boards[i].squares.map(cell => cell === 0 ? 'X' : cell === 1 ? 'O' : null);
        }
    }

    refreshBoard(squareIndex)
    {
        const id = utils.GetAllUrlParams().id;   
        this.playBotVsBot(id);
        this.updates(id, squareIndex);
    }

    playBotVsBot(id)
    {
        if (this.props.bot.toLowerCase() === "xo")
        {
            fetch(`api/game/playBotVsBot/${id}`, { method: 'POST' });
        }
    }

    updates(id, squareIndex)
    {
        let currentTurn = this.props.history.length;
        console.log("updates id: ", id);
        console.log("updates squareIndex: ", squareIndex);
        console.log("updates currentTurn: ", currentTurn);
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
            console.log("callUpdatesAPI exception: ", exception);
            setTimeout(() => { this.refreshBoard(squareIndex) }, 500);
        }
    }

    async updatesAPI(id, squareIndex, currentTurn)
    {
        return fetch(`/api/game/updates/${id}?currentTurn=${currentTurn}`, { 
            method: 'GET'
        })
        .then((response) => response.json())
        .then((messages) => {

            console.log("updates messages: ", messages);
            let turns = messages.turns;
            for (var i = 0; i < turns.length; i++)
            {
                let receivedCell = turns[i].cellNumber;
                if (receivedCell >= 0)
                {
                    this.props.gameBoardClicked(receivedCell, turns[i].side);
                    squareIndex = receivedCell;
                }
            }
            this.props.playerNamesReceived(messages.ticPlayerName, messages.tacPlayerName);
                
            setTimeout(() => this.refreshBoard(squareIndex), 500);
        });
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
        
        let squares = utils.ArrayNotNullOrEmpty(history) ? GetCurrentItem(history, this.props.reverseIsChecked, this.props.stepNumber).squares : this.props.board;
        
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

    

    

    

    

    

    

    

    

    

    sendTurn(squareIndex, side)
    {
        const id = utils.GetAllUrlParams().id;
        
        fetch(`/api/game/maketurn/${id}?name=${this.props.clientPlayerName}`, {
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
        
        if (this.props.clientPlayerName !== '')
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

