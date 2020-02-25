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
            console.log("handleSubmit true!");
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
        const { loggingIn } = this.props;
        const { username, password, submitted } = this.state;
        return (
            <div>
                <link
                    rel="stylesheet"
                    href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
                    integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
                    crossorigin="anonymous"
                />
                <div className = "col-lg-6 col-lg-offset-3">
                    <div className="alert alert-info">
                        Username: test<br/>
                        Password: test
                    </div>
                    <h2>Login</h2>
                    <form name = "form" onSubmit = { this.handleSubmit }>
                        <div className = { 'form-group' + (submitted && !username ? ' has-error' : '') }>
                            <label htmlFor = "username">Username</label>
                            <input
                                //disabled = { this.props.isDisabledNameInput }
                                type = "text"
                                className = "form-control"
                                name = "username"
                                value = { username }
                                onBlur = { 
                                    (event) =>
                                    {
                                        this.nameTextBoxEventBody(event)
                                    }
                                }
                                onChange = { this.handleChange }
                                onKeyDown = {
                                    this.checkEventEnterKey()
                                }
                            />
                            { submitted && !username &&
                                <div className = "help-block">Username is required</div>
                            }
                        </div>
                        <div className = { 'form-group' + (submitted && !password ? ' has-error' : '') }>
                            <label htmlFor = "password">Password</label>
                            <input type = "password" className = "form-control" name = "password" value = { password } onChange = { this.handleChange } />
                            { submitted && !password &&
                                <div className = "help-block">Password is required</div>
                            }
                        </div>
                        <div className = "form-group">
                            <button className = "btn btn-primary">Login</button>
                            { loggingIn &&
                                <img src = "data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                            }
                        </div>
                    </form>    
                </div>
            </div>
        );
    }

    nameTextBoxEventBody(event)
    {
        let name = event.target.value;
        
        if (name !== '')
        {
            this.textChanged(name);
            //event.target.disabled = true;
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