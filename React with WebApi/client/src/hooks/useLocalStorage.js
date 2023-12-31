import { useEffect, useState } from "react"

export const useLocalStorage = (key, defaultValue) => {

    const [data, setData] = useState(() => {
        const storedData = localStorage.getItem(key);

        return storedData ? JSON.parse(storedData) : defaultValue
    });

    useEffect(() => {
        localStorage.setItem(key, JSON.stringify(data));
    }, [key, data])

    const setLocalStorage = (newData) => {
        localStorage.setItem(key, JSON.stringify(newData));

        setData(newData)
    }

    return [
        data,
        setLocalStorage
    ]
}