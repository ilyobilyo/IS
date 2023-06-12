import { useNavigate, useParams } from 'react-router-dom';
import styles from '../create-product/CreateProduct.module.css';
import btn from '../Products.module.css'
import { useContext, useEffect, useState } from 'react';
import * as productService from '../../../services/productService'
import { CategoryContext } from '../../../contexts/CategoryContext';
import { AuthContext } from '../../../contexts/AuthContext';

export const EditProduct = () => {
    const {productId} = useParams();
    const {categories} = useContext(CategoryContext);
    const {user} = useContext(AuthContext);
    const navigate = useNavigate();

    const [product, setProduct] = useState({});
    const [formData, setFormData] = useState({
        id: '',
        name: '',
        price: '',
        file: null,
        photo: '',
        categoryId: ''
    });

    useEffect(() => {
        productService.GetProductById(productId)
        .then(data => {
            setProduct(data);
            setFormData(state => ({
                id: data.id,
                name: data.name,
                price: data.price,
                photo: data.photo,
                categoryId: data.categoryId
            }))
        })
        .catch((error) => {
            alert(error.message)
        })
    }, [])

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

        productService.EditProduct(formData, user.token)
            .then(() => {
                navigate('/products');
            })
            .catch((error) => {
                alert(error.message)
            })
    }

    return (
        <form action="get" className={styles.createForm} encType="multipart/form-data" onSubmit={onSubmit}>
            <input type="hidden" name='id' value={product.id}/>
            <div className={styles.inputContainer}>
                <label htmlFor="productName">Product Name</label>
                <input type="text" name="name" id="productName" defaultValue={product.name} onChange={onChange}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="productPrice">Product Name</label>
                <input type="number" step={0.01} name="price" id="productPrice" defaultValue={product.price} onChange={onChange}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="img">Product Photo</label>
                <input type="file" name="photo" id="img" onChange={onChangeFile}/>
            </div>

            <div className={styles.inputContainer}>
                <label htmlFor="img">Product Category</label>
                <select name="categoryId" id="cat" defaultValue={product.categoryId} onChange={onChange}>
                    <option></option>
                    {categories && categories.map(x => <option value={x.id} key={x.id}>{x.name}</option>)}
                </select>
            </div>


            <button className={btn.btn}>Edit</button>
        </form>
    )
}