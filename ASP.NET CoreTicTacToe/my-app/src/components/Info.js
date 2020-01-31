import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import { connect } from 'react-redux';
import Name from './Name'

const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            history: [{ squares: Array(9).fill(null), }],
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
        
        const winner = CalculateWinner(current.squares);
        
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