import React from 'react';
import Switch from "react-switch";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as Actions from '../actions';
import Game from '../components/Game';
import '../styles/app.css';

class App extends React.Component
{
    render() {
        return (
            <div>
                <Game />
            </div>
        )
    }
}

export default App;





