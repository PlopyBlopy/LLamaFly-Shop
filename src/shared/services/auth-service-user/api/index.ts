import { AxiosInstance } from "axios";
import { UserProfileCustomerRegister, UserLogin, UserProfileSellerRegister } from "../model";
import { TokenResponse } from "../../auth-service-token";
import { API } from "@/shared/api/http-client";

export const login = async (api: AxiosInstance, user: UserLogin): Promise<TokenResponse> => {
  const response = await api.post(API.auth.login(), user);
  return response.data;
};
export const logout = async (api: AxiosInstance) => {
  await api.post(API.auth.logout());
};

export const registerSeller = async (api: AxiosInstance, userSellerRegister: UserProfileSellerRegister) => {
  await api.post(API.auth.registerSeller(), userSellerRegister);
};

export const registerCustomer = async (api: AxiosInstance, userCustomerRegister: UserProfileCustomerRegister) => {
  await api.post(API.auth.registerCustomer(), userCustomerRegister);
};
