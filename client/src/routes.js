import React from 'react';
import {BrowserRouter, Route, Routes} from 'react-router-dom';

import Login from './pages/login';
import Book from './pages/book';
import NewBook from './pages/newBook';

export default function AppRoutes(){
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' exact element={<Login/>} />
                <Route path='/books' element={<Book/>} />
                <Route path='/books/new' element={<NewBook/>} />
            </Routes>
        </BrowserRouter>


    );
}