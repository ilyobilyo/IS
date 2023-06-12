import { useContext } from 'react'
import styles from './Home.module.css'
import { CartContext } from '../../contexts/CartContext'
import { Link } from 'react-router-dom';

export const Home = () => {
    const {carts} = useContext(CartContext);

    return (
        <>
            <div className={styles.greeting}>
                <h1>Welcome in StoreIS</h1>
                <p>Create your own shoppin list</p>
            </div>
            <div className={styles.shopLists}>
                {carts ? carts.map(x => <Link to={`/cart/${x.name}`} key={x.name}>{x.name}</Link>) : <p>Create toyr first cart</p>}
            </div>
        </>
    )
}