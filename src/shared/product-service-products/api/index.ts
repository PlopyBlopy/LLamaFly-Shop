import { ProductServicehttpClient } from "../../api/index.ts";
import { addProductURL, getProductCardsURL, getProductDetailURL } from "../config/index.ts";
import { Product, ProductCard, ProductCreate, QueryParams } from "../index.ts";

const ENDPOINT = 'Products'

export const addProduct = async (product: ProductCreate ) => {
    const response = await ProductServicehttpClient.post(`${ENDPOINT}/${addProductURL}`, product);
    console.log(response.data);
}

export const getProductCards = async (params: QueryParams) => {
    const response = await ProductServicehttpClient.get(`${ENDPOINT}/${getProductCardsURL}`, { params });
    return response.data as ProductCard[];
};

export const getProductById = async (id: string) => {
    const response = await ProductServicehttpClient.get(`${ENDPOINT}/${getProductDetailURL}`, {
        params: {id},
    });
    return response.data as Product;
};