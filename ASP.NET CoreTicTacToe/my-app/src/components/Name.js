import React from 'react';
import { connect } from 'react-redux';
import { nameSetInLobby, spectatorResolved } from '../actions/actions'

const mapStateToProps = (state) =>
{
    return {
        lobbyPlayerName: state.lobbyPlayerName,
        ticPlayerName: state.ticPlayerName,
        tacPlayerName: state.tacPlayerName,
        isDisabledNameInput: state.isDisabledNameInput
    };
}

const mapDispatchToProps =
{
    nameSetInLobby,
    spectatorResolved
}

class Name extends React.Component
{
    
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

    nameTextBoxEventBody(event)
    {
        
            
        let name = event.target.value;
        console.log("nameTextBoxEventBody name: ", name);
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
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);