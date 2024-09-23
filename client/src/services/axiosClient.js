import axios from "axios";

class AxiosClient {
    constructor(baseURL) {
        this.client = axios.create({
            baseURL: baseURL,
            headers: {
                "Content-Type": "application/json",
            },
        });
    }

    get = (url, config) => {
        return this.client.get(url, config);
    };

    put = (url, data, config) => {
        return this.client.put(url, data, config);
    };

    post = (url, data, config) => {
        return this.client.post(url, data, config);
    };

    delete = (url, config) => {
        return this.client.delete(url, config);
    };

    setHeaders = (headers) => {
        this.client.defaults.headers.common = headers;
    };

    interceptRequests = (onFulfilled, onRejected) => {
        this.client.interceptors.request.use(onFulfilled, onRejected);
    };

    interceptResponses = (onFulfilled, onRejected) => {
        this.client.interceptors.response.use(onFulfilled, onRejected);
    };
}

export default AxiosClient;
