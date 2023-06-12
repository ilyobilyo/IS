const baseUrl = 'https://localhost:7137/Category';

export const GetAll = async () => {
    const response = await fetch(baseUrl);
    const data = await response.json();

    return data.categories;
}

export const GetCategoryById = async (id) => {
    const response = await fetch(baseUrl + `/${id}`);
    const data = await response.json();

    return data.category;
}

export const Create = async (formData, token) => {
    const response = await fetch(baseUrl,{
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${token}`
        },
        body: JSON.stringify(formData)
    })

    const data = await response.json();

    return data.category;
}

export const Edit = async (formData, token) => {
    const response = await fetch(baseUrl + `/${formData.id}`,{
        method: 'put',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `bearer ${token}`
        },
        body: JSON.stringify(formData)
    })

    const data = await response.json();

    return data.category;
}