import React from 'react';
import { connect } from 'react-redux';
import History from './History'
import { lobbyInit, spectatorResolved } from '../actions/actions';
import CalculateWinner from '../gameLogic/CalculateWinner';
import GetCurrentItem from '../gameLogic/GetCurrentItem';
import utils from '../utility/utils';

const mapStateToProps = (state) =>
{
    return {
        clientPlayerName: state.nameReducer.clientPlayerName,
        history: state.historyReducer.history,
        isSpectator: state.commonReducer.isSpectator,
        reverseIsChecked: state.historyReducer.reverseIsChecked,
        stepNumber: state.historyReducer.status.stepNumber,
        tacPlayerName: state.commonReducer.tacPlayerName,
        ticPlayerName: state.commonReducer.ticPlayerName,
        xIsNext: state.historyReducer.status.xIsNext  
    }
}

const mapDispatchToProps = 
{
    lobbyInit,
    spectatorResolved  
}

class Info extends React.Component
{
    render() 
    {
        this.resolveSpectator();
        const history = this.props.history;
        let current = GetCurrentItem(history, this.props.reverseIsChecked, this.props.stepNumber);
        
        let winner = utils.ArrayNotNullOrEmpty(history) ? CalculateWinner(current.squares) : null; 
        let status = this.calculateStatus(winner);
        
        return (
            <div className="game-info">
                <div> { this.renderSpectatorInfo() } </div>
                <div> { status } </div>
                <History/>
                <button onClick = { () => this.returnToLobby() }>Вернуться в лобби</button>
            </div>
        );
    }

    resolveSpectator()
    {
        if (this.props.ticPlayerName 
            && this.props.tacPlayerName 
            && this.props.clientPlayerName !== this.props.ticPlayerName
            && this.props.clientPlayerName !== this.props.tacPlayerName)
        {
            this.props.spectatorResolved();
        }
    }

    calculateStatus(winner)
    {
        if (winner)
        {
            return 'Выиграл ' + this.generateWinnerPhrase(winner);
        }
        else
        {
            return this.generateNextTurn();
        }
    }

    generateWinnerPhrase(winner)
    {
        if (winner === 'X')
        {
            return `${this.props.ticPlayerName}(X)`; 
        }
        if (winner === 'O')
        {
            return `${this.props.tacPlayerName}(O)`;
        }
    }

    generateNextTurn()
    {
        let turnMark = this.props.reverseIsChecked
            ? this.props.xIsNext ? 'O' : 'X'
            : this.props.xIsNext ? 'X' : 'O';
        if (turnMark === 'X')
        {
            return this.generateFullNextTurnString(turnMark, this.props.ticPlayerName);
        }
        if (turnMark === 'O')
        {
            return this.generateFullNextTurnString(turnMark, this.props.tacPlayerName);
        }
    }

    generateFullNextTurnString(turnMark, playerName)
    {
        const nextTurnString = 'Следующий ход: ';
        if (playerName != null)
        {
            return nextTurnString + playerName + ' (' + turnMark + ')'; 
        }
        else
        {
            return nextTurnString + turnMark;
        }
    }

    renderSpectatorInfo()
    {
        if (this.props.isSpectator)
        {
            return utils.SplitLineToParagraphs(`${this.generatePhraseForSpectator(this.props.ticPlayerName, 'X')}
                                              \n${this.generatePhraseForSpectator(this.props.tacPlayerName, 'O')}`);
        }
        return null;
    }

    generatePhraseForSpectator(playerName, mark)
    {
        return 'Игрок ' + playerName + ' на стороне ' + mark;
    }

    returnToLobby()
    {
        this.props.lobbyInit();
        window.history.replaceState(null, null, `../?name=${this.props.clientPlayerName}`);
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Info);