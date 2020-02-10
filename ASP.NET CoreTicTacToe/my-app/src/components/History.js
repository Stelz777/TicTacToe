import React from 'react';
import Switch from "react-switch";
import { connect } from 'react-redux';
import { historyButtonSwitched, historyItemClicked } from '../actions/actions';

const mapStateToProps = (state) =>
{
    if (!state)
    {
        return {
            history: null,
            reverseIsChecked: false,
        }
    }
    
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
        
        if (step < 0)
        {
            step = 0;
        }
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

    printMoveList(history, moves, previousStep)
    {
        if (history === null)
        {
            return null;
        }
        moves = history.map((step, move) => 
        {
            let i;
            let desc;
            if (!this.props.reverseIsChecked)
            {
                i = this.findDifferencesBetweenTwoArrays(step.squares, previousStep);
                desc = this.generateDescription(desc, move, i);
                previousStep = step.squares;
            }
            else
            {
                let reverseMove = history.length - move - 1;
                
                if (move + 1 < history.length)
                {
                    i = this.findDifferencesBetweenTwoArrays(history[move + 1].squares, history[move].squares);
                }
                else
                {
                    i = 0;
                }
                
                desc = this.generateDescription(desc, reverseMove, i);
            }
            
            return (
                <li key = { move } >
                    <button onClick = { this.props.reverseIsChecked ? () => this.jumpTo(history.length - move - 1, i) : () => this.jumpTo(move, i) }> { desc } </button>
                </li>
            );
        });
        return moves;
    }

    findDifferencesBetweenTwoArrays(step, previous)
    {
        if (step !== undefined)
        {
            for (let i = 0; i < step.length; i++)
            {
                if (step[i] !== previous[i])
                {
                    return i;
                }
            }
        }
    }

    generateDescription(desc, move, i)
    {
        desc = 'Перейти к ходу #' + (move + 1) + '(' + this.getColumn(i) + ' , ' + this.getRow(i) + ')';
        return desc;
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