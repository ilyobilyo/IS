const baseUrl = 'https://localhost:7137/User';

export const Register = async (formData) => {
    const response = await fetch(baseUrl + '/Register', {
        method: 'post',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(formData)
    });

    const data = await response.json();

    if (data.status !== 201) {
        throw new Error(data.message)
    }

    return data;
}

export const Login = async (formData) => {
    const response = await fetch(baseUrl + '/Login', {
        method: 'post',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(formData)
    })
    
    const data = await response.json();
    
    if (data.status !== 200) {
        throw new Error(data.message)
    }
    
    return data
}

export const Logout = async (token) => {

    const response = await fetch(baseUrl + '/Logout',{
        method: 'post',
        headers: {
            'Authorization': `bearer ${token}`
        }
    })

    if (response.status !== 204) {
        throw new Error('Logout failed')
    }
}