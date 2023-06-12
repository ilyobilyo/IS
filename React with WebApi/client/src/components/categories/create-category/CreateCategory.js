import { useContext, useState } from 'react';
import styles from '../../products/create-product/CreateProduct.module.css';
import btn from '../../products/Products.module.css'
import { checkRequiredInputField } from '../../../utils/validate';
import * as categoryService from '../../../services/categoryService'
import { AuthContext } from '../../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import { CategoryContext } from '../../../contexts/CategoryContext';

export const CreateCategory = () => {
    const {user} = useContext(AuthContext);
    const {addCategory} = useContext(CategoryContext);

    const [formData, setFormData] = useState({
        name: ''
    });
    const [errors, setErrors] = useState({
        name: '',
    })
    const navigate = useNavigate();

    const onChange = (e) => {
        setFormData(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onBlur = (e) => {
        setErrors(state => ({
            ...state,
            [e.target.name]: checkRequiredInputField(e.target.name, formData[e.target.name])
        }))
    }

    const onSubmit = (e) => {
        e.preventDefault();

        categoryService.Create(formData, user.token)
            .then((data) => {
                addCategory(data)
                navigate('/categories');
            })
            .catch((error) => {
                alert(error.message)
            })
    }

    return (
        <form action="get" className={styles.createForm} onSubmit={onSubmit}>
            <div className={styles.inputContainer}>
                <label htmlFor="categoryName">Category Name</label>
                <input type="text" name="name" id="categoryName" onChange={onChange} onBlur={onBlur}/>
            </div>
            {errors.name && <p style={{color: 'red'}}>{errors.name}</p>}
            <button className={btn.btn}>Create</button>
        </form>
    )
}