const baseUrl = 'https://localhost:7137/Product';

export const GetAllProducts = async () => {
    const response = await fetch(baseUrl)
    const data = await response.json();

    if (data.status !== 200) {
        throw new Error(data.message)
    }

    return data.products;
}

export const GetAllByCategory = async (categoryId) => {
    const response = await fetch(baseUrl + `?categoryId=${categoryId}`)
    const data = await response.json();

    if (data.status !== 200) {
        throw new Error(data.message)
    }

    return data.products;
}

export const GetProductById = async (productId) => {
    const response = await fetch(baseUrl + `/${productId}`)
    const data = await response.json();

    if (data.status !== 200) {
        throw new Error(data.message)
    }

    return data.product;
}

export const CreateProduct = async (formData, token) => {
    const productFormData = new FormData();

    productFormData.append('name', formData.name);
    productFormData.append('price', formData.price);
    productFormData.append('file', formData.file);
    productFormData.append('categoryId', formData.categoryId);

    const response = await fetch(baseUrl, {
        method: 'post',
        headers: {
            'Authorization': `bearer ${token}`
        },
        body: productFormData
    })
    const data = await response.json();

    if (data.status !== 201) {
        throw new Error(data.message)
    }

    return data.product
}

export const EditProduct = async (product, token) => {
    const productFormData = new FormData();

    productFormData.append('id', product.id);
    productFormData.append('name', product.name);
    productFormData.append('price', product.price);

    if (product.file !== null) {
        productFormData.append('file', product.file);
    }

    productFormData.append('categoryId', product.categoryId);

    const response = await fetch(baseUrl + `/${product.id}`, {
        method: 'put',
        headers: {
            'Authorization': `bearer ${token}`
        },
        body: productFormData
    })
    const data = await response.json();

    if (data.status !== 200) {
        throw new Error(data.message)
    }

    return data.product
}