import React from 'react';
import { connect } from 'react-redux';
import { userRegister } from '../actions/userActions';

const mapStateToProps = (state) =>
{
    return {
        
    }
}

const mapDispatchToProps = 
{
    userRegister
}

class Registration extends React.Component
{
    constructor(props)
    {
        super(props);
        this.state = {
            username: '',
            password: '',
            repeatedPassword: '',
            firstName: '',
            lastName: '',
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
        const { username, password, repeatedPassword, firstName, lastName } = this.state;
        if (username && password && repeatedPassword && firstName && lastName && (password === repeatedPassword))
        {
            console.log("handleSubmit true!");
            this.props.userRegister(username, password, firstName, lastName);
        }
    }

    render()
    {
        console.log("Registration render!");
        const { username, password, repeatedPassword, firstName, lastName, submitted } = this.state;
        return (
            <div>
                <div className = "col-lg-6 col-lg-offset-3">
                    <h2>Registration</h2>
                    <form name = "form" onSubmit = { this.handleSubmit }>
                        <div className = { 'form-group' + (submitted && !username ? ' has-error' : '') }>
                            <label htmlFor = "username">Username</label>
                            <input
                                type = "text"
                                className = "form-control"
                                name = "username"
                                value = { username }
                                onChange = { this.handleChange }
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
                        <div className = { 'form-group' + (submitted && !repeatedPassword ? ' has-error' : '') }>
                            <label htmlFor = "repeatedPassword">Repeat Password</label>
                            <input type = "password" className = "form-control" name = "repeatedPassword" value = { repeatedPassword } onChange = { this.handleChange } />
                            { submitted && !repeatedPassword &&
                                <div className = "help-block">Repeat password</div>
                            }
                        </div>
                        <div className = { 'form-group' + (submitted && !firstName ? ' has-error' : '') }>
                            <label htmlFor = "firstName">First Name</label>
                            <input
                                type = "text"
                                className = "form-control"
                                name = "firstName"
                                value = { firstName }
                                onChange = { this.handleChange }
                            />
                            { submitted && !firstName &&
                                <div className = "help-block">Firstname is required</div>
                            }
                        </div>
                        <div className = { 'form-group' + (submitted && !lastName ? ' has-error' : '') }>
                            <label htmlFor = "lastName">Last Name</label>
                            <input
                                type = "text"
                                className = "form-control"
                                name = "lastName"
                                value = { lastName }
                                onChange = { this.handleChange }
                            />
                            { submitted && !lastName &&
                                <div className = "help-block">Lastname is required</div>
                            }
                        </div>
                        <div className = "form-group">
                            <button className = "btn btn-primary">Done</button>
                        </div>
                    </form>
                </div>
            </div>
        );
    }

}

export default connect(mapStateToProps, mapDispatchToProps)(Registration);