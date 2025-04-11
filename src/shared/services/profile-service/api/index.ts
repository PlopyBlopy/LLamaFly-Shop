import { AxiosInstance } from "axios";
import { AdminResponse, CustomerResponse, SellerResponse } from "../model";
import { API } from "@/shared/api/http-client";

export const getAdminProfile = async (api: AxiosInstance): Promise<AdminResponse> => {
  const response = await api.get(API.profiles.admins.admin());
  return response.data;
};

export const getSellerProfile = async (api: AxiosInstance): Promise<SellerResponse> => {
  const response = await api.get(API.profiles.sellers.seller());
  return response.data;
};

export const getCustomerProfile = async (api: AxiosInstance): Promise<CustomerResponse> => {
  const response = await api.get(API.profiles.customers.customer());
  return response.data;
};
