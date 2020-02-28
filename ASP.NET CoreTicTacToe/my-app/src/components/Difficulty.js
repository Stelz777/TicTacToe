import React from 'react';
import { connect } from 'react-redux';
import { difficultySet } from '../actions/commonActions';

const mapStateToProps = (state) =>
{
    return {
        
    }
}

const mapDispatchToProps = 
{
    difficultySet
}

class Difficulty extends React.Component
{
    render()
    {
        return (
            <div className = "container">
                <div className = "row mt-5">
                    <div className = "col-sm-12">
                        <form onSubmit = { this.handleFormSubmit }>
                            <div className = "form-check">
                                <label>
                                    <input
                                        type = "radio"
                                        name = "difficulty"
                                        value = "simple"
                                        onChange = { this.handleOptionChange }
                                        className = "form-check-input"
                                    />
                                    Simple
                                </label>
                            </div>

                            <div className = "form-check">
                                <label>
                                    <input
                                        type = "radio"
                                        name = "difficulty"
                                        value = "minimax"
                                        onChange = { this.handleOptionChange }
                                        className = "form-check-input"
                                    />
                                    Minimax
                                </label>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );
    }

    handleOptionChange = changeEvent => {
        this.props.difficultySet(changeEvent.target.value);
    }

    handleFormSubmit = formSubmitEvent => {
        formSubmitEvent.preventDefault();
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Difficulty);