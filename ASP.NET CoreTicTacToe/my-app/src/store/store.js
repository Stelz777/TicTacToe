import { applyMiddleware, createStore } from 'redux';
import { createLogger } from 'redux-logger'
import thunkMiddleware from 'redux-thunk';
import rootReducer from '../reducers/rootReducer'

const loggerMiddleware = createLogger();

const store = createStore(rootReducer, applyMiddleware(thunkMiddleware, loggerMiddleware));

export default store;

