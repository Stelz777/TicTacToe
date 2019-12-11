import React from 'react';
import CalculateWinner from '../gameLogic/CalculateWinner';
import Switch from "react-switch";
import { connect } from 'react-redux';
import { checkedFlip, historyChanged, step, xIsNextByStep, highlightsChanged } from '../actions/index';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        stepNumber: state.stepNumber,
        xIsNext: state.xIsNext,
        checked: state.checked
    };
}

function mapDispatchToProps(dispatch)
{
    return {
        checkedFlip: () => dispatch(checkedFlip()),
        historyChanged: () => dispatch(historyChanged()),
        step: stepInput => dispatch(step(stepInput)),
        xIsNextByStep: step => dispatch(xIsNextByStep(step)),
        highlightsChanged: highlightsInput => dispatch(highlightsChanged(highlightsInput))
    };
}

class Info extends React.Component
{
    constructor(props)
    {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(checked)
    {
        this.props.checkedFlip();   
        this.props.historyChanged();
    }

    jumpTo(step, i)
    {
        let highlights = Array(9).fill(false);
        highlights[i] = true;    
        this.props.step(step);
        this.props.xIsNextByStep(step);
        this.props.highlightsChanged(highlights);      
    }

    getRow(i)
    {
        return Math.floor(i / 3) + 1;
    }

    getColumn(i)
    {
        return i % 3 + 1;
    }

    generateDescription(desc, condition, move, i)
    {
        desc = condition ? 'Перейти к ходу #' + move + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')' : 'К началу игры';
        return desc;
    }

    findDifferencesBetweenTwoArrays(step, previous)
    {
        if (step != undefined)
        {
            for (let i = 0; i < step.length; i++)
            {
                if (step[i] != previous[i])
                {
                    return i;
                }
            }
        }
    }

    printMoveList(history, moves, previousStep)
    {
        moves = history.map((step, move) => 
        {
            let i;
            let desc;
            if (!this.props.checked)
            {
                i = this.findDifferencesBetweenTwoArrays(step.squares, previousStep);
                desc = this.generateDescription(desc, move, move, i);
            }
            else
            {
                if (history[move + 1] != undefined)
                {
                    i = this.findDifferencesBetweenTwoArrays(step.squares, history[move + 1].squares);
                }
                else
                {
                    i = 0;
                }
                let reverseMove = history.length - move - 1;
                desc = this.generateDescription(desc, ((reverseMove) !== 0), reverseMove, i);
            }
            previousStep = step.squares;
            return (
                <li key = { move } >
                    <button onClick = { () => this.jumpTo(move, i) }> { desc } </button>
                </li>
            );
        });
        return moves;
    }

    render() 
    {
        const history = this.props.history;
        const current = history[this.props.stepNumber];

        let previousStep;
        previousStep = Array(9).fill(null);
        let moves;

        moves = this.printMoveList(history, moves, previousStep)
        
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
            <div>
                <div> { status } </div>
                <ol> { moves } </ol>
                <Switch
                    onChange = { this.handleChange }
                    checked = { this.props.checked }
                />
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Info);