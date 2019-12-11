import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux"
import { createStore, applyMiddleware } from "redux"
import thunk from "redux-thunk"

import Game from "./components/Game";
import reducers from "./reducers"

const store = createStore(reducers, applyMiddleware(thunk));

ReactDOM.render(
    <Provider store={store}>
        <Game />
    </Provider>,
    document.querySelector("#root")
)