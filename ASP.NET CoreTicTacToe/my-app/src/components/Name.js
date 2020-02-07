import React from 'react';
import { connect } from 'react-redux';
import { sideReceived } from '../actions/actions'

const mapStateToProps = (state) =>
{

}

const mapDispatchToProps =
{
    sideReceived
}

class Name extends React.Component
{
    render()
    {
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
        .then(response => response.json())
        .then(data => {
            
            this.props.sideReceived(data, name);
        })
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);