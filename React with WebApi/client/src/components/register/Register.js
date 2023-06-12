import { useContext, useState } from 'react'
import styles from '../products/create-product/CreateProduct.module.css'
import btn from '../products/Products.module.css'

import * as authService from '../../services/authService'
import { useNavigate } from 'react-router-dom'
import { checkRequiredInputField, validatePassAndRepass, validatePassword } from '../../utils/validate'
import { AuthContext } from '../../contexts/AuthContext'

export const Register = () => {
    const navigate = useNavigate();
    const { onLogin } = useContext(AuthContext);
    const [formData, setFormData] = useState({
        username: '',
        password: '',
        confirmPassword: '',
        email: ''
    })
    const [errors, setErrors] = useState({
        username: '',
        password: '',
        confirmPassword: '',
        email: ''
    })

    const onChange = (e) => {
        setFormData(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onBlur = (e) => {
        if (e.target.name === 'email' || e.target.name === 'username') {
            setErrors(state => ({
                ...state,
                [e.target.name]: checkRequiredInputField(e.target.name, formData[e.target.name])
            }))
        } else if (e.target.name === 'password') {
            setErrors(state => ({
                ...state,
                password: validatePassword(formData.password)
            }))
        } else if (e.target.name === 'confirmPassword') {
            setErrors(state => ({
                ...state,
                confirmPassword: validatePassAndRepass(formData.password, formData.rePassword)
            }))
        }
    }

    const onSubmit = (e) => {
        e.preventDefault();

        authService.Register(formData)
            .then(data => {
                onLogin(data)
                navigate('/');
            })
            .catch((error) => {
                setErrors(state => ({
                    ...state,
                    serviceError: error.message
                }))
            });
    }

    return (
        <form action="post" className={styles.createForm} onSubmit={onSubmit}>
            <div className={styles.inputContainer}>
                <label htmlFor="Username">Username</label>
                <input type="text" name="username" id="Username" onChange={onChange} onBlur={onBlur} />
                <p style={{ color: 'red', width: '50%' }}>{errors.username}</p>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="Email">Email</label>
                <input type="email" name="email" id="Email" onChange={onChange} onBlur={onBlur} />
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="pass">Password</label>
                <input type="password" name="password" id="pass" onChange={onChange} onBlur={onBlur} />
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="rePass">Confirm Password</label>
                <input type="password" name="confirmPassword" id="rePass" onChange={onChange} onBlur={onBlur} />
            </div>

            <button className={btn.btn} disabled={errors.email || errors.password || errors.rePassword || errors.username}>Register</button>
        </form>
    )
}