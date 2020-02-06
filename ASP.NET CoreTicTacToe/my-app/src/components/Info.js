import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import ValidateArray from '../validation/validator';
import { connect } from 'react-redux';
import Name from './Name'

const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            history: null,
            reverseIsChecked: false,
            xIsNext: true,
            stepNumber: 0
        }
    }
    else
    {
        return {
            history: state.history,
            stepNumber: state.status.stepNumber,
            xIsNext: state.status.xIsNext,
            reverseIsChecked: state.reverseIsChecked
        };
    }
}

class Info extends React.Component
{
    getCurrentHistoryItem(history)
    {
        console.log("Info.getCurrentHistoryItem history: ", history);
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

    calculateStatus(winner)
    {
        if (winner)
        {
            return 'Выиграл ' + winner;
        }
        else
        {
            if (this.props.reverseIsChecked)
            {
                return 'Следующий ход: ' + (this.props.xIsNext ? 'O' : 'X');
            }
            else
            {
                return 'Следующий ход: ' + (this.props.xIsNext ? 'X' : 'O');
            }
            
        }
    }

    render() 
    {
        const history = this.props.history;
        let current = this.getCurrentHistoryItem(history);
        
        let winner = ValidateArray(history) ? CalculateWinner(current.squares) : null; 
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