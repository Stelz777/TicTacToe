import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import { connect } from 'react-redux';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        stepNumber: state.status.stepNumber,
        xIsNext: state.status.xIsNext,
        reverseIsChecked: state.reverseIsChecked
    };
}

class Info extends React.Component
{
    render() 
    {
        const history = this.props.history;
        let current;
        if (this.props.reverseIsChecked)
        {
            current = history[history.length - this.props.stepNumber - 1];
        }
        else
        {
            current = history[this.props.stepNumber];
        }
        const winner = CalculateWinner(current.squares);
        
        let status;
        if (winner)
        {
            status = 'Выиграл ' + winner;
        }
        else
        {
            status = 'Следующий ход: ' + (this.props.xIsNext ? 'X' : 'O');
        }

        return (
            <div className="game-info">
                <div> { status } </div>
                <History/>
            </div>
        );
    }
}

export default connect(mapStateToProps)(Info);