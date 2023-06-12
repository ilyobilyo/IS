import styles from './Categories.module.css';
import btn from '../products/Products.module.css'
import { Link } from 'react-router-dom';
import { useContext } from 'react';
import { CategoryContext } from '../../contexts/CategoryContext';
import { CategoryItem } from './CategoryItem';

export const Categories = () => {
    const {categories} = useContext(CategoryContext);

    return (
        <div className={styles.wrapper}>
            <div className={styles.create}>
                <Link className={btn.btn} to='/createCategory'>Create category</Link>
            </div>
            <div className={styles.categories}>
                {categories && categories.map(x => <CategoryItem key={x.id} category={x}/>)}
            </div>
        </div>
    )
}