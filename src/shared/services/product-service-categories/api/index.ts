import { AxiosInstance } from "axios";
import { Category } from "../model";
import { API } from "@/shared/api/http-client";

export const getCategories = async (api: AxiosInstance) => {
  const response = await api.get(API.categories.categories());
  return response.data as Category[];
};
