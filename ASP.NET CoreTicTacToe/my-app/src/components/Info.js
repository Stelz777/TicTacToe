import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import utils from '../utility/utils';
import { connect } from 'react-redux';
import Name from './Name';
import GetCurrentItem from '../gameLogic/GetCurrentItem';
import { lobbyInit } from '../actions/actions';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        stepNumber: state.status.stepNumber,
        xIsNext: state.status.xIsNext,
        reverseIsChecked: state.reverseIsChecked,
        ticPlayerName: state.ticPlayerName,
        tacPlayerName: state.tacPlayerName,
        isSpectator: state.isSpectator
    };
}

const mapDispatchToProps =
{
    lobbyInit
}

class Info extends React.Component
{
    calculateStatus(winner)
    {
        if (winner)
        {
            return 'Выиграл ' + winner;
        }
        else
        {
            return this.generateNextTurn();
        }
    }

    generateNextTurn()
    {
        console.log("generateNextTurn this.props.ticPlayerName: ", this.props.ticPlayerName);
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
            return utils.SplitLineToParagraphs(`${this.generatePhraseForSpectator(this.props.ticPlayerName, 'X')}\n${this.generatePhraseForSpectator(this.props.tacPlayerName, 'O')}`);
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
        window.history.replaceState(null, null, '../');
    }

    render() 
    {
        const history = this.props.history;
        let current = GetCurrentItem(history, this.props.reverseIsChecked, this.props.stepNumber);
        
        let winner = utils.ArrayNotNullOrEmpty(history) ? CalculateWinner(current.squares) : null; 
        let status = this.calculateStatus(winner);
        

        return (
            <div className="game-info">
                <Name/>
                <div> { this.renderSpectatorInfo() } </div>
                <div> { status } </div>
                <History/>
                <button onClick = { () => this.returnToLobby() }>Вернуться в лобби</button>
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Info);