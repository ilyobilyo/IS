import { Link } from "react-router-dom"
import styles from './Categories.module.css'

export const CategoryItem = ({ category }) => {
    return (
        <div className={styles.actions}>
            <Link to={`/products/${category.id}`}>{category.name}</Link>
            <Link className={styles.edit} to={`/categories/edit/${category.id}`}>Edit</Link>
        </div>
    )
}