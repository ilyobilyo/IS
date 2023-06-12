import styles from './Categories.module.css';
import btn from '../products/Products.module.css'
import { Link } from 'react-router-dom';
import { useContext } from 'react';
import { CategoryContext } from '../../contexts/CategoryContext';
import { CategoryItem } from './CategoryItem';
import { AuthContext } from '../../contexts/AuthContext';

export const Categories = () => {
    const { categories } = useContext(CategoryContext);
    const { isAuthenticated } = useContext(AuthContext);


    return (
        <div className={styles.wrapper}>
            {isAuthenticated &&
                <div className={styles.create}>
                    <Link className={btn.btn} to='/createCategory'>Create category</Link>
                </div>
            }
            <div className={styles.categories}>
                {categories && categories.map(x => <CategoryItem key={x.id} category={x} />)}
            </div>
        </div>
    )
}