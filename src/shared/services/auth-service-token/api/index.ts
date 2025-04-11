import { AxiosInstance } from "axios";
import { TokenResponse } from "../model";
import { API } from "@/shared/api/http-client";

export const refresh = async (api: AxiosInstance): Promise<TokenResponse> => {
  const response = await api.post(API.auth.token.refresh());
  return response.data;
};
