import React from 'react';

function HighlightedSquare(props)
{
    return (
        <button
            className="highlightedSquare"
            onClick= { props.onClick }
        >
            {
                props.value
            }
        </button>
    );
}

export default HighlightedSquare;