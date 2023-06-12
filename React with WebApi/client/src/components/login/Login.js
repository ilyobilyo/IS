import { useContext, useState } from 'react';
import styles from '../products/create-product/CreateProduct.module.css'
import btn from '../products/Products.module.css'
import { AuthContext } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import { checkRequiredInputField, validatePassword } from '../../utils/validate';
import * as authService from '../../services/authService'

export const Login = () => {
    const { onLogin } = useContext(AuthContext);
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
    })

    const [errors, setErrors] = useState({
        email: '',
        password: '',
        serviceError: '',
    })

    const onChange = (e) => {
        setFormData(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onBlur = (e) => {
        if (e.target.name === 'email') {
            setErrors(state => ({
                ...state,
                [e.target.name]: checkRequiredInputField(e.target.name, formData[e.target.name])
            }))
        } else if (e.target.name === 'password') {
            setErrors(state => ({
                ...state,
                password: validatePassword(formData.password)
            }))
        }
    }

    const onSubmit = (e) => {
        e.preventDefault();
        authService.Login(formData)
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
    return(
        <form action="post" className={styles.createForm} onSubmit={onSubmit}>
            <div className={styles.inputContainer}>
                <label htmlFor="Email">Email</label>
                <input type="email" name="email" id="Email" onChange={onChange} onBlur={onBlur} />
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="pass">Password</label>
                <input type="password" name="password" id="pass" onChange={onChange} onBlur={onBlur} />
            </div>

            <button className={btn.btn} disabled={errors.email || errors.password }>Register</button>
        </form>
    )
}