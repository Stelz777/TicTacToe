import { createStore } from 'redux';
import reducer from '../reducers/rootReducer';

const store = createStore(reducer);

export default store;

