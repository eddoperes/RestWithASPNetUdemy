import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import {Link} from 'react-router-dom';
import {FiPower, FiEdit, FiTrash2} from 'react-icons/fi';

import "./styles.css";

import logoImage from '../../assets/logo.svg';

import api from '../../services/api';

export default function Book(){

    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(0);
    const userName = localStorage.getItem('userName');

    const navigate = useNavigate();

    const accessToken = localStorage.getItem('accessToken');

    useEffect(() => {
        fetchMoreBooks();
    }, [accessToken]);

    async function fetchMoreBooks()
    {
        const response = await api.get(`api/Book/v1/asc/4/${page}`,{
            headers : {
                Authorization : `Bearer ${accessToken}`
            }
        });
        setBooks(books.concat(response.data.list)); 
        setPage(page + 1);                            
    }

    async function logout()
    {
        try
        {
            const response = await api.get('api/Auth/v1/revoke', {
                headers : {
                    Authorization : `Bearer ${accessToken}`
                }
            });
            navigate('/');
        }
        catch (error)
        {
            alert(error)
        }
    }

    async function editBook(id)
    {
        try
        {            
            navigate(`new/${id}`);        
        }
        catch (error)
        {
            alert(error)
        }
    }

    async function deleteBook(id)
    {
        try
        {

            var resp = window.confirm('Are you sure?');

            if (resp == 1){

                const response = await api.delete(`api/Book/v1/${id}`, {
                    headers : {
                        Authorization : `Bearer ${accessToken}`
                    }
                });

                setBooks(books.filter(book => book.id != id));

            }

        }
        catch (error)
        {
            alert(error)
        }
    }

    return (
        <div className='book-container'>
            <header>
                <img src={logoImage}  alt="erudio"/>
                <span>Welcome <strong>{userName.toUpperCase()}</strong> </span>
                <Link className='button' to='new/0' >Add New Book</Link>
                <button type='button' onClick={()=>logout()}>
                    <FiPower size='18' color='#251FC5'/>
                </button>
            </header>

            <h1>Registered books</h1>
            <ul>

                {books.map(book => (

                    <li key={book.id}>
                        <strong>Title:</strong>
                        <p>{book.title}</p>
                        <strong>Author:</strong>
                        <p>{book.author}</p>
                        <strong>Prive:</strong>
                        <p>{Intl.NumberFormat('pt-BR',{style:'currency', currency : 'BRL'}).format(book.price)}</p>
                        <strong>Release date:</strong>
                        <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate))}</p>

                        <button type='button' onClick={()=> editBook(book.id)}>
                            <FiEdit size='20' color='#251FC5'/>
                        </button>

    
                        <button type='button' onClick={()=> {deleteBook(book.id)}}>
                            <FiTrash2 size='20' color='#251FC5'/>
                        </button>
                    </li>

                ))}

                

            </ul>

            <button className='button' type='button' onClick={()=> {fetchMoreBooks()}}> 
                Load More           
            </button>

        </div>
    );

}