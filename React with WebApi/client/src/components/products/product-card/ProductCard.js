import { Link } from 'react-router-dom'
import styles from '../Products.module.css'
import { useContext, useState } from 'react';
import { CartContext } from '../../../contexts/CartContext';

export const ProductCard = ({ product }) => {
    const [loadCarts, setLoadCarts] = useState(false);
    const [cart, setCart] = useState('');
    const { carts, addProduct } = useContext(CartContext);

    const chooseClickHandler = () => {
        setLoadCarts(true);
    }

    const onChangeCart = (e) => {
        setCart(e.target.value);
    }

    const addProductsClickHandler = (e) => {
        e.preventDefault();

        const productToAdd = {
            id: product.id,
            name: product.name,
            price: product.price,
            isPurchased: false
        }

        addProduct(cart, productToAdd);
        setLoadCarts(false);
    }

    return (
        <article className={styles.item}>
            <div className={styles.imageContainer}>
                <img src={`data:image;base64,${product.photo}`} alt="snimka na product" />
            </div>
            <div className={styles.info}>
                <p>{product.name}</p>
                <p>{product.price} lv.</p>
                <Link className={styles.btn} to={`/products/edit/${product.id}`}>Edit</Link>
                {loadCarts ?
                    <>
                        <select onChange={onChangeCart}>
                            <option></option>
                            {carts && carts.map(x => <option key={x.name} value={x.name}>{x.name}</option>)}
                        </select>
                        <button className={styles.btn} onClick={addProductsClickHandler}>Add to Cart</button>
                    </>
                    : <button className={styles.btn} onClick={chooseClickHandler}>Choose Cart</button>
                }
            </div>
        </article>
    )
}