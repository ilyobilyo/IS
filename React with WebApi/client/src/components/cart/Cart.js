import { useParams } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { CartContext } from "../../contexts/CartContext";
import styles from '../products/Products.module.css'
import { ProductCard } from "./ProductCard/ProductCard";

export const Cart = () => {
    const { carts } = useContext(CartContext);
    const { cartName } = useParams();
    const [cart, setCart] = useState({});

    useEffect(() => {
        setCart(carts.find(x => x.name === cartName));
    }, [])
    
    return (
        <>
            <h1 style={{textAlign: 'center'}}>{cart.name}</h1>
            <div className={styles.wrapper}>
                {cart.products && cart.products.map(x => <ProductCard key={x.name} product={x} />)}
            </div>
        </>
    )
}