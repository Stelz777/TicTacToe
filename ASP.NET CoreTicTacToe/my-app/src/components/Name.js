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
        console.log("textChanged name: ", name);
        fetch(`/api/lobby/addplayer/`, {
            method: 'POST',
            body: JSON.stringify({ Name: name }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            
            return (response.ok) ? response.json() : Promise.reject(response.status);
            
        })
        .catch((error) => {
            console.warn(error)
        })
        .then(data => {
            console.log("textchanged then data name: ", name);
            this.props.nameSetInLobby(name);
            console.log("textchanged then data this.props.lobbyPlayerName: ", this.props.lobbyPlayerName);
        })
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);