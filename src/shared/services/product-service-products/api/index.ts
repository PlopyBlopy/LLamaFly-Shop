import axios, { AxiosInstance } from "axios";
import { Product, ProductCard, ProductCreate, QueryParams } from "../index.ts";
import { API } from "@/shared/api/http-client/index.ts";

export const addProduct = async (api: AxiosInstance, product: ProductCreate): Promise<string> => {
  const response = await api.post(API.products.add(), product);
  return response.data;
};

export const getProductsCards = async (api: AxiosInstance, params: QueryParams) => {
  const response = await api.get(API.products.productsCards(), {
    params,
  });
  return response.data as ProductCard[];
};

export const getProductsSellerCards = async (api: AxiosInstance, sellerId: string) => {
  try {
  } catch (error) {
    if (axios.isAxiosError(error)) {
      throw error;
    }
    throw new Error();
  }
  const response = await api.get(API.products.sellerProductsCards(sellerId));
  return response.data as ProductCard[];
};

export const getProductById = async (api: AxiosInstance, productId: string) => {
  const response = await api.get(API.products.detail(productId));
  return response.data as Product;
};
