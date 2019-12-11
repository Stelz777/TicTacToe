import React from 'react';

class Toggle extends React.Component
{
    constructor(props)
    {
        super(props);
        this.state = { isToggleOn: true };
        this.handleClick = this.handleClick.bind(this);
    }
}