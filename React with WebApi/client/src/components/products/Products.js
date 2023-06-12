import { Link, useParams } from 'react-router-dom'
import styles from './Products.module.css'
import { useContext, useEffect, useState } from 'react'
import * as productService from '../../services/productService'
import { ProductCard } from './product-card/ProductCard'
import { CategoryContext } from '../../contexts/CategoryContext'
import { AuthContext } from '../../contexts/AuthContext'

export const Products = () => {
    const { categoryId } = useParams();
    const [products, setProducts] = useState([]);
    const [searchTerm, setSearchTerm] = useState({
        categoryId: ''
    })
    const { categories } = useContext(CategoryContext)
    const { isAuthenticated } = useContext(AuthContext)

    useEffect(() => {
        if (categoryId) {
            productService.GetAllByCategory(categoryId)
                .then(data => {
                    setProducts(state => data)
                })
                .catch((error) => {
                    alert(error.message)
                });
        } else {
            productService.GetAllProducts()
                .then(data => {
                    setProducts(state => data);
                })
                .catch((error) => {
                    alert(error.message)
                })
        }
    }, [categoryId])

    const onChange = (e) => {
        setSearchTerm(state => ({
            ...state,
            [e.target.name]: e.target.value
        }))
    }

    const onSubmit = (e) => {
        e.preventDefault();
        productService.GetAllByCategory(searchTerm.categoryId)
            .then(data => {
                setProducts(state => data)
            })
            .catch((error) => {
                alert(error.message)
            });
    }

    return (
        <>
            <div className={styles.container}>
                <form action="get" className={styles.searchForm} onSubmit={onSubmit}>
                    <select name="categoryId" id="cat" onChange={onChange}>
                        <option value=""></option>
                        {categories && categories.map(x => <option key={x.id} value={x.id}>{x.name}</option>)}
                    </select>

                    <button className={styles.btn}>Search</button>
                </form>

                {isAuthenticated &&
                    <Link className={styles.btn} to="/createProduct">Create Product</Link>
                }
            </div>
            <div className={styles.wrapper}>
                {products && products.map(x => <ProductCard key={x.id} product={x} />)}
            </div>
        </>

    )
}