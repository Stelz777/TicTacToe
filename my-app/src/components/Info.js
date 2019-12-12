import React from 'react';
import History from './History'
import CalculateWinner from '../gameLogic/CalculateWinner';
import { connect } from 'react-redux';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        stepNumber: state.stepNumber,
        xIsNext: state.xIsNext,
        checked: state.checked
    };
}

class Info extends React.Component
{
    render() 
    {
        const history = this.props.history;
        const current = history[this.props.stepNumber];

        const winner = CalculateWinner(current.squares);
        
        let status;
        if (winner)
        {
            status = 'Выиграл ' + winner;
        }
        else
        {
            if (this.props.checked && history.length % 2 == 0)
            {
                status = 'Следующий ход: ' + (this.props.xIsNext ? 'O' : 'X');
            }
            else
            {
                status = 'Следующий ход: ' + (this.props.xIsNext ? 'X' : 'O');
            }
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