import { Route, Routes } from 'react-router-dom';
import './App.css';
import { Header } from './components/header/Header';
import { Home } from './components/home/Home';
import { Products } from './components/products/Products';
import { CreateProduct } from './components/products/create-product/CreateProduct';
import { Categories } from './components/categories/Categories';
import { CreateCategory } from './components/categories/create-category/CreateCategory';
import { CategoryProvider } from './contexts/CategoryContext';
import { Register } from './components/register/Register';
import { AuthProvider } from './contexts/AuthContext';
import { Logout } from './components/logout/Logout';
import { Login } from './components/login/Login';
import { CreateCart } from './components/cart/create-cart/CreateCart';
import { CartProvider } from './contexts/CartContext';
import { Cart } from './components/cart/Cart';
import { EditProduct } from './components/products/edit-product/EditProduct';
import { EditCategory } from './components/categories/edit-category/EditCategory';

function App() {
  return (
    <div className="App">
      <AuthProvider>
        <CategoryProvider>
          <CartProvider>
            <Header />

            <Routes>
              <Route path='/' element={<Home />} />
              <Route path='/register' element={<Register />} />
              <Route path='/login' element={<Login />} />
              <Route path='/logout' element={<Logout />} />

              <Route path='/products' element={<Products />} />
              <Route path='/products/:categoryId' element={<Products />} />
              <Route path='/createProduct' element={<CreateProduct />} />
              <Route path='/products/edit/:productId' element={<EditProduct />} />

              <Route path='/categories' element={<Categories />} />
              <Route path='/createCategory' element={<CreateCategory />} />
              <Route path='/categories/edit/:categoryId' element={<EditCategory />} />

              <Route path='/createCart' element={<CreateCart />} />
              <Route path='/cart/:cartName' element={<Cart />} />
            </Routes>
          </CartProvider>
        </CategoryProvider>
      </AuthProvider>
    </div>
  );
}

export default App;
