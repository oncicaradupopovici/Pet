import axios from "axios";


export async function upload(url, formData, progressCallback) {
    const options = {
        method: 'put',
        onUploadProgress: function (progressEvent) {
            if (progressCallback)
                progressCallback(Math.round((progressEvent.loaded * 100) / progressEvent.total))
        },
        headers: getHeaders()
    };

    options.data = new FormData();
    Object.entries(formData).forEach(([key, value]) => {
        options.data.append(key, value);
    });

    return internalRequest(url, options);
}

export function get(url) {

    const options = {
        method: 'GET',
        headers: getHeaders()
    };

    return internalRequest(url, options);
}

export function post(url, data) {
    const options = {
        method: 'post',
        data: JSON.stringify(data),
        headers: getHeaders()
    };

    return internalRequest(url, options);
}

function getHeaders() {
    return {
        "Content-Type": "application/json",
        "TenantId": localStorage.getItem('TenantId')
    }
}

function internalRequest(url, options) {
    return axios.request(url, options)
        .then(res => res.data)
        .catch(function (error) {
            if (error.response && error.response.data) {
                throw { ...error.response.data, message: error.response.data.detail || error.response.data.title } || error;
            }
            // The request was made but no response was received
            // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
            // http.ClientRequest in node.js
            throw error;
        });
}