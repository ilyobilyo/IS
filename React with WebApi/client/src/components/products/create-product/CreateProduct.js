import styles from './CreateProduct.module.css';
import btn from '../Products.module.css'
import { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import * as productService from '../../../services/productService'
import { AuthContext } from '../../../contexts/AuthContext';
import { CategoryContext } from '../../../contexts/CategoryContext';

export const CreateProduct = () => {
    const {user} = useContext(AuthContext);
    const {categories} = useContext(CategoryContext);

    const [formData, setFormData] = useState({
        name: '',
        price: '',
        file: null,
        categoryId: ''
    });

    const navigate = useNavigate();

    const onChange = (e) => {
        setFormData(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onChangeFile = (e) => {
        setFormData(state => ({
            ...state,
            file: e.target.files[0]
        }))
    }

    const onSubmit = (e) => {
        e.preventDefault();

        productService.CreateProduct(formData, user.token)
            .then(() => {
                navigate('/products');
            })
            .catch((error) => {
                alert(error.message)
            })
    }

    return (
        <form action="get" className={styles.createForm} encType="multipart/form-data" onSubmit={onSubmit}>
            <div className={styles.inputContainer}>
                <label htmlFor="productName">Product Name</label>
                <input type="text" name="name" id="productName"  onChange={onChange}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="productPrice">Product Name</label>
                <input type="number" step={0.01} name="price" id="productPrice"  onChange={onChange}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="img">Product Photo</label>
                <input type="file" name="file" id="img"  onChange={onChangeFile}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="img">Product Category</label>
                <select name="categoryId" id="cat"  onChange={onChange}>
                    <option value=""></option>
                    {categories && categories.map(x => <option key={x.id} value={x.id}>{x.name}</option>)}
                </select>
            </div>

            <button className={btn.btn}>Create</button>
        </form>
    )
}