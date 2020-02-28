import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import utils from '../utility/utils';
import { userLogin, userLogout, userNameSetInLobby, userRegisterStarted } from '../actions/userActions';

const mapStateToProps = (state) =>
{
    const { loggingIn } = state.authenticationReducer;
    return {
        isDisabledNameInput: state.commonReducer.isDisabledNameInput,
        lobbyPlayerName: state.commonReducer.lobbyPlayerName,
        loggingIn,
        ticPlayerName: state.commonReducer.ticPlayerName,
        tacPlayerName: state.commonReducer.tacPlayerName
    }
}

const mapDispatchToProps = 
{
    userLogin,
    userLogout,
    userNameSetInLobby,
    userRegisterStarted
}

class Login extends React.Component
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
        this.register = this.register.bind(this);
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
        this.props.userNameSetInLobby(name);
    }

    render()
    {
        const { loggingIn } = this.props;
        const { username, password, submitted } = this.state;
        return (
            <div>
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
                                type = "text"
                                className = "form-control"
                                name = "username"
                                value = { username }
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
                        <div class = "btn-group">
                            <div className = "form-group">
                                <button className = "btn btn-primary">Login</button>
                                { loggingIn &&
                                    <img src = "data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                                }
                            </div>
                            <div className = "form-group">
                                <button className = "btn btn-secondary" type = "button" onClick = { this.register }>Register</button>
                                { loggingIn &&
                                    <img src = "data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                                }
                            </div>
                        </div>
                    </form>    
                </div>
            </div>
        );
    }

    register()
    {
        this.props.userRegisterStarted();
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

export default connect(mapStateToProps, mapDispatchToProps)(Login);