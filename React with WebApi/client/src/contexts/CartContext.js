import { createContext } from "react";
import { useLocalStorage } from '../hooks/useLocalStorage'

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [carts, setCarts] = useLocalStorage('carts', []);

    const createCart = (cart) => {
        setCarts((state) => (carts ? [...carts, cart] : cart));
    }

    const addProduct = (cartName, product) => {
        setCarts((prevCarts) => {
            const updatedCarts = prevCarts.map((cart) => {
                if (cart.name === cartName) {
                    if (cart.products.some(x => x.id === product.id)) {
                        alert(`${product.name} is already in this cart`)
                    } else{
                        return {
                            ...cart,
                            products: [...cart.products, product]
                        };
                    }
                }

                return cart;
            });

            return updatedCarts;
        });
    };

    const buyProduct = (cartName, product) => {
        setCarts((prevCarts) => {
            const updatedCarts = prevCarts.map((cart) => {
                if (cart.name === cartName) {
                    const updatedProducts = cart.products.map((p) => {
                        if (p.id === product.id) {
                            return {
                                ...p,
                                isPurchased: true
                            };
                        }
                        return p;
                    });

                    return {
                        ...cart,
                        products: updatedProducts
                    };
                }

                return cart;
            });

            return updatedCarts;
        });
    }

    const clearCarts = () => {
        setCarts([]);
    }

    return (
        <CartContext.Provider value={{ carts, createCart, addProduct, buyProduct, clearCarts }}>
            {children}
        </CartContext.Provider>
    )
}