import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import ArrayNotNullOrEmpty from '../utility/utils';
import { connect } from 'react-redux';
import Name from './Name';
import GetCurrentItem from '../gameLogic/GetCurrentItem';

const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            history: null,
            reverseIsChecked: false,
            xIsNext: true,
            stepNumber: 0,
            ticPlayerName: '',
            tacPlayerName: ''
        }
    }
    else
    {
        return {
            history: state.history,
            stepNumber: state.status.stepNumber,
            xIsNext: state.status.xIsNext,
            reverseIsChecked: state.reverseIsChecked,
            ticPlayerName: state.ticPlayerName,
            tacPlayerName: state.tacPlayerName
        };
    }
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

    render() 
    {
        const history = this.props.history;
        let current = GetCurrentItem(history, this.props.reverseIsChecked, this.props.stepNumber);
        
        let winner = ArrayNotNullOrEmpty(history) ? CalculateWinner(current.squares) : null; 
        let status = this.calculateStatus(winner);
        

        return (
            <div className="game-info">
                <Name/>
                <div> { status } </div>
                <History/>
            </div>
        );
    }
}

export default connect(mapStateToProps)(Info);