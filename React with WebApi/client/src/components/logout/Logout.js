import { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../contexts/AuthContext";

import * as authService from '../../services/authService';
import { CartContext } from "../../contexts/CartContext";

export const Logout = () => {
    const { onLogout, user } = useContext(AuthContext);
    const { clearCarts } = useContext(CartContext);

    const navigate = useNavigate();

    useEffect(() => {
        authService.Logout(user.token)
        .then(() => {
            onLogout();
            clearCarts();
            navigate('/');
        })
        .catch((error) => {
            alert(error.message);
        })
    }, [])

    return null;
}