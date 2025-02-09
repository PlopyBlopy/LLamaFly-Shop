import { ProductServicehttpClient } from "../../api"
import { getCategoriesURL } from "../config"
import { Category } from "../model"

const ENDPOINT = "Category"

export const getCategories = async () => {
    const response = await ProductServicehttpClient.get(`${ENDPOINT}/${getCategoriesURL}`);
    return response.data as Category[];
};