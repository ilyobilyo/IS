import { useContext, useState } from 'react'
import styles from '../../products/Products.module.css'
import { CartContext } from '../../../contexts/CartContext'
import { useParams } from 'react-router-dom';

export const ProductCard = ({ product }) => {
    const {buyProduct} = useContext(CartContext);
    const {cartName} = useParams();
    const [isPurchased, setIsPurchased] = useState(false);

    const buyClickHandler = (e) => {
        e.preventDefault();

        buyProduct(cartName, product)
        setIsPurchased(true);
    }

    return (
        <article style={{backgroundColor: '#efa73f'}} className={styles.item}>
            <div className={styles.info}>
                <p>{product.name}</p>
                <p>{product.price} lv.</p>
                {product.isPurchased || isPurchased
                ? <button className={styles.btn} style={{backgroundColor: product.isPurchased || isPurchased ? 'green' : ''}} disabled={product.isPurchased}>Purchased</button>
                : <button className={styles.btn} onClick={buyClickHandler}>Purchase</button>}
            </div>
        </article>
    )
}