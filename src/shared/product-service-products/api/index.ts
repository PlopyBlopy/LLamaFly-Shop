import { ProductServicehttpClient } from "../../api/index.ts";
import { getProductCardsURL, getProductDetailURL } from "../config/index.ts";
import { Product, ProductCard, QueryParams } from "../index.ts";

const ENDPOINT = 'Products'

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

