import { createContext, useEffect, useState } from "react";
import * as categoryService from '../services/categoryService';

export const CategoryContext = createContext();

export const CategoryProvider = ({ children }) => {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        categoryService.GetAll()
        .then(data => {
            setCategories(data);
        });
    }, [])

    const addCategory = (category) => {
        setCategories(state => [...state, category])
    }

    const updateCategory = (category) => {
        setCategories(state => {
            const updatedCategories = state.map(cat => {
                if (cat.id === category.id) {
                    return category;
                }
                return cat;
            });
            return updatedCategories;
        });
    }

    return (
        <CategoryContext.Provider value={{ categories, addCategory, updateCategory }}>
            {children}
        </CategoryContext.Provider>
    )
}