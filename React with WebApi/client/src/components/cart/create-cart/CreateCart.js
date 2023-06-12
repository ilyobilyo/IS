import { useContext, useState } from 'react';
import styles from '../../products/create-product/CreateProduct.module.css';
import btn from '../../products/Products.module.css'
import { useNavigate } from 'react-router-dom';
import { CartContext } from '../../../contexts/CartContext';

export const CreateCart = () => {
    const navigate = useNavigate();
    const {createCart} = useContext(CartContext);
    const[formData, setFormData] = useState({
        name: ''
    })

    const onChange = (e) => {
        setFormData(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onSubmit = (e) => {
        e.preventDefault();

        const newCart = {
            name: formData.name,
            products: []
        }

        createCart(newCart);
        navigate('/');
    }

    return(
        <form action="get" className={styles.createForm} onSubmit={onSubmit}>
            <div className={styles.inputContainer}>
                <label htmlFor="cartName">Cart Name</label>
                <input type="text" name="name" id="cartName" onChange={onChange}/>
            </div>

            <button className={btn.btn}>Create cart</button>
        </form>
    )
}