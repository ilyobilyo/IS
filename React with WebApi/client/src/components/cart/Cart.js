import { Link, useNavigate, useParams } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { CartContext } from "../../contexts/CartContext";
import styles from '../products/Products.module.css'
import { ProductCard } from "./ProductCard/ProductCard";

export const Cart = () => {
    const { carts, deleteCart } = useContext(CartContext);
    const { cartName } = useParams();
    const navigate = useNavigate();
    const [cart, setCart] = useState({});

    useEffect(() => {
        setCart(carts.find(x => x.name === cartName));
    }, [])

    const deleteHandler = (e) => {
        e.preventDefault();

        const confirmation = window.confirm('Are you sure to delete this cart');

        if (confirmation) {
            deleteCart(cartName);
            navigate('/');
        }
    }

    return (
        <>
            <div className={styles.actionWrapper}>
                <h1 style={{ textAlign: 'center' }}>Cart name: {cart.name}</h1>
                <button className={styles.delete} onClick={deleteHandler}>Delete cart</button>
            </div>

            <div className={styles.wrapper}>
                {cart.products && cart.products.map(x => <ProductCard key={x.name} product={x} />)}
            </div>
        </>
    )
}