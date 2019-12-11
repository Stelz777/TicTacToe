import { combineReducers, createStore } from 'redux'
import { addTodo, toggleTodo, setVisibilityFilter, VisibilityFIlters } from './actions'
import todoApp from './reducers'
import { connect } from 'react-redux'
const reducer = combineReducers({ visibilityFilter, todos });
const store = createStore(todoApp, window.STATE_FROM_SERVER);
const ADD_TODO = 'ADD_TODO';
const boundAddTodo = text => dispatch(addTodo(text));
const boundCompleteTodo = index => dispatch(completeTodo(index));
const initialState = 
{
    visibilityFilter: VisibilityFIlters.SHOW_ALL,
    todos: []
}
const { SHOW_ALL } = VisibilityFilters;
const todoApp = combineReducers(
    {
        visibilityFilter,
        todos
    }
);
export default todoApp

const reducer = combineReducers(
    {
        a: doSomethingWithA,
        b: processB,
        c: c
    }
);

const unsubscribe = store.subscribe(() => console.log(store.getState()));

const getVisibleTodos = (todos, filter) =>
{
    switch (filter)
    {
        case 'SHOW_COMPLETED':
            return todos.filter(t => t.completed);
        case 'SHOW_ACTIVE':
            return todos.filter(t => !t.completed);
        case 'SHOW_ALL':
        default:
            return todos;
    }
}

const mapStateToProps = state =>
{
    return
    {
        todos: getVisibleTodos(state.todos, state.visibilityFilter)
    };
}

const mapDispatchToProps = dispatch =>
{
    return
    {
        onTodoClick: id =>
        {
            dispatch(toggleTodo(id))
        }
    };
}

const VisibleTodoList = connect(
    mapStateToProps,
    mapDispatchToProps
)(TodoList);

export default VisibleTodoList

let previousState = 
{
    visibleTodoFilter: 'SHOW_ALL',
    todos:
    [
        {
            text: 'Read the docs.',
            complete: false
        }
    ]
};

let action = 
{
    type: 'ADD_TODO',
    text: 'Understand the flow.'
};

let nextState = todoApp(previousState, action);
let todoApp = combineReducers(
    {
        todos,
        visibleTodoFilter
    }
);
let nextTodos = todos(state.todos, action);
let nextVisibleTodoFilter = visibleTodoFilter(state.visibleTodoFilter, action);

function addTodoWithDispatch(text)
{
    const action = 
    {
        type: ADD_TODO,
        text
    };
    dispatch(action);
}

function addTodo(text)
{
    return
    {
        type: ADD_TODO,
        text
    };
}

function toObservable(store)
{
    return
    {
        subscribe({ next })
        {
            const unsubscribe = store.subscribe(() => next(store.getState()));
            next(store.getState());
            return { unsubscribe };
        }
    }
}

function todoApp(state = {}, action)
{
    
    
        return
        {
            visibilityFilter: visibilityFilter(state.visibilityFilter, action),
            todos: todos(state.todos, action)
        };
    
}

function visibilityFilter(state = SHOW_ALL, action)
{
    switch (action.type)
    {
        case SET_VISIBILITY_FILTER:
            return action.filter;
        default:
            return state;
    }
}

function todos(state = [], action)
{
    switch (action.type)
    {
        case 'ADD_TODO':
            return 
            [
                ...state,
                {
                    text: action.text,
                    completed: false
                }
            ];
        case 'TOGGLE_TODO':
            return state.map((todo, index) =>
            {
                if (index === action.index)
                {
                    return Object.assign({}, todo,
                        {
                            completed: !todo.completed
                        });
                }
                return todo;
            });
            
        
        default:
            return state;
    }
}

function counter(state = 0, action)
{
    switch (action.type)
    {
        case 'INCREMENT':
            return state + 1;
        case 'DECREMENT':
            return state - 1;
        default:
            return state;
    }
}

let store = createStore(counter);
store.subscribe(() => console.log(store.getState()));
store.dispatch({ type: 'INCREMENT' });
store.dispatch({ type: 'INCREMENT' });
store.dispatch({ type: 'DECREMENT' });
console.log(store.getState());
store.dispatch(
    {
        type: 'COMPLETE_TODO',
        index: 1
    }
);
store.dispatch(
    {
        type: 'SET_VISIBILITY_FILTER',
        filter: 'SHOW_COMPLETED'
    }
);
dispatch(addTodo(text));
dispatch(completeTodo(index));
boundAddTodo(text);
boundCompleteTodo(index);
store.dispatch(addTodo('Learn about actions'));
store.dispatch(addTodo('Learn about reducers'));
store.dispatch(addTodo('Learn about store'));
store.dispatch(toggleTodo(0));
store.dispatch(toggleTodo(1));
store.dispatch(setVisibilityFilter(VisibilityFIlters.SHOW_COMPLETED));
unsubscribe();

