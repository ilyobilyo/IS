export const checkRequiredInputField = (fieldName, value) => {
    if (value.length === 0){
        return `${fieldName} is required`;
    }
    return '';
}

export const validatePassword = (value) => {
    if (value.length < 5) {
        return 'Password must be at least 5 characters long.'
    }
    return '';
}

export const validatePassAndRepass = (pass, repass) => {
    if (pass !== repass) {
        return 'Password and Confirm password don\' match';
    }
    return '';
}