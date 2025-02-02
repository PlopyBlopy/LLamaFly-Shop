import { ProductServicehttpClient } from "../../api"
import { getCategoriesURL } from "../config"
import { Category } from "../model"

const SLUG = "Category"

export const getCategories = async () => {
    const response = await ProductServicehttpClient.get(`${SLUG}/${getCategoriesURL}`);
    return response.data as Category[];
};