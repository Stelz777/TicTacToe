import React from 'react';
import Switch from "react-switch";
import { connect } from 'react-redux';
import { historyButtonSwitched, historyItemClicked } from '../actions/actions';

const mapStateToProps = (state) =>
{
    return {
        history: state.history,
        reverseIsChecked: state.reverseIsChecked
    };
}

const mapDispatchToProps = 
{
    historyButtonSwitched, 
    historyItemClicked
};


class History extends React.Component
{
    constructor(props)
    {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(checked)
    {
        this.props.historyButtonSwitched();
    }

    jumpTo(step, i)
    {   
        this.props.historyItemClicked(step, i);     
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
            if (!this.props.reverseIsChecked)
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
                    <button onClick = { this.props.reverseIsChecked ? () => this.jumpTo(history.length - move - 1, i) : () => this.jumpTo(move, i) }> { desc } </button>
                </li>
            );
        });
        return moves;
    }

    render()
    {
        const history = this.props.history;
        let previousStep;
        previousStep = Array(9).fill(null);
        let moves;

        moves = this.printMoveList(history, moves, previousStep)

        return (
            <div>
                <ol> { moves } </ol>
                <Switch
                    onChange = { this.handleChange }
                    checked = { this.props.reverseIsChecked }
                />
            </div>
        )
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(History);