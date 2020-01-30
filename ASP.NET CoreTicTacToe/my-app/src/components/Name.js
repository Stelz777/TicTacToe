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
                onBlur = { (event) => 
                    {
                        this.textChanged(event.target.value);
                        event.target.disabled = true;
                    } 
                }
            />
        );
    }

    textChanged(name)
    {
        console.log("textchanged name: ", name);
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
            console.log("textchanged data: ", data);
            this.props.sideReceived(data);
        })
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Name);