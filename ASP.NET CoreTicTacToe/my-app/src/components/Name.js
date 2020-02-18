import React from 'react';
import { connect } from 'react-redux';
import { nameSetInLobby, spectatorResolved } from '../actions/actions'
import utils from '../utility/utils';

const mapStateToProps = (state) =>
{
    return {
        isDisabledNameInput: state.isDisabledNameInput,
        lobbyPlayerName: state.lobbyPlayerName,
        ticPlayerName: state.ticPlayerName,
        tacPlayerName: state.tacPlayerName
    };
}

const mapDispatchToProps =
{
    nameSetInLobby,
    spectatorResolved
}

class Name extends React.Component
{
    componentDidMount()
    {
        const name = utils.GetAllUrlParams().name;
        this.props.nameSetInLobby(name);
    }

    render()
    {
        return (
            <input
                disabled = { this.props.isDisabledNameInput }
                type = "text"
                
                onBlur = { 
                    (event) =>
                    {
                        this.nameTextBoxEventBody(event)
                    }
                }
                onKeyDown = {
                    this.checkEventEnterKey()
                }
            />
        );
    }

    nameTextBoxEventBody(event)
    {
        let name = event.target.value;
        
        if (name !== '')
        {
            this.textChanged(name);
            event.target.disabled = true;
        }   
    }

    textChanged(name)
    {
        this.props.nameSetInLobby(name);    
    }

    checkEventEnterKey()
    {
        return (event) =>
        {
            if (event.key === 'Enter')
            {
                this.nameTextBoxEventBody(event);
            }
        }
    } 
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);