import { Link } from 'react-router-dom'
import styles from './Header.module.css'
import { useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';

export const Header = () => {
    const { isAuthenticated } = useContext(AuthContext);

    return (
        <header>
            <nav>
                <ul className={styles.navList}>
                    <li className={styles.navListItem}>
                        <Link to="/">Home</Link>
                    </li>
                    <li className={styles.navListItem}>
                        <Link to="/products">Products</Link>
                    </li>
                    <li className={styles.navListItem}>
                        <Link to="/categories">Categories</Link>
                    </li>
                    {isAuthenticated
                        ? <>
                            <li className={styles.navListItem}>
                                <Link to="/createCart">Create cart</Link>
                            </li>
                            <li className={styles.navListItem}>
                                <Link to="/logout">Logout</Link>
                            </li>
                        </>
                        : <>
                            <li className={styles.navListItem}>
                                <Link to="/login">Login</Link>
                            </li>
                            <li className={styles.navListItem}>
                                <Link to="/register">Register</Link>
                            </li>
                        </>
                    }
                </ul>
            </nav>
        </header>
    )
}