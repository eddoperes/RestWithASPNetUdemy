import React, {useState} from 'react';

export default function Header()
{
    const [counter, setCounter] = useState(0);

    function increment(){
        setCounter(counter + 1);
    }

    return (
        <div>
            <header>
                Counter: {counter}
            </header>
            <button onClick={increment}>Add</button>
        </div>        
    );

}