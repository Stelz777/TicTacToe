import React from 'react';
import { connect } from 'react-redux';
import { nameSetInLobby, spectatorResolved } from '../actions/actions'

const mapStateToProps = (state) =>
{
    return {
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
    
    render()
    {
        if (this.props.ticPlayerName !== null 
            && this.props.ticPlayerName !== '' 
            && this.props.tacPlayerName !== null 
            && this.props.tacPlayerName !== '' 
            && this.props.lobbyPlayerName === '')
        {
            console.log("spectator resolved!");
            this.props.spectatorResolved();
            return (null);
        }
        return (
            <input
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