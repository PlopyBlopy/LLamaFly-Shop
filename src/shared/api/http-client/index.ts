import axios from "axios";

export const ProductServicehttpClient = axios.create({
    baseURL: "https://localhost:7274"
});

export const ImageServicehttpClient = axios.create({
    baseURL: "https://localhost:7275"
});


