import React , {useState, useEffect} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import {Link} from 'react-router-dom';
import {FiArrowLeft} from 'react-icons/fi';

import "./styles.css";

import logoImage from '../../assets/logo.svg';

import api from '../../services/api';

export default function NewBook()
{

    const [id, setId] = useState(null);
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');   
    const {bookId} = useParams();
    const navigate = useNavigate();
    const accessToken = localStorage.getItem('accessToken');
    
    useEffect(() => {
        if (bookId == 0)
            return;
        else
            loadBook();   
    }, [bookId]);

    async function loadBook()
    {
        try
        {
            const response = await api.get(`api/Book/v1/${bookId}`,{
                headers : {
                    Authorization : `Bearer ${accessToken}`
                }
            });
            setId(response.data.id);
            setTitle(response.data.title);
            setAuthor(response.data.author);
            setPrice(response.data.price);
            let adjustedDate = response.data.launchDate.split('T')[0];
            setLaunchDate(adjustedDate);
        }
        catch (error)
        {
            alert(error);
            navigate('/books');
        }
    }

    async function saveOrUpdate(e)
    {
        e.preventDefault();
        const data = {title, author, launchDate, price};               
        try
        {
            if (bookId==0){
                const response = await api.post('api/Book/v1', data, {
                    headers : {
                        Authorization : `Bearer ${accessToken}`
                    }
                });
            }
            else
            {
                data.id = id;
                const response = await api.put('api/Book/v1', data, {
                    headers : {
                        Authorization : `Bearer ${accessToken}`
                    }
                });
            }
            navigate('/books');
        }
        catch (error)
        {
            alert(error)
        }
    }

    return(
        <div className='new-book-container'>
            <div className='content'>
                <section className='form'>
                    <img src={logoImage} alt='erudio' />
                    <h1>{bookId==0?'Add':'Update'} New Book</h1>
                    <p>Enter the book information and click on {bookId==0?'Add':'Update'}</p>
                    <Link className='back-link' to='/books' >
                        <FiArrowLeft sie={16} color='#251FC5'/>
                        Back to Books
                    </Link>
                </section>
                <form onSubmit={(e)=>saveOrUpdate(e)}>
                    <input placeholder='Title' 
                            value = {title}
                            onChange = {e=> setTitle(e.target.value)}                    
                    />
                    <input placeholder='Author' 
                            value = {author}
                            onChange = {e=> setAuthor(e.target.value)}
                    />
                    <input type='date' 
                            value = {launchDate}
                            onChange = {e=> setLaunchDate(e.target.value)}
                    />
                    <input placeholder='Price' 
                            value = {price}
                            onChange = {e=> setPrice(e.target.value)}
                    />
                    <button className='button' type='submit'>{bookId==0?'Add':'Update'}</button>
                </form>
            </div>
        </div>
    );
}