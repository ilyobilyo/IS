import { createContext, useEffect, useState } from "react";
import { useLocalStorage } from '../hooks/useLocalStorage'

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useLocalStorage('auth', {});
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        if (user.token) {
            setIsAuthenticated(true);
        }
    })

    const onLogin = (data) => {
        const user = {
            user: data.user,
            token: data.token
        }
        setUser(user);
        setIsAuthenticated(true);
    }

    const onLogout = () => {
        setUser({})
        setIsAuthenticated(false);
    }

    return (
        <AuthContext.Provider value={{ user, onLogin, onLogout, isAuthenticated, setUser}}>
            {children}
        </AuthContext.Provider>
    )
}