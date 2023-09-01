import React from 'react'
import Header from '../../components/header/Header'
import Button from 'react-bootstrap/Button'
import Footer from '../../components/footer/Footer'
import LoginForm from '../../components/forms/LoginForm'

function Login() {
    return (
        <>
            <Header/>
            <div className='d-flex flex-column justify-content-between flex-grow-1'>
                <LoginForm/>
                <Footer />
            </div>
            
        </>
    )
}

export default Login
