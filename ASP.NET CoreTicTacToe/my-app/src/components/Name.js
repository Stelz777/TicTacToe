import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { nameSetInLobby } from '../actions/nameActions'
import utils from '../utility/utils';
import { userLogin, userLogout } from '../actions/userActions';

const mapStateToProps = (state) =>
{
    const { loggingIn } = state.authenticationReducer;
    return {
        isDisabledNameInput: state.commonReducer.isDisabledNameInput,
        lobbyPlayerName: state.nameReducer.lobbyPlayerName,
        loggingIn,
        ticPlayerName: state.commonReducer.ticPlayerName,
        tacPlayerName: state.commonReducer.tacPlayerName
    }
}

const mapDispatchToProps = 
{
    nameSetInLobby,
    userLogin,
    userLogout
}

class Name extends React.Component
{
    constructor(props)
    {
        super(props);
        this.props.userLogout();
        this.state = {
            username: '',
            password: '',
            submitted: false
        };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event)
    {
        const { name, value } = event.target;
        this.setState({ [name]: value });
    }

    handleSubmit(event)
    {
        event.preventDefault();
        this.setState({ submitted: true });
        const { username, password } = this.state;
        if (username && password)
        {
            this.props.userLogin(username, password);
        }
    }

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