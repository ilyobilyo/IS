import { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';


export const PrivateGuard = ({children}) => {
    const { isAuthenticated, user } = useContext(AuthContext);
   
    if (!isAuthenticated || user.isDeleted) {
        return <Navigate to="/login" replace />
    }

    return children ? children : <Outlet />  
};