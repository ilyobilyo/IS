import { useContext, useEffect, useState } from 'react';
import styles from '../../products/create-product/CreateProduct.module.css';
import btn from '../../products/Products.module.css'
import { AuthContext } from '../../../contexts/AuthContext';
import { CategoryContext } from '../../../contexts/CategoryContext';
import * as categoryService from '../../../services/categoryService'
import { useNavigate, useParams } from 'react-router-dom';
import { checkRequiredInputField } from '../../../utils/validate';


export const EditCategory = () => {
    const {user} = useContext(AuthContext);
    const {updateCategory} = useContext(CategoryContext);
    const {categoryId} = useParams();
    const [formData, setFormData] = useState({
        id: '',
        name: ''
    });
    const [errors, setErrors] = useState({
        name: '',
    })
    const navigate = useNavigate();

    useEffect(() => {
        categoryService.GetCategoryById(categoryId)
        .then(data => {
            setFormData(state => ({
                ...state,
                id: data.id,
                name: data.name
            }))
        })
    }, [])

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

        categoryService.Edit(formData, user.token)
            .then((data) => {
                updateCategory(data)
                navigate('/categories');
            })
            .catch((error) => {
                alert(error.message)
            })
    }
    return(
        <form action="get" className={styles.createForm} onSubmit={onSubmit}>
            <input type="hidden" name='id' value={formData.id}/>
            <div className={styles.inputContainer}>
                <label htmlFor="categoryName">Category Name</label>
                <input type="text" name="name" id="categoryName" defaultValue={formData.name} onChange={onChange} onBlur={onBlur}/>
            </div>
            {errors.name && <p style={{color: 'red'}}>{errors.name}</p>}

            <button className={btn.btn}>Edit</button>
        </form>
    )
}