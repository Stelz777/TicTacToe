import { applyMiddleware, createStore } from 'redux';
import rootReducer from '../reducers/rootReducer';
import logger from 'redux-logger'


const store = createStore(rootReducer, applyMiddleware(logger));

export default store;

