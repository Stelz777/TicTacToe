import React from 'react'

return React.createElement(
    "div", 
    {
        className: "shopping-list"
    },
    React.createElement("h1", null, "Shopping list for ", props.name),
    React.createElement("ul", null, 
        React.createElement("li", null, "Instagram"),
        React.createElement("li", null, "WhatsApp"),
        React.createElement("li", null, "Oculus")
    )
);