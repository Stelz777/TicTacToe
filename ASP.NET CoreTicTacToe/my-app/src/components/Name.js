import React from 'react';
import { connect } from 'react-redux';
import { sideReceived, spectatorResolved } from '../actions/actions'

const mapStateToProps = (state) =>
{
    if (state === undefined)
    {
        return {
            playerName: '',
            ticPlayerName: '',
            tacPlayerName: ''
        }
    }
    else
    {
        return {
            playerName: state.playerName,
            ticPlayerName: state.ticPlayerName,
            tacPlayerName: state.tacPlayerName
        };
    }
}

const mapDispatchToProps =
{
    sideReceived,
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
            && this.props.playerName === '')
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
        let id = new URLSearchParams(window.location.search).get('id');
        fetch(`/api/game/setname/${id}`, {
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
            console.warn(error);
        })
        .then(data => {
            
            this.props.sideReceived(data, name);
            
        })
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);